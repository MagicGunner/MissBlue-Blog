using AutoMapper;
using Backend.Application.Service;
using Backend.Common.Enums;
using Backend.Common.Results;
using Backend.Domain.IRepository;
using Backend.Modules.Blog.Contracts.DTO;
using Backend.Modules.Blog.Contracts.IService;
using Backend.Modules.Blog.Contracts.VO;
using Backend.Modules.Blog.Domain.Entities;
using Microsoft.AspNetCore.Http;
using SqlSugar;

namespace Backend.Modules.Blog.Application.Service;

public class ArticleService(IMapper mapper, IBaseRepositories<Article> baseRepositories) : BaseServices<Article>(mapper, baseRepositories), IArticleService {
    private readonly IMapper _mapper = mapper;

    public async Task<PageVO<List<ArticleVO>>> ListAllArticle(int pageNum, int pageSize) {
        var totalCount = new RefAsync<int>();

        var pageList = await Db.Queryable<Article>()
                               .Where(it => it.Status == SQLConst.PUBLIC_ARTICLE)
                               .OrderBy(it => it.CreateTime, OrderByType.Desc)
                               .ToPageListAsync(pageNum, pageSize, totalCount);


        return new PageVO<List<ArticleVO>> {
                                               Page = await Article2VO(pageList),
                                               Total = totalCount
                                           };
    }

    public async Task<List<RecommendArticleVO>> ListRecommendArticle() {
        var articles = await Db.Queryable<Article>()
                               .Where(it => it.IsTop == SQLConst.RECOMMEND_ARTICLE
                                         && it.Status == SQLConst.PUBLIC_ARTICLE)
                               .ToListAsync();
        return articles.Select(a => _mapper.Map<RecommendArticleVO>(a)).ToList();
    }

    public async Task<List<RandomArticleVO>> ListRandomArticle() {
        var articles = await Db.Queryable<Article>()
                               .Where(it => it.Status == SQLConst.PUBLIC_ARTICLE)
                               .OrderBy(it => SqlFunc.GetRandom())
                               .Take(SQLConst.RANDOM_ARTICLE_COUNT) // 限制返回数量
                               .ToListAsync();
        return articles.Select(a => _mapper.Map<RandomArticleVO>(a)).ToList();
    }

    public Task<ArticleDetailVO> GetArticleDetail(long id) {
        throw new NotImplementedException();
    }

    public async Task<List<RelatedArticleVO>> RelatedArticleList(long categoryId, long articleId) {
        // 查询满足条件的文章
        var articles = await Db.Queryable<Article>()
                               .Where(it => it.Status == SQLConst.PUBLIC_ARTICLE) // 状态=公开
                               .Where(it => it.CategoryId == categoryId)          // 分类=目标分类
                               .Where(it => it.Id != articleId)                   // 排除自己
                               .OrderBy(it => it.CreateTime, OrderByType.Desc)    // 可以按创建时间排序
                               .Take(SQLConst.RELATED_ARTICLE_COUNT)              // 限制数量，比如5
                               .ToListAsync();
        return articles.Select(a => _mapper.Map<RelatedArticleVO>(a)).ToList();
    }

    public Task<List<TimeLineVO>> ListTimeLine() {
        throw new NotImplementedException();
    }

    public Task<List<CategoryArticleVO>> ListCategoryArticle(int type, long typeId) {
        throw new NotImplementedException();
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

    public Task<List<InitSearchTitleVO>> InitSearchByTitle() {
        throw new NotImplementedException();
    }

    public Task<List<HotArticleVO>> ListHotArticle() {
        throw new NotImplementedException();
    }

    public Task<List<SearchArticleByContentVO>> SearchArticleByContent(string content) {
        throw new NotImplementedException();
    }

    private async Task<List<ArticleVO>> Article2VO(List<Article>? articles) {
        if (articles == null || articles.Count == 0) {
            return [];
        }

        var articleIds = articles.Select(a => a.Id).ToList();
        var categoryIds = articles.Select(a => a.CategoryId).ToList();

        // 2️⃣ 查询分类
        var categoryList = await Db.Queryable<Category>()
                                   .In(it => it.Id, categoryIds)
                                   .ToListAsync();

        var categoryMap = categoryList.ToDictionary(it => it.Id, it => it.CategoryName);

        // 3️⃣ 查询文章标签关联
        var articleTagList = await Db.Queryable<ArticleTag>()
                                     .In(it => it.ArticleId, articleIds)
                                     .ToListAsync();

        var tagIds = articleTagList.Select(at => at.TagId).Distinct().ToList();

        // 4️⃣ 查询标签
        var tagList = await Db.Queryable<Tag>()
                              .In(it => it.Id, tagIds)
                              .ToListAsync();

        var tagMap = tagList.ToDictionary(it => it.Id, it => it.TagName);

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
}