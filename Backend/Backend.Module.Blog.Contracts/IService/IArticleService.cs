using Backend.Common.Results;
using Backend.Modules.Blog.Contracts.DTO;
using Backend.Modules.Blog.Contracts.VO;
using Microsoft.AspNetCore.Http;

namespace Backend.Modules.Blog.Contracts.IService;

public interface IArticleService {
    Task<PageVO<List<ArticleVO>>>        ListAll(int pageNum, int pageSize);
    Task<List<RecommendArticleVO>>       ListRecommend();
    Task<List<RandomArticleVO>>          ListRandomArticle();
    Task<ArticleDetailVO?>               GetDetail(long          id);
    Task<List<RelatedArticleVO>>         RelatedArticleList(long categoryId, long articleId);
    Task<List<TimeLineVO>>               ListTimeLine();
    Task<List<CategoryArticleVO>>        ListCategoryArticle(int      type, long typeId);
    Task<bool>                           AddVisitCount(long           articleId);
    Task<string>                         UploadArticleCover(IFormFile articleCover);
    Task<bool>                           Publish(ArticleDto           articleDto);
    Task<bool>                           DeleteArticleCover(string    articleCoverUrl);
    Task<string>                         UploadArticleImage(IFormFile articleImage);
    Task<List<ArticleListVO>>            ListArticle();
    Task<List<ArticleListVO>>            SearchArticle(SearchArticleDTO searchDto);
    Task<bool>                           UpdateStatus(long              id, int status);
    Task<bool>                           UpdateIsTop(long               id, int isTop);
    Task<ArticleDto?>                    GetArticleDto(long             id);
    Task                                 DeleteArticle(List<long>       ids);
    Task<List<InitSearchTitleVO>>        InitSearchByTitle();
    Task<List<HotArticleVO>>             ListHotArticle();
    Task<List<SearchArticleByContentVO>> SearchArticleByContent(string keyword);
}