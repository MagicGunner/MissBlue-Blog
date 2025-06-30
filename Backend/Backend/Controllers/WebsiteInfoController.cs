using Backend.Common.Results;
using Backend.Modules.Blog.Contracts.IService;
using Backend.Modules.Blog.Contracts.VO;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[ApiController]
[Route("websiteInfo")]
public class WebsiteInfoController(IWebsiteInfoService websiteInfoService) : ControllerBase {
    /// <summary>
    /// 获取网站信息（前端）
    /// </summary>
    [HttpGet("front")]
    public async Task<ResponseResult<WebsiteInfoVO>> GetWebsiteInfoFront() => new(true, await websiteInfoService.GetWebsiteInfo());
}