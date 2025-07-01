using AutoMapper;
using Backend.Application.Service;
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
    public Task<PageVO<List<ArticleVO>>> ListAllArticle(int pageNum, int pageSize) {
        throw new NotImplementedException();
    }

    public Task<List<RecommendArticleVO>> ListRecommendArticle() {
        throw new NotImplementedException();
    }

    public Task<List<RandomArticleVO>> ListRandomArticle() {
        throw new NotImplementedException();
    }

    public Task<ArticleDetailVO> GetArticleDetail(long id) {
        throw new NotImplementedException();
    }

    public Task<List<RelatedArticleVO>> RelatedArticleList(long categoryId, long articleId) {
        throw new NotImplementedException();
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
}