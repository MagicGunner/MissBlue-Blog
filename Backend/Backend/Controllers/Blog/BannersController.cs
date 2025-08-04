using System.ComponentModel.DataAnnotations;
using Backend.Common.Attributes;
using Backend.Common.Results;
using Backend.Modules.Blog.Contracts.DTO;
using Backend.Modules.Blog.Contracts.IService;
using Backend.Modules.Blog.Contracts.VO;
using Backend.Modules.Blog.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Backend.Controllers.Blog;

[ApiController]
[Route("api/banners")]
public class BannersController(IBannersService bannersService) : ControllerBase {
    [HttpGet("list")]
    [AccessLimit(60, 25)]
    [SwaggerOperation(Summary = "前台获取所有前台首页Banner图片")]
    public async Task<List<string>> GetBanners() => await bannersService.GetBanners();

    [HttpGet("back/list")]
    [AccessLimit(60, 60)]
    [Authorize(Policy = "blog:banner:list")]
    [SwaggerOperation(Summary = "后台获取所有前台首页Banner图片", Description = "后台获取所有前台首页Banner图片")]
    public async Task<ResponseResult<List<BannersVO>>> BackGetBanners() {
        var result = await bannersService.BackGetBanners();
        return ResponseHandler<List<BannersVO>>.Create(result);
    }

    [HttpDelete("{id}")]
    [AccessLimit(60, 30)]
    [Authorize(Policy = "blog:banner:delete")]
    [SwaggerOperation(Summary = "删除前台首页Banner图片", Description = "删除前台首页Banner图片")]
    public async Task<ResponseResult<object>> Delete([FromRoute] [Required] long id) => ResponseHandler<object>.Create(await bannersService.RemoveBannerById(id));

    [HttpPost("upload/banner")]
    [AccessLimit(60, 30)]
    [Authorize(Policy = "blog:banner:add")]
    [SwaggerOperation(Summary = "添加前台首页Banner图片", Description = "添加前台首页Banner图片")]
    public async Task<ResponseResult<BannersVO?>> UploadArticleImage([Required] IFormFile bannerImage) {
        var result = await bannersService.UploadBannerImage(bannerImage);
        return ResponseHandler<BannersVO?>.Create(result.bannersVo, msg: result.msg);
    }

    [HttpPut("update/sort/order")]
    [AccessLimit(60, 30)]
    [Authorize(Policy = "blog:banner:update")]
    [SwaggerOperation(Summary = "更新前台首页Banner图片顺序", Description = "更新前台首页Banner图片顺序")]
    public async Task<ResponseResult<string>> UpdateSortOrder([Required] List<BannersDTO> banners) {
        var result = await bannersService.UpdateSortOrder(banners);
        return ResponseHandler<string>.Create(result);
    }
}