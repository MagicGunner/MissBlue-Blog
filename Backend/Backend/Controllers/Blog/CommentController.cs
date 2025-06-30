using System.ComponentModel.DataAnnotations;
using Backend.Common.Results;
using Backend.Modules.Blog.Contracts.DTO;
using Backend.Modules.Blog.Contracts.IService;
using Backend.Modules.Blog.Contracts.VO;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers.Blog;

[ApiController]
[Route("api/comment")]
[Tags("评论")]
public class CommentController(ICommentService commentService) : ControllerBase {
    [HttpPost("/auth/add/comment")]
    public Task<ResponseResult<string>> AddComment([FromBody] [Required] UserCommentDTO userCommentDto) {
        throw new NotImplementedException();
    }

    [HttpGet("getComment")]
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
    
    
}