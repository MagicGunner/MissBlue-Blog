using System.ComponentModel.DataAnnotations;
using Backend.Common.Attributes;
using Backend.Common.Results;
using Backend.Modules.Blog.Contracts.IService;
using Backend.Modules.Blog.Contracts.VO;
using Backend.Modules.Blog.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Backend.Controllers.Blog;

[ApiController]
[Route("api/like")]
[SwaggerTag("文章相关接口")]
public class LikeController(ILikeService likeService) : ControllerBase {
    [HttpPost("auth/like")]
    [AccessLimit(60, 10)]
    [CheckBlacklist]
    [SwaggerOperation(Summary = "点赞", Description = "点赞")]
    public async Task<ResponseResult<object>> SetLiked([FromQuery] [Required] int type,
                                                       [FromQuery] [Required] int typeId) =>
        ResponseHandler<object>.Create(await likeService.SetLiked(type, typeId));
        

    [HttpDelete("auth/like")]
    [AccessLimit(60, 10)]
    [CheckBlacklist]
    [SwaggerOperation(Summary = "取消点赞", Description = "取消点赞")]
    public async Task<ResponseResult<object>> UnSetLiked([FromQuery] [Required] int type,
                                                         [FromQuery] [Required] int typeId) =>
        ResponseHandler<object>.Create(await likeService.UnSetLiked(type, typeId));

    [HttpGet("whether/like")]
    [AccessLimit(60, 60)]
    [SwaggerOperation(Summary = "是否已经点赞", Description = "是否已经点赞")]
    public async Task<ResponseResult<List<LikeVo>>> IsLike([FromQuery] [Required] int   type,
                                                           [FromQuery]            long? typeId) {
        var result = await likeService.IsLike(type, typeId);
        return ResponseHandler<List<LikeVo>>.Create(result);
    }
}