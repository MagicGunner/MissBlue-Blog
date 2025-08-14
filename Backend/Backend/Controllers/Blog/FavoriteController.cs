using System.ComponentModel.DataAnnotations;
using Backend.Common.Attributes;
using Backend.Common.Results;
using Backend.Contracts.DTO;
using Backend.Contracts.IService;
using Backend.Contracts.VO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Backend.Controllers.Blog;

[ApiController]
[Route("api/favorite")]
[SwaggerTag("收藏相关接口")]
public class FavoriteController(IFavoriteService favoriteService) : ControllerBase {
    [HttpPost("auth/favorite")]
    [AccessLimit(60, 10)]
    [CheckBlacklist]
    [SwaggerOperation(Summary = "收藏", Description = "收藏")]
    public async Task<ResponseResult<object>> SetFavorited([FromQuery] [Required] int  type,
                                                           [FromQuery] [Required] long typeId) =>
        ResponseHandler<object>.Create(await favoriteService.SetFavorited(type, typeId));

    [HttpDelete("auth/favorite")]
    [AccessLimit(60, 10)]
    [CheckBlacklist]
    [SwaggerOperation(Summary = "取消收藏", Description = "取消收藏")]
    public async Task<ResponseResult<object>> UnSetFavorited([FromQuery] [Required] int type,
                                                             [FromQuery] [Required] int typeId) =>
        ResponseHandler<object>.Create(await favoriteService.UnSetFavorited(type, typeId));

    [HttpGet("whether/favorite")]
    [AccessLimit(60, 60)]
    [SwaggerOperation(Summary = "是否已经收藏", Description = "是否已经收藏")]
    public async Task<ResponseResult<bool>> IsFavorite([FromQuery] [Required] int type,
                                                       [FromQuery] [Required] int typeId) =>
        ResponseHandler<bool>.Create(await favoriteService.IsFavorited(type, typeId));

    [HttpGet("back/list")]
    [AccessLimit(60, 30)]
    [Authorize(Policy = "blog:favorite:list")]
    [SwaggerOperation(Summary = "后台收藏列表", Description = "后台收藏列表")]
    public async Task<ResponseResult<List<FavoriteListVO>>> BackList() {
        var result = await favoriteService.GetBackList(null);
        return ResponseHandler<List<FavoriteListVO>>.Create(result);
    }

    [HttpPost("back/search")]
    [AccessLimit(60, 30)]
    [Authorize(Policy = "blog:favorite:search")]
    [SwaggerOperation(Summary = "搜索后台收藏列表", Description = "搜索后台收藏列表")]
    public async Task<ResponseResult<List<FavoriteListVO>>> BackList([FromBody] SearchFavoriteDTO searchDTO) {
        var result = await favoriteService.GetBackList(searchDTO);
        return ResponseHandler<List<FavoriteListVO>>.Create(result);
    }

    [HttpPost("back/isCheck")]
    [AccessLimit(60, 30)]
    [Authorize(Policy = "blog:favorite:isCheck")]
    [SwaggerOperation(Summary = "修改收藏是否通过", Description = "修改收藏是否通过")]
    public async Task<ResponseResult<object>> SetChecked([FromBody] [Required] FavoriteIsCheckDTO favoriteIsCheckDto) =>
        ResponseHandler<object>.Create(await favoriteService.SetChecked(favoriteIsCheckDto));

    [HttpDelete("back/delete")]
    [AccessLimit(60, 30)]
    [Authorize(Policy = "blog:favorite:delete")]
    [SwaggerOperation(Summary = "删除收藏", Description = "删除收藏")]
    public async Task<ResponseResult<object>> Delete([FromBody] [Required] List<long> ids) => ResponseHandler<object>.Create(await favoriteService.Delete(ids));
}