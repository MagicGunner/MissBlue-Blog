using System.ComponentModel.DataAnnotations;
using Backend.Common.Attributes;
using Backend.Common.Results;
using Backend.Modules.Blog.Contracts.IService;
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
    public Task<ResponseResult<List<Banners>>> BackGetBanners() {
        throw new NotImplementedException();
    }

    [HttpDelete("{id}")]
    [AccessLimit(60, 30)]
    [Authorize(Policy = "blog:banner:delete")]
    [SwaggerOperation(Summary = "删除前台首页Banner图片", Description = "删除前台首页Banner图片")]
    public Task<ResponseResult<string>> Delete([FromRoute] [Required] long id) {
        throw new NotImplementedException();
    }

    [HttpPost("upload/banner")]
    [AccessLimit(60, 30)]
    [Authorize(Policy = "blog:banner:add")]
    [SwaggerOperation(Summary = "添加前台首页Banner图片", Description = "添加前台首页Banner图片")]
    public Task<ResponseResult<Banners>> UploadArticleImage([Required] IFormFile bannerImage) {
        throw new NotImplementedException();
    }

    [HttpPut("update/sort/order")]
    [AccessLimit(60, 30)]
    [Authorize(Policy = "blog:banner:update")]
    [SwaggerOperation(Summary = "更新前台首页Banner图片顺序", Description = "更新前台首页Banner图片顺序")]
    public Task<ResponseResult<string>> UpdateSortOrder([Required] List<Banners> banners) {
        throw new NotImplementedException();
    }
}