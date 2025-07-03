using System.ComponentModel.DataAnnotations;
using Backend.Common.Attributes;
using Backend.Common.Results;
using Backend.Modules.Blog.Contracts.DTO;
using Backend.Modules.Blog.Contracts.VO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Backend.Controllers.Blog;

[ApiController]
[Route("api/link")]
[SwaggerTag("友链相关接口")]
public class LinkController : ControllerBase {
    [HttpPost("auth/apply")]
    [AccessLimit(60, 10)]
    [CheckBlacklist]
    [SwaggerOperation(Summary = "申请友链", Description = "申请友链")]
    public Task<ResponseResult<object>> ApplyLink([FromBody] [Required] LinkDTO linkDTO) {
        throw new NotImplementedException();
    }

    [HttpGet("list")]
    [AccessLimit(60, 30)]
    [SwaggerOperation(Summary = "查询所有通过申请的友链", Description = "查询所有通过申请的友链")]
    public Task<ResponseResult<List<LinkVO>>> GetLinkList() {
        throw new NotImplementedException();
    }

    [HttpGet("back/list")]
    [AccessLimit(60, 30)]
    [Authorize(Policy = "blog:link:list")]
    [SwaggerOperation(Summary = "后台友链列表", Description = "后台友链列表")]
    public Task<ResponseResult<List<LinkListVO>>> BackList() {
        throw new NotImplementedException();
    }

    [HttpPost("back/search")]
    [AccessLimit(60, 30)]
    [Authorize(Policy = "blog:link:search")]
    [SwaggerOperation(Summary = "搜索后台友链列表", Description = "搜索后台友链列表")]
    public Task<ResponseResult<List<LinkListVO>>> BackList([FromBody] SearchLinkDTO searchDTO) {
        throw new NotImplementedException();
    }

    [HttpPost("back/isCheck")]
    [AccessLimit(60, 30)]
    [Authorize(Policy = "blog:link:isCheck")]
    [SwaggerOperation(Summary = "修改友链是否通过", Description = "修改友链是否通过")]
    public Task<ResponseResult<object>> IsCheck([FromBody] [Required] LinkIsCheckDTO linkIsCheckDTO) {
        throw new NotImplementedException();
    }

    [HttpDelete("back/delete")]
    [AccessLimit(60, 30)]
    [Authorize(Policy = "blog:link:delete")]
    [SwaggerOperation(Summary = "删除友链", Description = "删除友链")]
    public Task<ResponseResult<object>> Delete([FromBody] List<long> ids) {
        throw new NotImplementedException();
    }

    [HttpGet("email/apply")]
    [AccessLimit(60, 60)]
    [SwaggerOperation(Summary = "邮箱审核友链", Description = "邮箱审核友链")]
    public Task<string> EmailApply([FromQuery] [Required] string verifyCode) {
        throw new NotImplementedException();
    }
}