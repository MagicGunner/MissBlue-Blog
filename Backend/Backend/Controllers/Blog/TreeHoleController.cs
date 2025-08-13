using Backend.Common.Attributes;
using Backend.Common.Results;
using Backend.Modules.Blog.Contracts.DTO;
using Backend.Modules.Blog.Contracts.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Backend.Controllers.Blog;

[ApiController]
[Route("api/treeHole")]
[SwaggerTag("树洞相关接口")]
public class TreeHoleController(ITreeHoleService treeHoleService) : ControllerBase {
    [HttpPost("auth/addTreeHole")]
    [AccessLimit(60, 60)]
    [SwaggerOperation(Summary = "添加树洞")]
    [ServiceFilter(typeof(CheckBlacklistAttribute))] // 需要自定义实现 CheckBlacklistAttribute
    public async Task<ResponseResult<object>> AddTreeHole([FromBody] string content) {
        return ResponseHandler<object>.Create(await treeHoleService.Add(content));
    }

    [HttpGet("getTreeHoleList")]
    [AccessLimit(60, 60)]
    [SwaggerOperation(Summary = "查看树洞")]
    public async Task<ResponseResult<object>> GetTreeHoleList() {
        return ResponseHandler<object>.Create(await treeHoleService.GetFrontList());
    }

    [HttpGet("back/list")]
    [Authorize(Policy = "blog:treeHole:list")]
    [AccessLimit(60, 30)]
    [SwaggerOperation(Summary = "后台树洞列表")]
    public async Task<ResponseResult<object>> BackList() {
        return ResponseHandler<object>.Create(await treeHoleService.GetBackList(null));
    }

    [HttpPost("back/search")]
    [Authorize(Policy = "blog:treeHole:search")]
    [AccessLimit(60, 30)]
    [SwaggerOperation(Summary = "搜索后台树洞列表")]
    public async Task<ResponseResult<object>> SearchBackList([FromBody] SearchTreeHoleDTO searchDto) {
        return ResponseHandler<object>.Create(await treeHoleService.GetBackList(searchDto));

    }

    [HttpPost("back/isCheck")]
    [Authorize(Policy = "blog:treeHole:isCheck")]
    [AccessLimit(60, 30)]
    [SwaggerOperation(Summary = "修改树洞是否通过")]
    public async Task<ResponseResult<object>> IsCheck([FromBody] TreeHoleIsCheckDTO checkDto) {
        return ResponseHandler<object>.Create(await treeHoleService.SetCheck(checkDto));
    }

    [HttpDelete("back/delete")]
    [Authorize(Policy = "blog:treeHole:delete")]
    [AccessLimit(60, 30)]
    [SwaggerOperation(Summary = "删除树洞")]
    public async Task<ResponseResult<object>> Delete([FromBody] List<long> ids) {
        return ResponseHandler<object>.Create(await treeHoleService.DeleteByIds(ids));
    }
}