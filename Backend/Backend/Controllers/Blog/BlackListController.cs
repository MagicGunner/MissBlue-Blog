using System.ComponentModel.DataAnnotations;
using Backend.Common.Attributes;
using Backend.Common.Results;
using Backend.Modules.Blog.Contracts.DTO;
using Backend.Modules.Blog.Contracts.IService;
using Backend.Modules.Blog.Contracts.VO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Backend.Controllers.Blog;

[ApiController]
[Route("api/blackList")]
[SwaggerTag("黑名单相关接口")]
public class BlackListController(IBlackListService blackListService) : ControllerBase {
    [HttpPost("add")]
    [AccessLimit(60, 30)]
    [Authorize(Policy = "blog:black:add")]
    [SwaggerOperation(Summary = "添加黑名单", Description = "添加黑名单")]
    public Task<ResponseResult<object>> AddBlackList([FromBody] [Required] AddBlackListDTO addBlackListDTO) {
        throw new NotImplementedException();
    }

    [HttpPut("update")]
    [AccessLimit(60, 30)]
    [Authorize(Policy = "blog:black:update")]
    [SwaggerOperation(Summary = "修改黑名单", Description = "修改黑名单")]
    public Task<ResponseResult<object>> UpdateBlackList([FromBody] [Required] UpdateBlackListDTO updateBlackListDTO) {
        throw new NotImplementedException();
    }

    [HttpPost("getBlackListing")]
    [AccessLimit(60, 30)]
    [Authorize(Policy = "blog:black:select")]
    [SwaggerOperation(Summary = "查询黑名单", Description = "查询黑名单")]
    public Task<ResponseResult<List<BlackListVO>>> GetBlackList([FromBody] SearchBlackListDTO? searchBlackListDTO) {
        throw new NotImplementedException();
    }

    [HttpDelete("delete")]
    [AccessLimit(60, 30)]
    [Authorize(Policy = "blog:black:delete")]
    [SwaggerOperation(Summary = "删除黑名单", Description = "删除黑名单")]
    public Task<ResponseResult<object>> DeleteBlackList([FromBody] [Required] List<long> ids) {
        throw new NotImplementedException();
    }
}