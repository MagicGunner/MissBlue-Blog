using Backend.Common.Attributes;
using Backend.Common.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Backend.Controllers.Blog;

[ApiController]
[Route("api/treeHole")]
[SwaggerTag("树洞相关接口")]
public class TreeHoleController : ControllerBase {
    [HttpPost("auth/addTreeHole")]
    [AccessLimit(60, 60)]
    [SwaggerOperation(Summary = "添加树洞")]
    [ServiceFilter(typeof(CheckBlacklistAttribute))] // 需要自定义实现 CheckBlacklistAttribute
    public Task<ResponseResult<object>> AddTreeHole([FromBody] string content) {
        throw new NotImplementedException();
    }

    [HttpGet("getTreeHoleList")]
    [AccessLimit(60, 60)]
    [SwaggerOperation(Summary = "查看树洞")]
    public Task<ResponseResult<object>> GetTreeHoleList() {
        throw new NotImplementedException();
    }

    [HttpGet("back/list")]
    [Authorize(Policy = "blog:treeHole:list")]
    [AccessLimit(60, 30)]
    [SwaggerOperation(Summary = "后台树洞列表")]
    public Task<ResponseResult<object>> BackList() {
        throw new NotImplementedException();
    }

    [HttpPost("back/search")]
    [Authorize(Policy = "blog:treeHole:search")]
    [AccessLimit(60, 30)]
    [SwaggerOperation(Summary = "搜索后台树洞列表")]
    public Task<ResponseResult<object>> SearchBackList([FromBody] object searchDto) {
        throw new NotImplementedException();
    }

    [HttpPost("back/isCheck")]
    [Authorize(Policy = "blog:treeHole:isCheck")]
    [AccessLimit(60, 30)]
    [SwaggerOperation(Summary = "修改树洞是否通过")]
    public Task<ResponseResult<object>> IsCheck([FromBody] object checkDto) {
        throw new NotImplementedException();
    }

    [HttpDelete("back/delete")]
    [Authorize(Policy = "blog:treeHole:delete")]
    [AccessLimit(60, 30)]
    [SwaggerOperation(Summary = "删除树洞")]
    public Task<ResponseResult<object>> Delete([FromBody] List<long> ids) {
        throw new NotImplementedException();
    }
}