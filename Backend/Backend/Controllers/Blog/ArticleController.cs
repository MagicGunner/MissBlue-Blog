using System.ComponentModel.DataAnnotations;
using Backend.Common.Attributes;
using Backend.Common.Results;
using Backend.Modules.Blog.Contracts.DTO;
using Backend.Modules.Blog.Contracts.IService;
using Backend.Modules.Blog.Contracts.VO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Backend.Controllers.Blog;

[ApiController]
[Route("api/article")]
[SwaggerTag("文章相关接口")]
public class ArticleController(IArticleService articleService) : ControllerBase {
    [HttpGet("search/init/title")]
    [AccessLimit(60, 5)]
    [SwaggerOperation(Summary = "初始化通过标题搜索文章", Description = "初始化通过标题搜索文章")]
    public async Task<ResponseResult<List<InitSearchTitleVO>>> InitSearchByTitle() {
        var data = await articleService.InitSearchByTitle();
        return ResponseHandler<List<InitSearchTitleVO>>.Create(data);
    }

    [HttpGet("search/by/content")]
    [AccessLimit(60, 5)]
    [SwaggerOperation(Summary = "内容搜索文章", Description = "内容搜索文章")]
    public async Task<ResponseResult<List<SearchArticleByContentVO>>> SearchByContent([FromQuery, Required(ErrorMessage = "文章内容不能为空")]
                                                                                      [StringLength(15, MinimumLength = 1, ErrorMessage = "文章搜索长度应在1-15之间")]
                                                                                      [SwaggerParameter(Description = "搜索文章内容", Required = true)]
                                                                                      string keyword) {
        var result = await articleService.SearchArticleByContent(keyword);
        return ResponseHandler<List<SearchArticleByContentVO>>.Create(result);
    }

    [HttpGet("hot")]
    [AccessLimit(60, 60)]
    [SwaggerOperation(Summary = "获取热门推荐文章", Description = "获取热门推荐文章")]
    public async Task<ResponseResult<List<HotArticleVO>>> Hot() {
        var result = await articleService.ListHotArticle();
        return ResponseHandler<List<HotArticleVO>>.Create(result);
    }

    [HttpGet("list")]
    [AccessLimit(60, 10)]
    [SwaggerOperation(Summary = "获取所有的文章列表", Description = "获取所有的文章列表")]
    public async Task<ResponseResult<PageVO<List<ArticleVO>>>> List([FromQuery, Required] [SwaggerParameter(Description = "页码", Required = true)] int pageNum,
                                                                    [FromQuery, Required] [SwaggerParameter(Description = "每页数量", Required = true)]
                                                                    int pageSize) {
        var page = await articleService.ListAll(pageNum, pageSize);
        return ResponseHandler<PageVO<List<ArticleVO>>>.Create(page);
    }

    [HttpGet("recommend")]
    [AccessLimit(60, 60)]
    [SwaggerOperation(Summary = "获取推荐的文章信息", Description = "获取推荐的文章信息")]
    public async Task<ResponseResult<List<RecommendArticleVO>>> Recommend() {
        var result = await articleService.ListRecommend();
        return ResponseHandler<List<RecommendArticleVO>>.Create(result);
    }

    [HttpGet("random")]
    [AccessLimit(60, 60)]
    [SwaggerOperation(Summary = "获取随机的文章信息", Description = "获取随机的文章信息")]
    public async Task<ResponseResult<List<RandomArticleVO>>> Random() {
        var result = await articleService.ListRandomArticle();
        return ResponseHandler<List<RandomArticleVO>>.Create(result);
    }

    [HttpGet("detail/{id}")]
    [AccessLimit(60, 60)]
    [SwaggerOperation(Summary = "获取文章详情", Description = "获取文章详情")]
    public async Task<ResponseResult<ArticleDetailVO>> Detail([FromRoute, Required] [SwaggerParameter("文章Id", Required = true)] long id) {
        var result = await articleService.GetDetail(id);
        return ResponseHandler<ArticleDetailVO>.Create(result);
    }

    [HttpGet("related/{categoryId}/{articleId}")]
    [AccessLimit(60, 60)]
    [SwaggerOperation(Summary = "相关文章信息", Description = "相关文章信息")]
    public async Task<ResponseResult<List<RelatedArticleVO>>> Related([FromRoute, Required] [SwaggerParameter("分类id", Required = true)] long categoryId,
                                                                      [FromRoute, Required] [SwaggerParameter("文章id", Required = true)]
                                                                      long articleId) {
        var result = await articleService.RelatedArticleList(categoryId, articleId);
        return ResponseHandler<List<RelatedArticleVO>>.Create(result);
    }

    /// <summary>
    /// 获取时间轴数据
    /// </summary>
    [HttpGet("timeLine")]
    [AccessLimit(60, 15)]
    [SwaggerOperation(Summary = "获取时间轴数据", Description = "获取时间轴数据")]
    public async Task<ResponseResult<List<TimeLineVO>>> TimeLine() {
        var result = await articleService.ListTimeLine();
        return ResponseHandler<List<TimeLineVO>>.Create(result);
    }


    [HttpGet("where/list/{typeId}")]
    [AccessLimit(60, 15)]
    [SwaggerOperation(Summary = "获取分类与标签下的文章", Description = "获取分类与标签下的文章")]
    public async Task<ResponseResult<List<CategoryArticleVO>>> ListCategoryArticle([FromRoute, Required] [SwaggerParameter("类型id", Required = true)] long typeId,
                                                                                   [FromQuery, Required] [SwaggerParameter("类型", Required = true)]
                                                                                   int type) {
        var result = await articleService.ListCategoryArticle(type, typeId);
        return ResponseHandler<List<CategoryArticleVO>>.Create(result);
    }


