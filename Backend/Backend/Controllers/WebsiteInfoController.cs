using System.ComponentModel.DataAnnotations;
using Backend.Common.Attributes;
using Backend.Common.Results;
using Backend.Contracts.DTO;
using Backend.Contracts.IService;
using Backend.Contracts.VO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Backend.Controllers;

[ApiController]
[Route("api/websiteInfo")]
[SwaggerTag("网站基本信息")]
public class WebsiteInfoController(IWebsiteInfoService websiteInfoService) : ControllerBase {
    [HttpPost("upload/avatar")]
    [AccessLimit(60, 5)] // 👈 限流参数
    [Authorize(Policy = "blog:update:websiteInfo")]
    [SwaggerOperation(Summary = "上传站长头像", Description = "上传站长头像")]
    public Task<ResponseResult<string>> UploadAvatar(IFormFile avatar) {
        throw new NotImplementedException();
    }

    [HttpPost("upload/background")]
    [Authorize(Policy = "blog:update:websiteInfo")]
    [AccessLimit(60, 5)] // ✅ 自定义限流
    [SwaggerOperation(Summary = "上传站长资料卡背景", Description = "上传站长资料卡背景")]
    [Consumes("multipart/form-data")] // ✅ 文件上传必须添加
    public Task<ResponseResult<string>> UploadBackground(IFormFile background) {
        // var result = await _websiteInfoService.UploadImageInsertOrUpdateAsync(UploadEnum.WebsiteInfoBackground, background, 1);
        //
        // return ResponseResult<string>.Success(result);
        throw new NotImplementedException();
    }

    [HttpGet]
    [AccessLimit(60, 30)]
    [Authorize(Policy = "blog:get:websiteInfo")]
    [SwaggerOperation(Summary = "查看网站信息-后端", Description = "查看网站信息-后端")]
    public Task<ResponseResult<object>> SelectWebsiteInfo() {
        throw new NotImplementedException();
    }

    [HttpGet("front")]
    [AccessLimit(60, 30)]
    [SwaggerOperation(Summary = "查看网站信息-前端", Description = "查看网站信息-前端")]
    public async Task<ResponseResult<WebsiteInfoVO>> GetWebsiteInfoFront() => ResponseHandler<WebsiteInfoVO>.Create(await websiteInfoService.GetWebsiteInfo());

    [HttpPost("stationmaster")]
    [SwaggerOperation(Summary = "修改或创建站长信息", Description = "修改或创建站长信息")]
    public ResponseResult<object> UpdateStationmasterInfo([FromBody] StationmasterInfoDTO stationmasterInfoDto) {
        throw new NotImplementedException();
    }

    [HttpPost("webInfo")]
    [SwaggerOperation(Summary = "修改或创建网站信息", Description = "修改或创建网站信息")]
    public Stack<ResponseResult<object>> UpdateWebsiteInfo([FromBody] WebsiteInfoDTO websiteInfoDto) {
        throw new NotImplementedException();
    }
}