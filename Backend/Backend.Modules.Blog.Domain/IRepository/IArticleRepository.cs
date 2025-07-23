using System.Linq.Expressions;
using Backend.Domain.IRepository;
using Backend.Modules.Blog.Domain.Entities;
using SqlSugar;

namespace Backend.Modules.Blog.Domain.IRepository;

public interface IArticleRepository : IBaseRepositories<Article> {
    Task<List<Article>> FullQuery(Expression<Func<Article, bool>>?   whereExpression   = null,
                                  Expression<Func<Article, object>>? orderByExpression = null,
                                  OrderByType                        orderByType       = OrderByType.Asc,
                                  int?                               pageNum           = null,
                                  int?                               pageSize          = null,
                                  RefAsync<int>?                     totalCount        = null,
                                  int?                               take              = null,
                                  bool                               isRandom          = false);

    Task<Dictionary<long, string>> GetContentDic(List<long> userIds);

    /// <summary>
    /// 前台展根据页面数量和页面尺寸示文章
    /// </summary>
    /// <param name="pageNum"></param>
    /// <param name="pageSize"></param>
    /// <param name="totalCount"></param>
    /// <returns></returns>
    Task<List<Article>> ListAll(int pageNum, int pageSize, RefAsync<int> totalCount);

    Task<List<Article>> ListComment();
    Task<List<Article>> ListRandom();
    Task<List<Article>> ListRelated(long categoryId, long articleId);
    Task<List<Article>> LisHot();
    Task<Article?>      GetById(long            id);
    Task<Article?>      FindPre(long            currentId);
    Task<Article?>      FindNext(long           currentId);
    Task<long>          CountByTag(long         tagId);
    Task<List<Article>> FindByContent(string    keyword);
    Task<List<Article>> ListCategoryArticle(int type, long typeId);

    Task<List<Article>> SearchArticle(string? articleTitle, long? categoryId, int? status, int? isTop);

    Task<bool> UpdateStatus(long id, int status);
    Task<bool> UpdateIsTop(long  id, int isTop);

    #region 后台接口

    Task<List<Article>> ListAll();
    Task<bool>          Publish(Article article, List<long> tagIds);

    #endregion
}