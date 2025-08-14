using System.ComponentModel.DataAnnotations;
using Backend.Common.Attributes;
using Backend.Common.Results;
using Backend.Contracts.DTO;
using Backend.Contracts.VO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Backend.Controllers.Blog;

[ApiController]
[Route("api/loginLog")]
[SwaggerTag("登录日志相关接口")]
public class LoginLogController : ControllerBase {
    [HttpGet("list")]
    [AccessLimit(60, 30)]
    [Authorize(Policy = "system:log:login:list")]
    [SwaggerOperation(Summary = "显示所有登录日志", Description = "显示所有登录日志")]
    public Task<ResponseResult<List<LoginLogVO>>> GetLoginLogList() {
        throw new NotImplementedException();
    }

    [HttpPost("search")]
    [AccessLimit(60, 30)]
    [Authorize(Policy = "system:log:login:search")]
    [SwaggerOperation(Summary = "搜索登录日志", Description = "搜索登录日志")]
    public Task<ResponseResult<List<LoginLogVO>>> GetLoginLogSearch([FromBody] [Required] LoginLogDTO loginLogDTO) {
        throw new NotImplementedException();
    }

    [HttpDelete("delete")]
    [AccessLimit(60, 30)]
    [Authorize(Policy = "system:log:login:delete")]
    [SwaggerOperation(Summary = "删除/清空登录日志", Description = "删除/清空登录日志")]
    public Task<ResponseResult<object>> DeleteLoginLog([FromBody] [Required] LoginLogDeleteDTO deleteLoginLogDTO) {
        throw new NotImplementedException();
    }
}