    [HttpGet("visit/{id}")]
    [AccessLimit(60, 60)]
    [SwaggerOperation(Summary = "文章访问量+1", Description = "文章访问量+1")]
    public async Task<ResponseResult<object>> Visit([FromRoute, Required] [SwaggerParameter("文章id", Required = true)] long id) =>
        ResponseHandler<object>.Create(await articleService.AddVisitCount(id));

    [HttpPost("upload/articleCover")]
    [Authorize(Policy = "blog:publish:article")]
    [AccessLimit(60, 5)]
    [SwaggerOperation(Summary = "上传文章封面", Description = "上传文章封面")]
    public async Task<ResponseResult<string>> UploadArticleCover(IFormFile articleCover) {
        var url = await articleService.UploadArticleCover(articleCover);
        return ResponseHandler<string>.Create(url);
    }

    [HttpPost("publish")]
    [Authorize(Policy = "blog:publish:article")]
    [AccessLimit(60, 30)]
    [SwaggerOperation(Summary = "发布文章", Description = "发布文章")]
    public async Task<ResponseResult<object>> Publish([FromBody, Required] ArticleDto articleDto) => ResponseHandler<object>.Create(await articleService.Publish(articleDto));


    [HttpGet("delete/articleCover")]
    [Authorize(Policy = "blog:publish:article")]
    [AccessLimit(60, 30)]
    [SwaggerOperation(Summary = "删除文章封面", Description = "删除文章封面")]
    public async Task<ResponseResult<object>> DeleteArticleCover([FromQuery, Required] [SwaggerParameter("文章封面", Required = true)] string articleCoverUrl) =>
        ResponseHandler<object>.Create(await articleService.DeleteArticleCover(articleCoverUrl));

    [Authorize(Policy = "blog:publish:article")]
    [HttpPost("upload/articleImage")]
    [AccessLimit(60, 30)]
    [SwaggerOperation(Summary = "上传文章图片", Description = "上传文章图片")]
    public async Task<ResponseResult<string>> UploadArticleImage(IFormFile articleImage) {
        var url = await articleService.UploadArticleImage(articleImage);
        return ResponseHandler<string>.Create(url);
    }

    [HttpGet("back/list")]
    [Authorize(Policy = "blog:article:list")]
    [AccessLimit(60, 30)]
    [SwaggerOperation(Summary = "获取所有的文章列表", Description = "获取所有的文章列表")]
    public async Task<ResponseResult<List<ArticleListVO>>> ListArticle() {
        var result = await articleService.ListArticle();
        return ResponseHandler<List<ArticleListVO>>.Create(result);
    }

    [HttpPost("back/search")]
    [Authorize(Policy = "blog:article:search")]
    [AccessLimit(60, 30)]
    [SwaggerOperation(Summary = "搜索文章列表", Description = "搜索文章列表")]
    public async Task<ResponseResult<List<ArticleListVO>>> SearchArticle([FromBody, Required] SearchArticleDTO dto) {
        var result = await articleService.SearchArticle(dto);
        return ResponseHandler<List<ArticleListVO>>.Create(result);
    }

    [HttpPost("back/update/status")]
    [Authorize(Policy = "blog:article:update")]
    [AccessLimit(60, 30)]
    [SwaggerOperation(Summary = "修改文章状态", Description = "修改文章状态")]
    public async Task<ResponseResult<object>> UpdateArticleStatus([FromQuery, Required] [SwaggerParameter("文章id", Required = true)] long id,
                                                                  [FromQuery, Required] [SwaggerParameter("状态", Required = true)]
                                                                  int status) =>
        ResponseHandler<object>.Create(await articleService.UpdateStatus(id, status));

    [HttpPost("back/update/isTop")]
    [Authorize(Policy = "blog:article:update")]
    [AccessLimit(60, 30)]
    [SwaggerOperation(Summary = "修改文章是否顶置", Description = "修改文章是否顶置")]
    public async Task<ResponseResult<object>> UpdateArticleIsTop([FromQuery, Required] [SwaggerParameter("文章id", Required = true)] long id,
                                                                 [FromQuery, Required] [SwaggerParameter("是否顶置", Required = true)]
                                                                 int isTop) =>
        ResponseHandler<object>.Create(await articleService.UpdateIsTop(id, isTop));

    [HttpGet("back/echo/{id}")]
    [Authorize(Policy = "blog:article:echo")]
    [AccessLimit(60, 30)]
    [SwaggerOperation(Summary = "回显文章数据", Description = "回显文章数据")]
    public async Task<ResponseResult<ArticleDto>> GetArticleEcho([FromRoute] long id) {
        var dto = await articleService.GetArticleDto(id);
        return ResponseHandler<ArticleDto>.Create(dto);
    }


    [HttpDelete("back/delete")]
    [Authorize(Policy = "blog:article:delete")]
    [AccessLimit(60, 30)]
    [SwaggerOperation(Summary = "删除文章", Description = "删除文章")]
    public async Task<ResponseResult<object>> DeleteArticle([FromBody, Required] List<long> ids) => ResponseHandler<object>.Create(await articleService.DeleteArticle(ids));
}