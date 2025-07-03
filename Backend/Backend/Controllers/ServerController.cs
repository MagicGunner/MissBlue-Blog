using Backend.Common.Attributes;
using Backend.Common.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Backend.Controllers;

[ApiController]
[Route("api/monitor/server")]
[SwaggerTag("服务监控")]
public class ServerController : ControllerBase {
    [HttpGet]
    [Authorize(Policy = "monitor:server:list")]
    [AccessLimit(60, 30)]
    [SwaggerOperation(Summary = "获取服务监控数据", Description = "获取服务监控数据")]
    public Task<ResponseResult<object>> GetInfo() {
        throw new NotImplementedException();
    }
}