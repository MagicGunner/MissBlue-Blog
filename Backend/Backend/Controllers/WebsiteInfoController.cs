using System.ComponentModel.DataAnnotations;
using Backend.Common.Attributes;
using Backend.Common.Results;
using Backend.Modules.Blog.Contracts.IService;
using Backend.Modules.Blog.Contracts.VO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Backend.Controllers;

[ApiController]
[Route("api/websiteInfo")]
[SwaggerTag("网站基本信息")]
public class WebsiteInfoController(IWebsiteInfoService websiteInfoService) : ControllerBase {
    // [HttpPost("upload/avatar")]
    // [AccessLimit(60, 5)] // 👈 限流参数
    // [Authorize(Policy = "blog:update:websiteInfo")]
    // [SwaggerOperation(Summary = "上传站长头像", Description = "上传站长头像")]
    // public Task<ResponseResult<string>> UploadAvatar([FromForm] [Required] IFormFile avatar) {
    //     throw new NotImplementedException();
    // }

    // [HttpPost("upload/background")]
    // [Authorize(Policy = "blog:update:websiteInfo")]
    // [AccessLimit(60, 5)] // ✅ 自定义限流
    // [SwaggerOperation(Summary = "上传站长资料卡背景", Description = "上传站长资料卡背景")]
    // [Consumes("multipart/form-data")] // ✅ 文件上传必须添加
    // public Task<ResponseResult<string>> UploadBackground(
    //     [FromForm] [Required] IFormFile background) {
    //     // var result = await _websiteInfoService.UploadImageInsertOrUpdateAsync(UploadEnum.WebsiteInfoBackground, background, 1);
    //     //
    //     // return ResponseResult<string>.Success(result);
    //     throw new NotImplementedException();
    // }
    
    [HttpGet("front")]
    public async Task<ResponseResult<WebsiteInfoVO>> GetWebsiteInfoFront() => new(true, await websiteInfoService.GetWebsiteInfo());
}