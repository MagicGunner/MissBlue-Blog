using System.ComponentModel.DataAnnotations;
using Backend.Common.Attributes;
using Backend.Common.Results;
using Backend.Contracts.DTO;
using Backend.Contracts.IService;
using Backend.Contracts.VO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Backend.Controllers.Blog;

[ApiController]
[Route("api/leaveWord")]
[SwaggerTag("留言板")]
public class LeaveWordController(ILeaveWordService leaveWordService) : ControllerBase {
    [HttpGet("list")]
    [AllowAnonymous]
    [AccessLimit(60, 10)]
    [SwaggerOperation(Summary = "获取留言板列表", Description = "获取留言板列表")]
    public async Task<ResponseResult<List<LeaveWordVO>>> List([FromQuery] string? id) {
        var result = await leaveWordService.GetList(id);
        return ResponseHandler<List<LeaveWordVO>>.Create(result);
    }

    [HttpPost("auth/userLeaveWord")]
    [AllowAnonymous] // CheckBlacklist 可扩展实现
    [AccessLimit(60, 10)]
    [SwaggerOperation(Summary = "用户留言", Description = "用户留言")]
    public async Task<ResponseResult<object>> UserLeaveWord([FromBody] [Required] string content) => ResponseHandler<object>.Create(await leaveWordService.AddLeaveWord(content));

    [HttpGet("back/list")]
    [Authorize(Policy = "blog:leaveword:list")]
    [AccessLimit(60, 30)]
    [SwaggerOperation(Summary = "后台留言列表", Description = "后台留言列表")]
    public async Task<ResponseResult<List<LeaveWordListVO>>> BackList() {
        var result = await leaveWordService.GetBackList();
        return ResponseHandler<List<LeaveWordListVO>>.Create(result);
    }

    [HttpPost("back/search")]
    [Authorize(Policy = "blog:leaveword:search")]
    [AccessLimit(60, 30)]
    [SwaggerOperation(Summary = "搜索后台留言列表", Description = "搜索后台留言列表")]
    public async Task<ResponseResult<List<LeaveWordListVO>>> BackSearch([FromBody] [Required] SearchLeaveWordDTO searchDto) {
        var result = await leaveWordService.GetBackList(searchDto);
        return ResponseHandler<List<LeaveWordListVO>>.Create(result);
    }

    [HttpPost("back/isCheck")]
    [Authorize(Policy = "blog:leaveword:isCheck")]
    [AccessLimit(60, 30)]
    [SwaggerOperation(Summary = "修改留言是否通过", Description = "修改留言是否通过")]
    public async Task<ResponseResult<object>> IsCheck([FromBody] [Required] LeaveWordIsCheckDTO leaveWordIsCheckDto) =>
        ResponseHandler<object>.Create(await leaveWordService.SetIsCheck(leaveWordIsCheckDto));

    [HttpDelete("back/delete")]
    [Authorize(Policy = "blog:leaveword:delete")]
    [AccessLimit(60, 30)]
    [SwaggerOperation(Summary = "删除留言", Description = "删除留言")]
    public async Task<ResponseResult<object>> Delete([FromBody] [Required] List<long> ids) => ResponseHandler<object>.Create(await leaveWordService.Delete(ids));
}