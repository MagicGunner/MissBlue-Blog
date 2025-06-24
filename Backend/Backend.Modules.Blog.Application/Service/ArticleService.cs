using Backend.Common.Results;
using Backend.Modules.Blog.Contracts.DTO;
using Backend.Modules.Blog.Contracts.Interface;
using Backend.Modules.Blog.Contracts.VO;
using Backend.Modules.Blog.Domain.Entities;
using Microsoft.AspNetCore.Http;
using SqlSugar;

namespace Backend.Modules.Blog.Application.Service;

public class ArticleService(ISqlSugarClient db) : IArticleService {
    public async Task<PageVO<List<ArticleVO>>> ListAllArticleAsync(int pageNum, int pageSize) {
        RefAsync<int> total = 0;
        var articleList = await db.Queryable<Article>()
                                  .Where(a => a.Status == 0 && a.IsDeleted == 0)
                                  .OrderBy(a => a.CreateTime, OrderByType.Desc)
                                  .ToPageListAsync(pageNum, pageSize, total);

        var articleIds = articleList.Select(a => a.Id).ToList();
        var categoryIds = articleList.Select(a => a.CategoryId).Distinct().ToList();

        // 1. 查询分类信息
        var categories = (await db.Queryable<Category>()
                                  .In(c => c.Id, categoryIds)
                                  .ToDictionaryAsync(c => c.Id, c => c.CategoryName))
           .ToDictionary(kv => long.Parse(kv.Key), kv => kv.Value?.ToString() ?? "");

        // 2. 查询中间表 ArticleTag
        var articleTags = await db.Queryable<ArticleTag>()
                                  .Where(at => articleIds.Contains(at.ArticleId))
                                  .ToListAsync();

        var tagIds = articleTags.Select(at => at.TagId).Distinct().ToList();

        // 3. 查询标签名
        var tags = (await db.Queryable<Tag>()
                            .In(t => t.Id, tagIds)
                            .ToDictionaryAsync(t => t.Id, t => t.TagName))
           .ToDictionary(kv => long.Parse(kv.Key), kv => kv.Value?.ToString() ?? "");

        // 4.映射 VO
        var voList = articleList.Select(article => {
                                            var vo = new ArticleVO {
                                                                       Id = article.Id,
                                                                       ArticleTitle = article.ArticleTitle,
                                                                       ArticleCover = article.ArticleCover,
                                                                       ArticleContent = article.ArticleContent,
                                                                       ArticleType = article.ArticleType,
                                                                       VisitCount = article.VisitCount,
                                                                       CreateTime = article.CreateTime,
                                                                       UpdateTime = article.UpdateTime,
                                                                       CategoryName = categories.TryGetValue(article.CategoryId, out var cname) ? cname : "",
                                                                       Tags = articleTags
                                                                             .Where(at => at.ArticleId == article.Id)
                                                                             .Select(at => tags.TryGetValue(at.ArticleId, out var tagName) ? tagName : "")
                                                                             .ToList()
                                                                   };

                                            return vo;
                                        })
                                .ToList();

        return new PageVO<List<ArticleVO>> {
                                               Page = voList,
                                               Total = total
                                           };
    }

    public Task<List<RecommendArticleVO>> ListRecommendArticleAsync() {
        throw new NotImplementedException();
    }

    public Task<List<RandomArticleVO>> ListRandomArticleAsync() {
        throw new NotImplementedException();
    }

    public Task<ArticleDetailVO> GetArticleDetailAsync(long id) {
        throw new NotImplementedException();
    }

    public Task<List<RelatedArticleVO>> RelatedArticleListAsync(long categoryId, long articleId) {
        throw new NotImplementedException();
    }

    public Task<List<TimeLineVO>> ListTimeLineAsync() {
        throw new NotImplementedException();
    }

    public Task<List<CategoryArticleVO>> ListCategoryArticleAsync(int type, long typeId) {
        throw new NotImplementedException();
    }

    public Task AddVisitCountAsync(long id) {
        throw new NotImplementedException();
    }

    public Task<string> UploadArticleCoverAsync(IFormFile articleCover) {
        throw new NotImplementedException();
    }

    public async Task<ResponseResult<object>> PublishAsync(ArticleDto articleDto) {
        var result = await db.Ado.UseTranAsync(async () => {
                                                   // 1. 将 DTO 映射为实体
                                                   // todo 这边要重写
                                                   var article = new Article {
                                                                                 Id = articleDto.Id ?? 0,
                                                                                 ArticleTitle = articleDto.ArticleTitle,
                                                                                 ArticleContent = articleDto.ArticleContent,
                                                                                 ArticleCover = articleDto.ArticleCover,
                                                                                 ArticleType = articleDto.ArticleType,
                                                                                 Status = articleDto.Status,
                                                                                 IsTop = articleDto.IsTop,
                                                                                 CategoryId = articleDto.CategoryId,
                                                                                 UserId = 1 // 从上下文中获取登录用户
                                                                             };

                                                   // 2. 插入或更新文章
                                                   var articleResult = await db.Storageable(article).ExecuteCommandAsync();

                                                   // 3. 删除旧的标签关系
                                                   await db.Deleteable<ArticleTag>()
                                                           .Where(at => at.ArticleId == article.Id)
                                                           .ExecuteCommandAsync();

                                                   // 4. 插入新的标签关系
                                                   var articleTags = articleDto.TagId.Select(tagId => new ArticleTag {
                                                                                                                         ArticleId = article.Id,
                                                                                                                         TagId = tagId
                                                                                                                     })
                                                                               .ToList();

                                                   await db.Insertable(articleTags).ExecuteCommandAsync();
                                               });
        return result.IsSuccess
                   ? ResponseResult<object>.Success()
                   : ResponseResult<object>.Failure(msg: "发布失败");
    }

    public Task DeleteArticleCoverAsync(string articleCoverUrl) {
        throw new NotImplementedException();
    }

    public Task<string> UploadArticleImageAsync(IFormFile articleImage) {
        throw new NotImplementedException();
    }

    public Task<List<ArticleListVO>> ListArticleAsync() {
        throw new NotImplementedException();
    }

    public Task<List<ArticleListVO>> SearchArticleAsync(SearchArticleDTO searchDto) {
        throw new NotImplementedException();
    }

    public Task UpdateStatusAsync(long id, int status) {
        throw new NotImplementedException();
    }

    public Task UpdateIsTopAsync(long id, bool isTop) {
        throw new NotImplementedException();
    }

    public Task<ArticleDto> GetArticleDtoAsync(long id) {
        throw new NotImplementedException();
    }

    public Task DeleteArticleAsync(List<long> ids) {
        throw new NotImplementedException();
    }

    public Task<List<InitSearchTitleVO>> InitSearchByTitleAsync() {
        throw new NotImplementedException();
    }

    public Task<List<HotArticleVO>> ListHotArticleAsync() {
        throw new NotImplementedException();
    }

    public Task<List<SearchArticleByContentVO>> SearchArticleByContentAsync(string content) {
        throw new NotImplementedException();
    }
}