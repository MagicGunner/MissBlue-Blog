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
[Route("api/link")]
[SwaggerTag("友链相关接口")]
public class LinkController(ILinkService linkService) : ControllerBase {
    [HttpPost("auth/apply")]
    [AccessLimit(60, 10)]
    [CheckBlacklist]
    [SwaggerOperation(Summary = "申请友链", Description = "申请友链")]
    public async Task<ResponseResult<object>> ApplyLink([FromBody] [Required] LinkDTO linkDto) {
        return ResponseHandler<object>.Create(await linkService.ApplyLink(linkDto));
    }

    [HttpGet("list")]
    [AccessLimit(60, 30)]
    [SwaggerOperation(Summary = "查询所有通过申请的友链", Description = "查询所有通过申请的友链")]
    public async Task<ResponseResult<List<LinkVO>>> GetLinkList() {
        return ResponseHandler<List<LinkVO>>.Create(await linkService.GetLinkList());
    }

    [HttpGet("back/list")]
    [AccessLimit(60, 30)]
    [Authorize(Policy = "blog:link:list")]
    [SwaggerOperation(Summary = "后台友链列表", Description = "后台友链列表")]
    public async Task<ResponseResult<List<LinkListVO>>> BackList() {
        return ResponseHandler<List<LinkListVO>>.Create(await linkService.GetBackLinkList(null));
    }

    [HttpPost("back/search")]
    [AccessLimit(60, 30)]
    [Authorize(Policy = "blog:link:search")]
    [SwaggerOperation(Summary = "搜索后台友链列表", Description = "搜索后台友链列表")]
    public async Task<ResponseResult<List<LinkListVO>>> BackList([FromBody] SearchLinkDTO searchDto) {
        return ResponseHandler<List<LinkListVO>>.Create(await linkService.GetBackLinkList(searchDto));
    }

    [HttpPost("back/isCheck")]
    [AccessLimit(60, 30)]
    [Authorize(Policy = "blog:link:isCheck")]
    [SwaggerOperation(Summary = "修改友链是否通过", Description = "修改友链是否通过")]
    public async Task<ResponseResult<object>> SetChecked([FromBody] [Required] LinkIsCheckDTO linkIsCheckDTO) {
        return ResponseHandler<object>.Create(await linkService.SetChecked(linkIsCheckDTO));
        ;
    }

    [HttpDelete("back/delete")]
    [AccessLimit(60, 30)]
    [Authorize(Policy = "blog:link:delete")]
    [SwaggerOperation(Summary = "删除友链", Description = "删除友链")]
    public async Task<ResponseResult<object>> Delete([FromBody] List<long> ids) {
        return ResponseHandler<object>.Create(await linkService.Delete(ids));
    }

    [HttpGet("email/apply")]
    [AccessLimit(60, 60)]
    [SwaggerOperation(Summary = "邮箱审核友链", Description = "邮箱审核友链")]
    public async Task EmailApply([FromQuery] [Required] string verifyCode) {
        await linkService.EmailApply(verifyCode, Response);
    }
}