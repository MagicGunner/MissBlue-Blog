using System.ComponentModel.DataAnnotations;
using Backend.Common.Attributes;
using Backend.Common.Results;
using Backend.Modules.Blog.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Backend.Controllers.Blog;

[ApiController]
[Route("api/like")]
[SwaggerTag("文章相关接口")]
public class LikeController : ControllerBase {
    [HttpPost("auth/like")]
    [AccessLimit(60, 10)]
    [CheckBlacklist]
    [SwaggerOperation(Summary = "点赞", Description = "点赞")]
    public Task<ResponseResult<object>> Like([FromQuery] [Required] int type,
                                             [FromQuery] [Required] int typeId) {
        throw new NotImplementedException();
    }

    [HttpDelete("auth/like")]
    [AccessLimit(60, 10)]
    [CheckBlacklist]
    [SwaggerOperation(Summary = "取消点赞", Description = "取消点赞")]
    public Task<ResponseResult<object>> CancelLike([FromQuery] [Required] int type,
                                                   [FromQuery] [Required] int typeId) {
        throw new NotImplementedException();
    }

    [HttpGet("whether/like")]
    [AccessLimit(60, 60)]
    [SwaggerOperation(Summary = "是否已经点赞", Description = "是否已经点赞")]
    public Task<ResponseResult<List<Like>>> IsLike([FromQuery] [Required] int  type,
                                                   [FromQuery]            int? typeId) {
        throw new NotImplementedException();
    }
}