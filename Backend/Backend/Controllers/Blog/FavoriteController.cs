using System.ComponentModel.DataAnnotations;
using Backend.Common.Attributes;
using Backend.Common.Results;
using Backend.Modules.Blog.Contracts.DTO;
using Backend.Modules.Blog.Contracts.VO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Backend.Controllers.Blog;

[ApiController]
[Route("api/favorite")]
[SwaggerTag("收藏相关接口")]
public class FavoriteController : ControllerBase {
    [HttpPost("auth/favorite")]
    [AccessLimit(60, 10)]
    [CheckBlacklist]
    [SwaggerOperation(Summary = "收藏", Description = "收藏")]
    public Task<ResponseResult<object>> Favorite([FromQuery] [Required] int   type,
                                                 [FromQuery]            long? typeId) {
        throw new NotImplementedException();
    }

    [HttpDelete("auth/favorite")]
    [AccessLimit(60, 10)]
    [CheckBlacklist]
    [SwaggerOperation(Summary = "取消收藏", Description = "取消收藏")]
    public Task<ResponseResult<object>> CancelFavorite([FromQuery] [Required] int  type,
                                                       [FromQuery]            int? typeId) {
        throw new NotImplementedException();
    }

    [HttpGet("whether/favorite")]
    [AccessLimit(60, 60)]
    [SwaggerOperation(Summary = "是否已经收藏", Description = "是否已经收藏")]
    public Task<ResponseResult<bool>> IsFavorite([FromQuery] [Required] int  type,
                                                 [FromQuery]            int? typeId) {
        throw new NotImplementedException();
    }

    [HttpGet("back/list")]
    [AccessLimit(60, 30)]
    [Authorize(Policy = "blog:favorite:list")]
    [SwaggerOperation(Summary = "后台收藏列表", Description = "后台收藏列表")]
    public Task<ResponseResult<List<FavoriteListVO>>> BackList() {
        throw new NotImplementedException();
    }

    [HttpPost("back/search")]
    [AccessLimit(60, 30)]
    [Authorize(Policy = "blog:favorite:search")]
    [SwaggerOperation(Summary = "搜索后台收藏列表", Description = "搜索后台收藏列表")]
    public Task<ResponseResult<List<FavoriteListVO>>> BackList([FromBody] SearchFavoriteDTO searchDTO) {
        throw new NotImplementedException();
    }

    [HttpPost("back/isCheck")]
    [AccessLimit(60, 30)]
    [Authorize(Policy = "blog:favorite:isCheck")]
    [SwaggerOperation(Summary = "修改收藏是否通过", Description = "修改收藏是否通过")]
    public Task<ResponseResult<object>> IsCheck([FromBody] [Required] FavoriteIsCheckDTO favoriteIsCheckDTO) {
        throw new NotImplementedException();
    }

    [HttpDelete("back/delete")]
    [AccessLimit(60, 30)]
    [Authorize(Policy = "blog:favorite:delete")]
    [SwaggerOperation(Summary = "删除收藏", Description = "删除收藏")]
    public Task<ResponseResult<object>> Delete([FromBody] [Required] List<long> ids) {
        throw new NotImplementedException();
    }
}