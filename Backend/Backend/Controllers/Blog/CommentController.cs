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
[Route("api/comment")]
[SwaggerTag("评论相关接口")]
public class CommentController(ICommentService commentService) : ControllerBase {
    [HttpGet("getComment")]
    [AllowAnonymous]
    [AccessLimit(60, 60)]
    [SwaggerOperation(Summary = "获取评论", Description = "获取评论")]
    public async Task<ResponseResult<PageVO<List<ArticleCommentVO>>>> GetComment([FromQuery, Required(ErrorMessage = "Type必填")] int type,
                                                                                 [FromQuery, Required(ErrorMessage = "TypeId必填")]
                                                                                 int typeId,
                                                                                 [FromQuery, Required(ErrorMessage = "PageNum必填"), Range(1, int.MaxValue, ErrorMessage = "页码必须大于0")]
                                                                                 int pageNum,
                                                                                 [FromQuery, Required(ErrorMessage = "PageSize必填"), Range(1, 100, ErrorMessage = "每页数量必须在1-100之间")]
                                                                                 int pageSize) {
        var result = await commentService.GetComment(type, typeId, pageNum, pageSize);
        return new ResponseResult<PageVO<List<ArticleCommentVO>>>(result.Total > 0, result);
    }

    [HttpPost("auth/add/comment")]
    [AllowAnonymous] // 需要 CheckBlacklist 替代方案时移除
    [AccessLimit(60, 10)]
    [SwaggerOperation(Summary = "用户添加评论", Description = "用户添加评论")]
    public Task<ResponseResult<string>> AddComment([FromBody] [Required] UserCommentDTO userCommentDto) {
        throw new NotImplementedException();
    }

    [HttpGet("back/list")]
    [Authorize(Policy = "blog:comment:list")]
    [AccessLimit(60, 30)]
    [SwaggerOperation(Summary = "后台评论列表", Description = "后台评论列表")]
    public Task<ResponseResult<List<CommentListVO>>> BackList() {
        throw new NotImplementedException();
    }

    [HttpPost("back/search")]
    [Authorize(Policy = "blog:comment:search")]
    [AccessLimit(60, 30)]
    [SwaggerOperation(Summary = "搜索后台评论列表", Description = "搜索后台评论列表")]
    public Task<ResponseResult<List<CommentListVO>>> BackSearch([FromBody] SearchCommentDTO searchCommentDto) {
        throw new NotImplementedException();
    }

    [HttpPost("back/isCheck")]
    [Authorize(Policy = "blog:comment:isCheck")]
    [AccessLimit(60, 30)]
    [SwaggerOperation(Summary = "修改评论是否通过", Description = "修改评论是否通过")]
    public Task<ResponseResult<object>> IsCheck([FromBody] [Required] CommentIsCheckDTO commentIsCheckDto) {
        throw new NotImplementedException();
    }

    [HttpDelete("back/delete/{id}")]
    [Authorize(Policy = "blog:comment:delete")]
    [AccessLimit(60, 30)]
    [SwaggerOperation(Summary = "删除评论", Description = "删除评论")]
    public Task<ResponseResult<object>> Delete([FromRoute] [Required] long id) {
        throw new NotImplementedException();
    }
}