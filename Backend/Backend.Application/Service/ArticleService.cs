using AutoMapper;
using Backend.Application.Interface;
using Backend.Common.Const;
using Backend.Common.Enums;
using Backend.Common.Redis;
using Backend.Common.Utils;
using Backend.Contracts.DTO;
using Backend.Contracts.IService;
using Backend.Contracts.VO;
using Backend.Domain.Entity;
using Backend.Domain.Enums;
using Backend.Domain.IRepository;
using Microsoft.AspNetCore.Http;
using SqlSugar;

namespace Backend.Application.Service;

public class ArticleService(IMapper                    mapper,
                            IBaseRepositories<Article> baseRepositories,
                            IArticleRepository         articleRepository,
                            ICategoryRepository        categoryRepository,
                            ITagRepository             tagRepository,
                            ICommentRepository         commentRepository,
                            ILikeRepository            likeRepository,
                            IFavoriteRepository        favoriteRepository,
                            IRedisBasketRepository     redisBasketRepository,
                            IUserRepository            userRepository,
                            IMinIOService              minIoService,
                            ICurrentUser               currentUser)
    : BaseServices<Article>(mapper, baseRepositories), IArticleService {
    private readonly IMapper _mapper = mapper;

    public async Task<PageVO<List<ArticleVO>>> ListAll(int pageNum, int pageSize) {
        var totalCount = new RefAsync<int>();
        var pageList = await articleRepository.ListAll(pageNum, pageSize, totalCount);
        var articleVOs = await Article2VO(pageList);

        // 1. 判断 Redis 是否已缓存点赞/评论/收藏数
        var hasKey = await redisBasketRepository.Exist(RedisConst.ARTICLE_LIKE_COUNT) &&
                     await redisBasketRepository.Exist(RedisConst.ARTICLE_COMMENT_COUNT) &&
                     await redisBasketRepository.Exist(RedisConst.ARTICLE_FAVORITE_COUNT);

        // 6. Redis 点赞/评论/收藏数量（如果缓存存在）
        if (hasKey) {
            foreach (var vo in articleVOs) {
                await SetArticleCountAsync(vo, RedisConst.ARTICLE_LIKE_COUNT, CountType.Like);
                await SetArticleCountAsync(vo, RedisConst.ARTICLE_COMMENT_COUNT, CountType.Comment);
                await SetArticleCountAsync(vo, RedisConst.ARTICLE_FAVORITE_COUNT, CountType.Favorite);
            }
        }


        return new PageVO<List<ArticleVO>> {
                                               Page = articleVOs,
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
        // var articleTags = await Db.Queryable<ArticleTag>().In(at => at.ArticleId, articleIds).ToListAsync();
        var tagDic = await tagRepository.GetDicByArticleId(articleIds);
        return articles.Select(article => {
                                   var vo = _mapper.Map<CategoryArticleVO>(article);
                                   vo.Tags = tagDic[article.Id].Select(at => _mapper.Map<TagVO>(at)).ToList();
                                   return vo;
                               })
                       .ToList();
    }

    public async Task<bool> AddVisitCount(long articleId) {
        var ip = currentUser.IpAddress;
        if (string.IsNullOrEmpty(ip)) return false;

        var limitKey = $"visit_limit:{articleId}:{ip}";
        var countKey = $"visit_count:{articleId}";

        // 判断该 IP 是否在限流范围内
        if (!await redisBasketRepository.Exist(limitKey)) {
            // 设置 IP 限流 key，过期自动删除
            await redisBasketRepository.Set(limitKey, "1", TimeSpan.FromSeconds(RedisConst.ARTICLE_VISIT_COUNT_INTERVAL));

            // 判断访问计数 key 是否存在
            if (await redisBasketRepository.Exist(countKey)) await redisBasketRepository.IncrementSortedSetScoreAsync("article_views_count", $"article_{articleId}");
            else await redisBasketRepository.AddOrUpdateSortedSetAsync("article_views_count", 1, $"article_{articleId}");
        }

        return true;
    }

    public async Task<string> UploadArticleCover(IFormFile articleCover) {
        if (articleCover == null || articleCover.Length == 0) throw new Exception("上传文件不能为空");

        try {
            // 调用 MinIO 上传，类型为 ArticleCover（自动包含格式、大小校验等）
            var fileUrl = await minIoService.UploadAsync(UploadEnum.ArticleCover, articleCover);
            return fileUrl;
        } catch (Exception ex) {
            throw new Exception("文章封面上传失败：" + ex.Message);
        }
    }

    public async Task<bool> Publish(ArticleDto articleDto) {
        var article = _mapper.Map<Article>(articleDto);
        if (currentUser.UserId == null) {
            return false;
        }

        article.UserId = currentUser.UserId.Value;
        return await articleRepository.Publish(article, articleDto.TagId);
    }

    public async Task<bool> DeleteArticleCover(string articleCoverUrl) {
        try {
            // 提取对象名（即 MinIO 中的 ObjectName）
            var bucketName = minIoService.BucketName;
            var index = articleCoverUrl.IndexOf(bucketName, StringComparison.Ordinal);
            if (index == -1) return false;

            var objectName = articleCoverUrl.Substring(index + bucketName.Length).TrimStart('/');

            // 调用删除方法
            var success = await minIoService.DeleteAsync(objectName);
            return success;
        } catch (Exception ex) {
            return false;
        }
    }

    public async Task<string> UploadArticleImage(IFormFile articleImage) {
        if (articleImage == null || articleImage.Length == 0) throw new Exception("上传文件不能为空");

        try {
            // 调用 MinIO 上传，类型为 ArticleImage（自动格式和大小校验）
            var fileUrl = await minIoService.UploadAsync(UploadEnum.ArticleImage, articleImage);
            return fileUrl;
        } catch (Exception ex) {
            throw new Exception("文章图片上传失败：" + ex.Message);
        }
    }

    public async Task<List<ArticleListVO>> ListArticle() {
        var articles = await articleRepository.ListAll();
        var categoryDic = await categoryRepository.GetNameDic(articles.Select(a => a.CategoryId).ToList());
        var tagDic = await tagRepository.GetDicByArticleId(articles.Select(a => a.Id).ToList());
        return articles.Select(a => {
                                   var vo = _mapper.Map<ArticleListVO>(a);
                                   if (categoryDic.TryGetValue(a.CategoryId, out var categoryName)) vo.CategoryName = categoryName;
                                   if (tagDic.TryGetValue(a.CategoryId, out var tags)) vo.TagsName = tags.Select(tag => tag.TagName).ToList();
                                   return vo;
                               })
                       .ToList();
    }

    public async Task<List<ArticleListVO>> SearchArticle(SearchArticleDTO searchDto) {
        var articles = await articleRepository.SearchArticle(searchDto.ArticleTitle, searchDto.CategoryId, searchDto.Status, searchDto.IsTop);
        if (articles.Count == 0) return [];
        var categoryNameDic = await categoryRepository.GetNameDic(articles.Select(a => a.CategoryId).ToList());
        var dicByArticleId = await tagRepository.GetDicByArticleId(articles.Select(a => a.Id).ToList());
        var userNameDic = await userRepository.GetNameDicByIds(articles.Select(a => a.UserId).ToList());
        return articles.Select(a => {
                                   var vo = _mapper.Map<ArticleListVO>(a);
                                   if (categoryNameDic.TryGetValue(a.CategoryId, out var categoryName)) vo.CategoryName = categoryName;
                                   if (dicByArticleId.TryGetValue(a.Id, out var tags)) vo.TagsName = tags.Select(t => t.TagName).ToList();
                                   if (userNameDic.TryGetValue(a.UserId, out var userName)) vo.UserName = userName;
                                   return vo;
                               })
                       .ToList();
    }

    public async Task<bool> UpdateStatus(long id, int status) => await articleRepository.UpdateStatus(id, status);

    public async Task<bool> UpdateIsTop(long id, int isTop) => await articleRepository.UpdateIsTop(id, isTop);

    public async Task<ArticleDto?> GetArticleDto(long id) {
        var article = await articleRepository.GetById(id);
        if (article == null) return null;
        var articleDto = _mapper.Map<ArticleDto>(article);
        var tagDic = await tagRepository.GetDicByArticleId([id]);
        if (tagDic.TryGetValue(id, out var tags)) {
            articleDto.TagId = tags.Select(t => t.Id).ToList();
        }

        return articleDto;
    }

    public async Task<bool> DeleteArticle(List<long> ids) {
        return (await Db.Ado.UseTranAsync(async () => {
                                              var success = await articleRepository.DeleteByIds(ids);
                                              if (!success) throw new Exception("删除文章失败");

                                              // 2. 删除文章标签关联
                                              await Db.Deleteable<ArticleTag>()
                                                      .Where(at => ids.Contains(at.ArticleId))
                                                      .ExecuteCommandAsync();

                                              // 3. 删除文章点赞
                                              await Db.Deleteable<Like>()
                                                      .Where(l => l.Type == (int)LikeType.Article && ids.Contains(l.TypeId))
                                                      .ExecuteCommandAsync();

                                              // 4. 删除文章收藏
                                              await Db.Deleteable<Favorite>()
                                                      .Where(f => f.Type == (int)FavoriteType.Article && ids.Contains(f.TypeId))
                                                      .ExecuteCommandAsync();

                                              // 5. 删除文章评论
                                              await Db.Deleteable<Comment>()
                                                      .Where(c => c.Type == (int)CommentType.Article && ids.Contains(c.TypeId))
                                                      .ExecuteCommandAsync();
                                          })).IsSuccess;
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

        var articleId = articleVO.Id.ToString();
        // 获取 Redis 中的字段值（Hash）
        var countObj = await redisBasketRepository.HashGetAsync(redisKey, articleId);

        long count = 0;

        if (countObj.HasValue) {
            long.TryParse(countObj.ToString(), out count);
        } else {
            // 如果缓存不存在，初始化为 0
            await redisBasketRepository.HashSetAsync(redisKey, articleId, 0);
        }

        switch (articleField) {
            // 设置到 VO 对象中
            case CountType.Favorite: articleVO.FavoriteCount = count; break;
            case CountType.Like:     articleVO.LikeCount = count; break;
            case CountType.Comment:  articleVO.CommentCount = count; break;
        }
    }
}