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
[Route("api/log")]
[SwaggerTag("操作日志相关接口")]
public class LogController : ControllerBase {
    [HttpGet("list/{current}/{pageSize}")]
    [AccessLimit(60, 30)]
    [Authorize(Policy = "system:log:list")]
    [SwaggerOperation(Summary = "显示所有操作日志", Description = "显示所有操作日志")]
    public Task<ResponseResult<PageVO<object>>> GetLogList([FromRoute] [Required] long current, [FromRoute] [Required] long pageSize) {
        throw new NotImplementedException();
    }

    [HttpPost("search")]
    [AccessLimit(60, 30)]
    [Authorize(Policy = "system:log:search")]
    [SwaggerOperation(Summary = "搜索操作日志", Description = "搜索操作日志")]
    public Task<ResponseResult<PageVO<object>>> GetLogSearch([FromBody] [Required] LogDTO logDTO) {
        throw new NotImplementedException();
    }

    [HttpDelete("delete")]
    [AccessLimit(60, 30)]
    [Authorize(Policy = "system:log:delete")]
    [SwaggerOperation(Summary = "删除/清空操作日志", Description = "删除/清空操作日志")]
    public Task<ResponseResult<object>> DeleteLog([FromBody] [Required] LogDeleteDTO logDeleteDTO) {
        throw new NotImplementedException();
    }
}