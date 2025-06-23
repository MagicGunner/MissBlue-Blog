using Backend.Common.Results;
using Backend.Modules.Blog.Contracts.DTO;
using Backend.Modules.Blog.Contracts.Interface;
using Backend.Modules.Blog.Contracts.VO;
using Backend.Modules.Blog.Domain.Entities;
using Microsoft.AspNetCore.Http;
using SqlSugar;

namespace Backend.Modules.Blog.Application.Service;

public class ArticleService : IArticleService {
    private readonly ISqlSugarClient _db;

    public ArticleService(ISqlSugarClient db) {
        _db = db;
        Console.WriteLine("✅ ArticleService 构造函数被调用了");
    }

    public async Task<PageVO<List<ArticleVO>>> ListAllArticleAsync(int pageNum, int pageSize) {
        RefAsync<int> total = 0;

        var articleList = await _db.Queryable<Article>().ToListAsync();

        if (articleList == null || articleList.Count == 0) {
            Console.WriteLine("No articles found."); // 可选日志
        }

        // var articleList = await _db.Queryable<Article>()
        //                            .Where(a => a.Status == 1 && a.IsDeleted == 0)
        //                            .OrderBy(a => a.CreateTime, OrderByType.Desc)
        //                            .ToPageListAsync(pageNum, pageSize, total);
        //
        // var articleIds = articleList.Select(a => a.Id).ToList();
        // var categoryIds = articleList.Select(a => a.CategoryId).Distinct().ToList();
        //
        // // 1. 查询分类信息
        // var categories = await _db.Queryable<Category>()
        //                           .In(c => c.Id, categoryIds)
        //                           .ToDictionaryAsync(c => c.Id, c => c.CategoryName);
        //
        // // 2. 查询中间表 ArticleTag
        // var articleTags = await _db.Queryable<ArticleTag>()
        //                            .Where(at => articleIds.Contains(at.ArticleId))
        //                            .ToListAsync();
        //
        // var tagIds = articleTags.Select(at => at.TagId).Distinct().ToList();
        //
        // // 3. 查询标签名
        // var tags = await _db.Queryable<Tag>()
        //                     .In(t => t.Id, tagIds)
        //                     .ToDictionaryAsync(t => t.Id, t => t.TagName);

        // 4. 映射 VO
        // var voList = articleList.Select(article => {
        //                                     var vo = new ArticleVO {
        //                                                                Id = article.Id,
        //                                                                ArticleTitle = article.ArticleTitle,
        //                                                                ArticleCover = article.ArticleCover,
        //                                                                ArticleContent = article.ArticleContent,
        //                                                                ArticleType = article.ArticleType,
        //                                                                VisitCount = article.VisitCount,
        //                                                                CreateTime = article.CreateTime,
        //                                                                UpdateTime = article.UpdateTime,
        //                                                                CategoryName = categories.TryGetValue(article.CategoryId, out var cname) ? cname : "",
        //                                                                Tags = articleTags
        //                                                                      .Where(at => at.ArticleId == article.Id)
        //                                                                      .Select(at => tags.TryGetValue(at.Id, out var tname) ? tname : "")
        //                                                                      .ToList()
        //                                                            };
        //
        //                                     return vo;
        //                                 })
        //                         .ToList();
        //
        // // 5. 从 Redis 补全评论数 / 点赞数 / 收藏数
        // var redisKeys = new[] { RedisConst.ARTICLE_COMMENT_COUNT, RedisConst.ARTICLE_LIKE_COUNT, RedisConst.ARTICLE_FAVORITE_COUNT };
        //
        // if (_redis.Exists(redisKeys[0]) && _redis.Exists(redisKeys[1]) && _redis.Exists(redisKeys[2])) {
        //     foreach (var vo in voList) {
        //         vo.CommentCount = _redis.HGet<long>(RedisConst.ARTICLE_COMMENT_COUNT, vo.Id.ToString());
        //         vo.LikeCount = _redis.HGet<long>(RedisConst.ARTICLE_LIKE_COUNT, vo.Id.ToString());
        //         vo.FavoriteCount = _redis.HGet<long>(RedisConst.ARTICLE_FAVORITE_COUNT, vo.Id.ToString());
        //     }
        // }
        //
        // return new PageVO<List<ArticleVO>>(voList, total);
        return null;
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
        var result = await _db.Ado.UseTranAsync(async () => {
                                                    // 1. 将 DTO 映射为实体
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
                                                    var articleResult = await _db.Storageable(article).ExecuteCommandAsync();

                                                    // 3. 删除旧的标签关系
                                                    await _db.Deleteable<ArticleTag>()
                                                             .Where(at => at.ArticleId == article.Id)
                                                             .ExecuteCommandAsync();

                                                    // 4. 插入新的标签关系
                                                    var articleTags = articleDto.TagId.Select(tagId => new ArticleTag {
                                                                                                                          ArticleId = article.Id,
                                                                                                                          TagId = tagId
                                                                                                                      })
                                                                                .ToList();

                                                    await _db.Insertable(articleTags).ExecuteCommandAsync();
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