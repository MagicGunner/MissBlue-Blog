using Backend.Modules.Blog.Contracts.DTO;
using Backend.Modules.Blog.Contracts.Interface;
using Backend.Modules.Blog.Contracts.VO;
using Microsoft.AspNetCore.Http;

namespace Backend.Modules.Blog.Application.Service;

public class ArticleService : IArticleService {
    public Task<PageVO<List<ArticleVO>>> ListAllArticleAsync(int pageNum, int pageSize) {
        throw new NotImplementedException();
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

    public Task PublishAsync(ArticleDto articleDto) {
        throw new NotImplementedException();
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