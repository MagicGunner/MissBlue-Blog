using System.Linq.Expressions;
using Backend.Common.Enums;
using Backend.Domain.Entity;
using Backend.Domain.IRepository;
using Backend.Infrastructure.UnitOfWorks;
using SqlSugar;

namespace Backend.Infrastructure.Repository;

public class ArticleRepository(IUnitOfWorkManage unitOfWorkManage, ICategoryRepository categoryRepository) : BaseRepositories<Article>(unitOfWorkManage), IArticleRepository {
    public async Task<List<Article>> FullQuery(Expression<Func<Article, bool>>?   whereExpression   = null,
                                               Expression<Func<Article, object>>? orderByExpression = null,
                                               OrderByType                        orderByType       = OrderByType.Asc,
                                               int?                               pageNum           = null,
                                               int?                               pageSize          = null,
                                               RefAsync<int>?                     totalCount        = null,
                                               int?                               take              = null,
                                               bool                               isRandom          = false) {
        var query = Db.Queryable<Article>(); // _db 为 ISqlSugarClient 实例

        if (whereExpression != null) query = query.Where(whereExpression);

        if (isRandom) {
            query = query.OrderBy(SqlFunc.GetRandom());
        } else if (orderByExpression != null) {
            query = query.OrderBy(orderByExpression, orderByType);
        }

        if (pageNum.HasValue && pageSize.HasValue) return await query.ToPageListAsync(pageNum.Value, pageSize.Value, totalCount);

        if (take.HasValue) {
            query = query.Take(take.Value);
        }

        return await query.ToListAsync();
    }

    public async Task<Dictionary<long, string>> GetContentDic(List<long> userIds) =>
        (await Db.Queryable<Article>().Where(article => userIds.Contains(article.UserId)).ToListAsync())
       .ToDictionary(article => article.Id, article => article.ArticleContent);

    public async Task<List<Article>> ListAll(int pageNum, int pageSize, RefAsync<int> totalCount) {
        return await Db.Queryable<Article>()
                       .Where(article => article.Status == SQLConst.PUBLIC_ARTICLE)
                       .OrderBy(article => article.CreateTime, OrderByType.Desc)
                       .ToPageListAsync(pageNum, pageSize, totalCount);
    }

    public async Task<List<Article>> ListComment() => await Query(article => article.IsTop == SQLConst.RECOMMEND_ARTICLE && article.Status == SQLConst.PUBLIC_ARTICLE);

    public async Task<List<Article>> ListRandom() =>
        await Db.Queryable<Article>().Where(article => article.Status == SQLConst.PUBLIC_ARTICLE).OrderBy(article => SqlFunc.GetRandom()).Take(SQLConst.RANDOM_ARTICLE_COUNT).ToListAsync();

    public async Task<List<Article>> ListRelated(long categoryId, long articleId) =>
        await Db.Queryable<Article>()
                .Where(at => at.Status == SQLConst.PUBLIC_ARTICLE && at.CategoryId == categoryId && at.Id != articleId)
                .OrderBy(at => at.CreateTime, OrderByType.Desc)
                .Take(SQLConst.RELATED_ARTICLE_COUNT)
                .ToListAsync();

    public async Task<List<Article>> LisHot() =>
        await Db.Queryable<Article>()
                .Where(a => a.Status == SQLConst.PUBLIC_ARTICLE)
                .OrderBy(a => a.VisitCount, OrderByType.Desc)
                .Take(SQLConst.HotArticleCount)
                .ToListAsync();

    public async Task<Article?> FindPre(long    currentId) => await Db.Queryable<Article>().Where(a => a.Id < currentId).OrderBy(a => a.Id, OrderByType.Desc).FirstAsync();
    public async Task<Article?> FindNext(long   currentId) => await Db.Queryable<Article>().Where(a => a.Id > currentId).OrderBy(a => a.Id, OrderByType.Asc).FirstAsync();
    public async Task<long>     CountByTag(long tagId)     => await Db.Queryable<ArticleTag>().Where(at => at.TagId == tagId).CountAsync();

    public async Task<List<Article>> FindByContent(string keyword) => await Db.Queryable<Article>().Where(a => a.ArticleContent.Contains(keyword) && a.Status == SQLConst.PUBLIC_ARTICLE).ToListAsync();

    public async Task<List<Article>> ListCategoryArticle(int type, long typeId) {
        switch (type) {
            case 1: return await Db.Queryable<Article>().Where(a => a.CategoryId == typeId).ToListAsync();
            case 2: {
                var articleIds = await Db.Queryable<ArticleTag>().Where(a => a.TagId == typeId).Select(a => a.ArticleId).ToListAsync();
                return articleIds.Count > 0 ? await Db.Queryable<Article>().In(a => a.Id, articleIds).ToListAsync() : [];
            }
            default: return [];
        }
    }

    public async Task<List<Article>> SearchArticle(string? articleTitle, long? categoryId, int? status, int? isTop) {
        return await Db.Queryable<Article>()
                       .WhereIF(!string.IsNullOrWhiteSpace(articleTitle), a => a.ArticleTitle.Contains(articleTitle!))
                       .WhereIF(categoryId.HasValue, a => a.CategoryId == categoryId)
                       .WhereIF(status.HasValue, a => a.Status == status)
                       .WhereIF(isTop.HasValue, a => a.IsTop == isTop)
                       .ToListAsync();
    }

    public async Task<bool> UpdateStatus(long id, int status) {
        return await Db.Updateable<Article>().SetColumns(a => a.Status == status).Where(a => a.Id == id).ExecuteCommandAsync() > 0;
    }

    public async Task<bool> UpdateIsTop(long id, int isTop) {
        return await Db.Updateable<Article>().SetColumns(a => a.IsTop == isTop).Where(a => a.Id == id).ExecuteCommandAsync() > 0;
    }

    #region 后台接口

    public async Task<List<Article>> ListAll() => await Db.Queryable<Article>().OrderByDescending(article => article.CreateTime).ToListAsync();

    public async Task<bool> Publish(Article article, List<long> tagIds) {
        var articleId = article.Id;
        if (await Db.Queryable<Article>().AnyAsync(a => a.Id == articleId)) return false;
        return (await Db.Ado.UseTranAsync(async () => {
                                              await Add(article);
                                              // 2. 删除旧标签关系
                                              await Db.Deleteable<ArticleTag>().Where(at => at.ArticleId == article.Id).ExecuteCommandAsync();
                                              // 4. 插入新标签关系
                                              var tags = tagIds.Select(tagId => new ArticleTag {
                                                                                                   ArticleId = article.Id,
                                                                                                   TagId = tagId
                                                                                               })
                                                               .ToList();

                                              await Db.Insertable(tags).ExecuteCommandAsync();
                                          })).IsSuccess;
    }

    #endregion
}