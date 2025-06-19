using Backend.Modules.Blog.Contracts.DTO;
using Backend.Modules.Blog.Contracts.VO;
using Microsoft.AspNetCore.Http;

namespace Backend.Modules.Blog.Contracts.Interface;

public interface IArticleService {
    Task<PageVO<List<ArticleVO>>>        ListAllArticleAsync(int pageNum, int pageSize);
    Task<List<RecommendArticleVO>>       ListRecommendArticleAsync();
    Task<List<RandomArticleVO>>          ListRandomArticleAsync();
    Task<ArticleDetailVO>                GetArticleDetailAsync(long   id);
    Task<List<RelatedArticleVO>>         RelatedArticleListAsync(long categoryId, long articleId);
    Task<List<TimeLineVO>>               ListTimeLineAsync();
    Task<List<CategoryArticleVO>>        ListCategoryArticleAsync(int      type, long typeId);
    Task                                 AddVisitCountAsync(long           id);
    Task<string>                         UploadArticleCoverAsync(IFormFile articleCover);
    Task                                 PublishAsync(ArticleDto           articleDto);
    Task                                 DeleteArticleCoverAsync(string    articleCoverUrl);
    Task<string>                         UploadArticleImageAsync(IFormFile articleImage);
    Task<List<ArticleListVO>>            ListArticleAsync();
    Task<List<ArticleListVO>>            SearchArticleAsync(SearchArticleDTO searchDto);
    Task                                 UpdateStatusAsync(long              id, int  status);
    Task                                 UpdateIsTopAsync(long               id, bool isTop);
    Task<ArticleDto>                     GetArticleDtoAsync(long             id);
    Task                                 DeleteArticleAsync(List<long>       ids);
    Task<List<InitSearchTitleVO>>        InitSearchByTitleAsync();
    Task<List<HotArticleVO>>             ListHotArticleAsync();
    Task<List<SearchArticleByContentVO>> SearchArticleByContentAsync(string content);
}