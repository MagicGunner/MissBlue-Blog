using AutoMapper;
using Backend.Application.Service;
using Backend.Common.Enums;
using Backend.Common.Results;
using Backend.Common.Utils;
using Backend.Domain.IRepository;
using Backend.Modules.Blog.Contracts.DTO;
using Backend.Modules.Blog.Contracts.IService;
using Backend.Modules.Blog.Contracts.VO;
using Backend.Modules.Blog.Domain.Entities;
using Backend.Modules.Blog.Domain.Enums;
using Backend.Modules.Blog.Domain.IRepository;
using Microsoft.AspNetCore.Http;
using SqlSugar;

namespace Backend.Modules.Blog.Application.Service;

public class ArticleService(IMapper                    mapper,
                            IBaseRepositories<Article> baseRepositories,
                            IArticleRepository         articleRepository,
                            ICategoryRepository        categoryRepository,
                            ITagRepository             tagRepository,
                            ICommentRepository         commentRepository,
                            ILikeRepository            likeRepository,
                            IFavoriteRepository        favoriteRepository)
    : BaseServices<Article>(mapper, baseRepositories), IArticleService {
    private readonly IMapper _mapper = mapper;

    public async Task<PageVO<List<ArticleVO>>> ListAll(int pageNum, int pageSize) {
        var totalCount = new RefAsync<int>();
        var pageList = await articleRepository.ListAll(pageNum, pageSize, totalCount);

        // todo 完善Redis相关功能

        // 1. 判断 Redis 是否已缓存点赞/评论/收藏数
        // var hasKey = await _redisClient.KeyExistsAsync(RedisConst.ARTICLE_LIKE_COUNT) &&
        //              await _redisClient.KeyExistsAsync(RedisConst.ARTICLE_COMMENT_COUNT) &&
        //              await _redisClient.KeyExistsAsync(RedisConst.ARTICLE_FAVORITE_COUNT);

        // 6. Redis 点赞/评论/收藏数量（如果缓存存在）
        // if (hasKey) {
        //     foreach (var vo in articleVOs) {
        //         await SetArticleCountAsync(vo, RedisConst.ARTICLE_LIKE_COUNT, CountTypeEnum.Like);
        //         await SetArticleCountAsync(vo, RedisConst.ARTICLE_COMMENT_COUNT, CountTypeEnum.Comment);
        //         await SetArticleCountAsync(vo, RedisConst.ARTICLE_FAVORITE_COUNT, CountTypeEnum.Favorite);
        //     }
        // }


        return new PageVO<List<ArticleVO>> {
                                               Page = await Article2VO(pageList),
                                               Total = totalCount
                                           };
    }

    public async Task<List<RecommendArticleVO>> ListRecommend() {
        var articles = await articleRepository.ListComment();
        return articles.Select(a => _mapper.Map<RecommendArticleVO>(a)).ToList();
    }

    public async Task<List<RandomArticleVO>> ListRandomArticle() {
        var articles = await articleRepository.ListRandom();
        return articles.Select(a => _mapper.Map<RandomArticleVO>(a)).ToList();
    }

    public async Task<ArticleDetailVO?> GetDetail(long id) {
        var article = await articleRepository.GetById(id);
        if (article == null) {
            return null;
        }

        var category = await categoryRepository.GetById(article.CategoryId);
        var tagsId = await Db.Queryable<ArticleTag>().Where(articleTag => articleTag.ArticleId == article.Id).Select(articleTag => articleTag.TagId).ToListAsync();
        var tags = await tagRepository.GetByIds(tagsId);

        // 当前文章的上一篇文章与下一篇文章,大于当前文章的最小文章与小于当前文章的最大文章
        var preArticle = await articleRepository.FindPre(article.Id);
        var nextArticle = await articleRepository.FindNext(article.Id);
        var tagVoTasks = tags.Select(async tag => {
                                         var vo = _mapper.Map<TagVO>(tag);
                                         vo.ArticleCount = await articleRepository.CountByTag(tag.Id);
                                         return vo;
                                     });
        var tagVos = (await Task.WhenAll(tagVoTasks)).ToList();
        var articleDetailVo = _mapper.Map<ArticleDetailVO>(article);
        articleDetailVo.CategoryName = category.CategoryName;
        articleDetailVo.CategoryId = category.Id;
        articleDetailVo.Tags = tagVos;
        articleDetailVo.CommentCount = await commentRepository.GetCount(CommentType.Article, article.Id);
        articleDetailVo.LikeCount = await likeRepository.GetCount(LikeType.Article, article.Id);
        articleDetailVo.FavoriteCount = await favoriteRepository.GetCount(FavoriteType.Article, article.Id);
        if (preArticle != null) {
            articleDetailVo.PreArticleId = preArticle.Id;
            articleDetailVo.PreArticleTitle = preArticle.ArticleTitle;
        }

        if (nextArticle != null) {
            articleDetailVo.NextArticleId = nextArticle.Id;
            articleDetailVo.NextArticleTitle = nextArticle.ArticleTitle;
        }

        return articleDetailVo;
    }

    public async Task<List<RelatedArticleVO>> RelatedArticleList(long categoryId, long articleId) {
        var articles = await articleRepository.ListRelated(categoryId, articleId);
        return articles.Select(a => _mapper.Map<RelatedArticleVO>(a)).ToList();
    }

    public async Task<List<TimeLineVO>> ListTimeLine() => await Query<TimeLineVO>();

    public async Task<List<CategoryArticleVO>> ListCategoryArticle(int type, long typeId) {
        var articles = await articleRepository.ListCategoryArticle(type, typeId);
        var articleIds = articles.Select(a => a.Id).ToList();
        var articleTags = await Db.Queryable<ArticleTag>().In(at => at.ArticleId, articleIds).ToListAsync();
        var tagDic = new Dictionary<long, Tag>();
    }

    public Task AddVisitCount(long id) {
        throw new NotImplementedException();
    }

    public Task<string> UploadArticleCover(IFormFile articleCover) {
        throw new NotImplementedException();
    }

    public Task<ResponseResult<object>> Publish(ArticleDto articleDto) {
        throw new NotImplementedException();
    }

    public Task DeleteArticleCover(string articleCoverUrl) {
        throw new NotImplementedException();
    }

    public Task<string> UploadArticleImage(IFormFile articleImage) {
        throw new NotImplementedException();
    }

    public Task<List<ArticleListVO>> ListArticle() {
        throw new NotImplementedException();
    }

    public Task<List<ArticleListVO>> SearchArticle(SearchArticleDTO searchDto) {
        throw new NotImplementedException();
    }

    public Task UpdateStatus(long id, int status) {
        throw new NotImplementedException();
    }

    public Task UpdateIsTop(long id, bool isTop) {
        throw new NotImplementedException();
    }

    public Task<ArticleDto> GetArticleDto(long id) {
        throw new NotImplementedException();
    }

    public Task DeleteArticle(List<long> ids) {
        throw new NotImplementedException();
    }

    public async Task<List<InitSearchTitleVO>> InitSearchByTitle() {
        // 查询公开文章
        var articles = await articleRepository.Query(article => article.Status == SQLConst.PUBLIC_ARTICLE);

        if (articles.Count == 0) return [];
        var categoryDic = await categoryRepository.GetNameDic(articles.Select(a => a.CategoryId).ToList());
        // 映射为 VO，并设置 CategoryNam
        return articles.Select(article => {
                                   var vo = _mapper.Map<InitSearchTitleVO>(article);
                                   if (categoryDic.TryGetValue(article.CategoryId, out var categoryName)) vo.CategoryName = categoryName;
                                   return vo;
                               })
                       .ToList();
    }

    public async Task<List<HotArticleVO>> ListHotArticle() => (await articleRepository.LisHot()).Select(a => _mapper.Map<HotArticleVO>(a)).ToList();


    public async Task<List<SearchArticleByContentVO>> SearchArticleByContent(string keyword) {
        // 1. 查询文章内容包含关键字且为公开状态的文章
        var articles = await articleRepository.FindByContent(keyword);

        if (articles.Count == 0) return [];
        var categoryDic = await categoryRepository.GetNameDic(articles.Select(a => a.CategoryId).ToList());
        // 3. 转换为 VO 并设置 CategoryName
        var listVos = articles.Select(article => {
                                          var vo = _mapper.Map<SearchArticleByContentVO>(article);
                                          if (categoryDic.TryGetValue(article.CategoryId, out var categoryName)) vo.CategoryName = categoryName;
                                          return vo;
                                      })
                              .ToList();
        // 4. 处理匹配内容摘要
        var hasMatch = false;
        foreach (var vo in listVos) {
            var content = vo.ArticleContent ?? "";
            var index = content.IndexOf(keyword, StringComparison.OrdinalIgnoreCase);
            if (index != -1) {
                hasMatch = true;
                var length = Math.Min(content.Length - index, keyword.Length + 20);
                var excerpt = content.Substring(index, length);
                vo.ArticleContent = MarkdownUtil.ExtractTextFromMarkdown(excerpt);
            }
        }

        return hasMatch ? listVos : [];
    }

    private async Task<List<ArticleVO>> Article2VO(List<Article>? articles) {
        if (articles == null || articles.Count == 0) {
            return [];
        }

        var articleIds = articles.Select(a => a.Id).ToList();
        var categoryIds = articles.Select(a => a.CategoryId).ToList();

        var categoryMap = await categoryRepository.GetNameDic(categoryIds);

        // 3️⃣ 查询文章标签关联
        var articleTagList = await Db.Queryable<ArticleTag>()
                                     .In(articleTag => articleTag.ArticleId, articleIds)
                                     .ToListAsync();

        var tagIds = articleTagList.Select(at => at.TagId).Distinct().ToList();

        // 4️⃣ 查询标签

        var tagMap = await tagRepository.GetNameDic(tagIds);

        // 5️⃣ 封装VO
        return articles.Select(article => {
                                   var articleVo = _mapper.Map<ArticleVO>(article);
                                   articleVo.CategoryName = categoryMap.GetValueOrDefault(article.CategoryId);
                                   articleVo.Tags = articleTagList.Where(at => at.ArticleId == article.Id && tagMap.ContainsKey(at.TagId))
                                                                  .Select(at => tagMap[at.TagId])
                                                                  .ToList();
                                   return articleVo;
                               })
                       .ToList();
    }

    private async Task SetArticleCountAsync(ArticleVO articleVO, string redisKey, CountType articleField) {
        // todo 完善Redis相关功能

        // var articleId = articleVO.Id.ToString();
        //
        // // 获取 Redis 中的字段值（Hash）
        // var countObj = await _redisClient.HashGetAsync(redisKey, articleId);
        //
        // long count = 0;
        //
        // if (countObj.HasValue) {
        //     long.TryParse(countObj.ToString(), out count);
        // } else {
        //     // 如果缓存不存在，初始化为 0
        //     await _redisClient.HashSetAsync(redisKey, articleId, 0);
        // }
        //
        // // 设置到 VO 对象中
        // switch (articleField) {
        //     case CountTypeEnum.Favorite: articleVO.FavoriteCount = count; break;
        //     case CountTypeEnum.Like:     articleVO.LikeCount = count; break;
        //     case CountTypeEnum.Comment:  articleVO.CommentCount = count; break;
        // }
    }
}