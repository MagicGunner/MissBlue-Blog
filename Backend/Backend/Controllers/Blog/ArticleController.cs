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
        return new ResponseResult<List<InitSearchTitleVO>>(data.Count > 0, data);
    }

    [HttpGet("search/by/content")]
    [AccessLimit(60, 5)]
    [SwaggerOperation(Summary = "内容搜索文章", Description = "内容搜索文章")]
    public async Task<ResponseResult<List<SearchArticleByContentVO>>> SearchByContent([FromQuery, Required(ErrorMessage = "文章内容不能为空")]
                                                                                      [StringLength(15, MinimumLength = 1, ErrorMessage = "文章搜索长度应在1-15之间")]
                                                                                      [SwaggerParameter(Description = "搜索文章内容", Required = true)]
                                                                                      string keyword) {
        var result = await articleService.SearchArticleByContent(keyword);
        return new ResponseResult<List<SearchArticleByContentVO>>(result.Count > 0, result);
    }

    [HttpGet("hot")]
    [AccessLimit(60, 60)]
    [SwaggerOperation(Summary = "获取热门推荐文章", Description = "获取热门推荐文章")]
    public Task<ResponseResult<List<HotArticleVO>>> Hot() {
        // var result = await articleService.ListHotArticle();
        // return Ok(result);
        throw new NotImplementedException();
    }

    [HttpGet("list")]
    [AccessLimit(60, 10)]
    [SwaggerOperation(Summary = "获取所有的文章列表", Description = "获取所有的文章列表")]
    public async Task<ResponseResult<PageVO<List<ArticleVO>>>> List([FromQuery, Required] [SwaggerParameter(Description = "页码", Required = true)] int pageNum,
                                                                    [FromQuery, Required] [SwaggerParameter(Description = "每页数量", Required = true)]
                                                                    int pageSize) {
        var page = await articleService.ListAll(pageNum, pageSize);
        return new ResponseResult<PageVO<List<ArticleVO>>>(true, page);
    }

    [HttpGet("recommend")]
    [AccessLimit(60, 60)]
    [SwaggerOperation(Summary = "获取推荐的文章信息", Description = "获取推荐的文章信息")]
    public async Task<ResponseResult<List<RecommendArticleVO>>> Recommend() {
        var result = await articleService.ListRecommend();
        return new ResponseResult<List<RecommendArticleVO>>(true, result);
    }

    [HttpGet("random")]
    [AccessLimit(60, 60)]
    [SwaggerOperation(Summary = "获取随机的文章信息", Description = "获取随机的文章信息")]
    public async Task<ResponseResult<List<RandomArticleVO>>> Random() {
        var result = await articleService.ListRandomArticle();
        return new ResponseResult<List<RandomArticleVO>>(true, result);
    }

    [HttpGet("detail/{id}")]
    [AccessLimit(60, 60)]
    [SwaggerOperation(Summary = "获取文章详情", Description = "获取文章详情")]
    public async Task<ResponseResult<ArticleDetailVO>> Detail([FromRoute, Required] [SwaggerParameter("文章Id", Required = true)] long id) {
        var result = await articleService.GetDetail(id);
        return new ResponseResult<ArticleDetailVO>(result != null, result);
    }

    [HttpGet("related/{categoryId}/{articleId}")]
    [AccessLimit(60, 60)]
    [SwaggerOperation(Summary = "相关文章信息", Description = "相关文章信息")]
    public Task<ResponseResult<List<RelatedArticleVO>>> Related([FromRoute, Required] [SwaggerParameter("分类id", Required = true)] long categoryId,
                                                                [FromRoute, Required] [SwaggerParameter("文章id", Required = true)]
                                                                long articleId) {
        // var list = await articleService.RelatedArticleList(categoryId, articleId);
        // return Ok(list);
        throw new NotImplementedException();
    }

    /// <summary>
    /// 获取时间轴数据
    /// </summary>
    [HttpGet("timeLine")]
    [AccessLimit(60, 15)]
    [SwaggerOperation(Summary = "获取时间轴数据", Description = "获取时间轴数据")]
    public Task<ResponseResult<List<TimeLineVO>>> TimeLine() {
        // var list = await articleService.ListTimeLine();
        // return Ok(list);
        throw new NotImplementedException();
    }


    [HttpGet("where/list/{typeId}")]
    [AccessLimit(60, 15)]
    [SwaggerOperation(Summary = "获取分类与标签下的文章", Description = "获取分类与标签下的文章")]
    public Task<ResponseResult<List<CategoryArticleVO>>> ListCategoryArticle([FromRoute, Required] [SwaggerParameter("类型id", Required = true)] long typeId,
                                                                             [FromQuery, Required] [SwaggerParameter("类型", Required = true)]
                                                                             int type) {
        // var list = await articleService.ListCategoryArticle(type, typeId);
        // return Ok(list);
        throw new NotImplementedException();
    }


    [HttpGet("visit/{id}")]
    [AccessLimit(60, 60)]
    [SwaggerOperation(Summary = "文章访问量+1", Description = "文章访问量+1")]
    public Task<ResponseResult<object>> Visit([FromRoute, Required] [SwaggerParameter("文章id", Required = true)] long id) {
        // await articleService.AddVisitCount(id);
        throw new NotImplementedException();
    }

    [HttpPost("upload/articleCover")]
    [Authorize(Policy = "blog:publish:article")]
    [AccessLimit(60, 5)]
    [SwaggerOperation(Summary = "上传文章封面", Description = "上传文章封面")]
    public Task<ResponseResult<string>> UploadArticleCover(IFormFile articleCover) {
        // var url = await articleService.UploadArticleCover(articleCover);
        // return Ok(url);
        throw new NotImplementedException();
    }

    [HttpPost("publish")]
    [Authorize(Policy = "blog:publish:article")]
    [AccessLimit(60, 30)]
    [SwaggerOperation(Summary = "发布文章", Description = "发布文章")]
    public Task<ResponseResult<object>> Publish([FromBody, Required] ArticleDto articleDto) {
        // await articleService.Publish(articleDto);
        // return NoContent();
        throw new NotImplementedException();
    }


    [HttpGet("delete/articleCover")]
    [Authorize(Policy = "blog:publish:article")]
    [AccessLimit(60, 30)]
    [SwaggerOperation(Summary = "删除文章封面", Description = "删除文章封面")]
    public Task<ResponseResult<object>> DeleteArticleCover([FromQuery, Required] [SwaggerParameter("文章封面", Required = true)] string articleCoverUrl) {
        // await articleService.DeleteArticleCover(articleCoverUrl);
        // return NoContent();
        throw new NotImplementedException();
    }

    [Authorize(Policy = "blog:publish:article")]
    [HttpPost("upload/articleImage")]
    [AccessLimit(60, 30)]
    [SwaggerOperation(Summary = "上传文章图片", Description = "上传文章图片")]
    public Task<ActionResult<string>> UploadArticleImage(IFormFile articleImage) {
        // var url = await articleService.UploadArticleImage(articleImage);
        // return Ok(url);
        throw new NotImplementedException();
    }

    [HttpGet("back/list")]
    [Authorize(Policy = "blog:article:list")]
    [AccessLimit(60, 30)]
    [SwaggerOperation(Summary = "获取所有的文章列表", Description = "获取所有的文章列表")]
    public Task<ResponseResult<List<ArticleListVO>>> ListArticle() {
        // var list = await articleService.ListArticle();
        // return Ok(list);
        throw new NotImplementedException();
    }

    [HttpPost("back/search")]
    [Authorize(Policy = "blog:article:search")]
    [AccessLimit(60, 30)]
    [SwaggerOperation(Summary = "搜索文章列表", Description = "搜索文章列表")]
    public Task<ResponseResult<List<ArticleListVO>>> SearchArticle([FromBody, Required] SearchArticleDTO dto) {
        // var list = await articleService.SearchArticle(dto);
        // return Ok(list);
        throw new NotImplementedException();
    }

    [HttpPost("back/update/status")]
    [Authorize(Policy = "blog:article:update")]
    [AccessLimit(60, 30)]
    [SwaggerOperation(Summary = "修改文章状态", Description = "修改文章状态")]
    public Task<ResponseResult<object>> UpdateArticleStatus([FromQuery, Required] [SwaggerParameter("文章id", Required = true)] long id,
                                                            [FromQuery, Required] [SwaggerParameter("状态", Required = true)]
                                                            int status) {
        // await articleService.UpdateStatus(id, status);
        // return NoContent();
        throw new NotImplementedException();
    }

    [HttpPost("back/update/isTop")]
    [Authorize(Policy = "blog:article:update")]
    [AccessLimit(60, 30)]
    [SwaggerOperation(Summary = "修改文章是否顶置", Description = "修改文章是否顶置")]
    public Task<ResponseResult<object>> UpdateArticleIsTop([FromQuery, Required] [SwaggerParameter("文章id", Required = true)] long id,
                                                           [FromQuery, Required] [SwaggerParameter("是否顶置", Required = true)]
                                                           bool isTop) {
        // await articleService.UpdateIsTop(id, isTop);
        // return NoContent();
        throw new NotImplementedException();
    }

    [HttpGet("back/echo/{id}")]
    [Authorize(Policy = "blog:article:echo")]
    [AccessLimit(60, 30)]
    [SwaggerOperation(Summary = "回显文章数据", Description = "回显文章数据")]
    public Task<ResponseResult<ArticleDto>> GetArticleEcho([FromRoute] long id) {
        // var dto = await articleService.GetArticleDto(id);
        // return Ok(dto);
        throw new NotImplementedException();
    }


    [HttpDelete("back/delete")]
    [Authorize(Policy = "blog:article:delete")]
    [AccessLimit(60, 30)]
    [SwaggerOperation(Summary = "删除文章", Description = "删除文章")]
    public Task<ResponseResult<object>> DeleteArticle([FromBody, Required] List<long> ids) {
        // await articleService.DeleteArticle(ids);
        // return NoContent();
        throw new NotImplementedException();
    }
}