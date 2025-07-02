using System.ComponentModel.DataAnnotations;
using Backend.Common.Results;
using Backend.Modules.Blog.Contracts.DTO;
using Backend.Modules.Blog.Contracts.IService;
using Backend.Modules.Blog.Contracts.VO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers.Blog;

[ApiController]
[Route("api/article")]
[Tags("文章相关接口")]
public class ArticleController(IArticleService articleService) : ControllerBase {
    /// <summary>
    /// 初始化通过标题搜索文章
    /// </summary>
    [HttpGet("search/init/title")]
    public async Task<ActionResult<PageVO<IEnumerable<InitSearchTitleVO>>>> InitSearchByTitle() {
        var data = await articleService.InitSearchByTitle();
        return Ok(new PageVO<IEnumerable<InitSearchTitleVO>> {
                                                                 Page = data,
                                                                 Total = data.Count
                                                             });
    }

    /// <summary>
    /// 内容搜索文章
    /// </summary>
    [HttpGet("search/by/content")]
    public async Task<ActionResult<IEnumerable<SearchArticleByContentVO>>> SearchByContent([FromQuery, Required, StringLength(15, MinimumLength = 1)] string content) {
        var result = await articleService.SearchArticleByContent(content);
        return Ok(result);
    }

    /// <summary>
    /// 获取热门推荐文章
    /// </summary>
    [HttpGet("hot")]
    public async Task<ActionResult<IEnumerable<HotArticleVO>>> Hot() {
        var result = await articleService.ListHotArticle();
        return Ok(result);
    }

    /// <summary>
    /// 获取所有文章（分页）
    /// </summary>
    [HttpGet("list")]
    public async Task<ResponseResult<PageVO<List<ArticleVO>>>> ListAll([FromQuery, Required] int pageNum,
                                                                       [FromQuery, Required] int pageSize) {
        var page = await articleService.ListAllArticle(pageNum, pageSize);
        return new ResponseResult<PageVO<List<ArticleVO>>>(true, page);
    }

    /// <summary>
    /// 获取推荐文章
    /// </summary>
    [HttpGet("recommend")]
    public async Task<ResponseResult<List<RecommendArticleVO>>> Recommend() {
        var result = await articleService.ListRecommendArticle();
        return new ResponseResult<List<RecommendArticleVO>>(true, result);
    }

    /// <summary>
    /// 获取随机文章
    /// </summary>
    [HttpGet("random")]
    public async Task<ResponseResult<List<RandomArticleVO>>> Random() {
        var result = await articleService.ListRandomArticle();
        return new ResponseResult<List<RandomArticleVO>>(true, result);
    }

    /// <summary>
    /// 获取文章详情
    /// </summary>
    [HttpGet("detail/{id}")]
    public async Task<ActionResult<ArticleDetailVO>> Detail([FromRoute] long id) {
        var detail = await articleService.GetArticleDetail(id);
        return Ok(detail);
    }

    /// <summary>
    /// 获取相关文章
    /// </summary>
    [HttpGet("related/{categoryId}/{articleId}")]
    public async Task<ActionResult<IEnumerable<RelatedArticleVO>>> Related([FromRoute] long categoryId,
                                                                           [FromRoute] long articleId) {
        var list = await articleService.RelatedArticleList(categoryId, articleId);
        return Ok(list);
    }

    /// <summary>
    /// 获取时间轴数据
    /// </summary>
    [HttpGet("timeLine")]
    public async Task<ActionResult<IEnumerable<TimeLineVO>>> TimeLine() {
        var list = await articleService.ListTimeLine();
        return Ok(list);
    }

    /// <summary>
    /// 获取分类或标签下的文章
    /// </summary>
    [HttpGet("where/list/{typeId}")]
    public async Task<ActionResult<IEnumerable<CategoryArticleVO>>> ListCategoryArticle([FromRoute]           long typeId,
                                                                                        [FromQuery, Required] int  type) {
        var list = await articleService.ListCategoryArticle(type, typeId);
        return Ok(list);
    }

