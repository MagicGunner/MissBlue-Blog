using Backend.Common.Attributes;
using Backend.Common.Results;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Backend.Controllers.Blog;

[ApiController]
[Route("api/oauth")]
[SwaggerTag("第三方登录")]
public class OauthController : ControllerBase {
    [HttpGet("gitee/render")]
    [AccessLimit(60, 5)]
    [SwaggerOperation(Summary = "Gitee登录", Description = "Gitee登录")]
    public Task<ResponseResult<object>> GiteeRenderAuth() {
        throw new NotImplementedException();
    }

    [HttpGet("gitee/callback")]
    [AccessLimit(60, 5)]
    [SwaggerOperation(Summary = "Gitee登录回调", Description = "Gitee登录回调")]
    public Task<ResponseResult<object>> GiteeLogin([FromQuery] string code, [FromQuery] string state) {
        throw new NotImplementedException();
    }

    [HttpGet("github/render")]
    [AccessLimit(60, 5)]
    [SwaggerOperation(Summary = "Github登录", Description = "Github登录")]
    public Task<ResponseResult<object>> GithubRenderAuth() {
        throw new NotImplementedException();
    }

    [HttpGet("github/callback")]
    [AccessLimit(60, 5)]
    [SwaggerOperation(Summary = "Github登录回调", Description = "Github登录回调")]
    public Task<ResponseResult<object>> GithubLogin([FromQuery] string code, [FromQuery] string state) {
        throw new NotImplementedException();
    }
}