    /// <summary>
    /// 文章访问量加 1
    /// </summary>
    [HttpGet("visit/{id}")]
    public async Task<IActionResult> Visit([FromRoute] long id) {
        await articleService.AddVisitCount(id);
        return NoContent();
    }

    /// <summary>
    /// 上传文章封面
    /// </summary>
    [Authorize(Policy = "blog:publish:article")]
    [HttpPost("upload/articleCover")]
    public async Task<ActionResult<string>> UploadArticleCover(IFormFile articleCover) {
        var url = await articleService.UploadArticleCover(articleCover);
        return Ok(url);
    }

    /// <summary>
    /// 发布文章
    /// </summary>
    [Authorize(Policy = "blog:publish:article")]
    [HttpPost("publish")]
    public async Task<IActionResult> Publish([FromBody, Required] ArticleDto articleDto) {
        await articleService.Publish(articleDto);
        return NoContent();
    }

    /// <summary>
    /// 删除文章封面
    /// </summary>
    [Authorize(Policy = "blog:publish:article")]
    [HttpGet("delete/articleCover")]
    public async Task<IActionResult> DeleteArticleCover([FromQuery, Required] string articleCoverUrl) {
        await articleService.DeleteArticleCover(articleCoverUrl);
        return NoContent();
    }

    /// <summary>
    /// 上传文章图片
    /// </summary>
    [Authorize(Policy = "blog:publish:article")]
    [HttpPost("upload/articleImage")]
    public async Task<ActionResult<string>> UploadArticleImage(IFormFile articleImage) {
        var url = await articleService.UploadArticleImage(articleImage);
        return Ok(url);
    }

    /// <summary>
    /// 后台：获取所有文章列表
    /// </summary>
    [Authorize(Policy = "blog:article:list")]
    [HttpGet("back/list")]
    public async Task<ActionResult<IEnumerable<ArticleListVO>>> ListArticle() {
        var list = await articleService.ListArticle();
        return Ok(list);
    }

    /// <summary>
    /// 后台：搜索文章
    /// </summary>
    [Authorize(Policy = "blog:article:search")]
    [HttpPost("back/search")]
    public async Task<ActionResult<IEnumerable<ArticleListVO>>> SearchArticle([FromBody, Required] SearchArticleDTO dto) {
        var list = await articleService.SearchArticle(dto);
        return Ok(list);
    }

    /// <summary>
    /// 后台：修改文章状态
    /// </summary>
    [Authorize(Policy = "blog:article:update")]
    [HttpPost("back/update/status")]
    public async Task<IActionResult> UpdateArticleStatus([FromQuery, Required] long id,
                                                         [FromQuery, Required] int  status) {
        await articleService.UpdateStatus(id, status);
        return NoContent();
    }

    /// <summary>
    /// 后台：修改文章置顶
    /// </summary>
    [Authorize(Policy = "blog:article:update")]
    [HttpPost("back/update/isTop")]
    public async Task<IActionResult> UpdateArticleIsTop([FromQuery, Required] long id,
                                                        [FromQuery, Required] bool isTop) {
        await articleService.UpdateIsTop(id, isTop);
        return NoContent();
    }

    /// <summary>
    /// 后台：回显文章数据
    /// </summary>
    [Authorize(Policy = "blog:article:echo")]
    [HttpGet("back/echo/{id}")]
    public async Task<ActionResult<ArticleDto>> GetArticleEcho([FromRoute] long id) {
        var dto = await articleService.GetArticleDto(id);
        return Ok(dto);
    }

    /// <summary>
    /// 后台：删除文章
    /// </summary>
    [Authorize(Policy = "blog:article:delete")]
    [HttpDelete("back/delete")]
    public async Task<IActionResult> DeleteArticle([FromBody, Required] List<long> ids) {
        await articleService.DeleteArticle(ids);
        return NoContent();
    }
}