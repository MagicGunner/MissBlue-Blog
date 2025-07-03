using System.ComponentModel.DataAnnotations;
using Backend.Common.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Backend.Controllers.Blog;

[ApiController]
[Route("api/permission")]
[SwaggerTag("权限字符相关接口")]
public class PermissionController : ControllerBase {
    [HttpGet("list")]
    [Authorize(Policy = "system:permission:list")]
    [SwaggerOperation(Summary = "所有权限", Description = "所有权限")]
    public Task<ResponseResult<object>> List() {
        throw new NotImplementedException();
    }

    [HttpGet("search")]
    [Authorize(Policy = "system:permission:search")]
    [SwaggerOperation(Summary = "搜索权限", Description = "搜索权限")]
    public Task<ResponseResult<object>> SearchPermission([
                                                             FromQuery]
                                                         string? permissionDesc,
                                                         [FromQuery] string? permissionKey,
                                                         [FromQuery] long?   permissionMenuId) {
        throw new NotImplementedException();
    }

    [HttpGet("menu")]
    [Authorize(Policy = "system:permission:menu:list")]
    [SwaggerOperation(Summary = "查询所有权限所在菜单", Description = "查询所有权限所在菜单")]
    public Task<ResponseResult<object>> Menu() {
        throw new NotImplementedException();
    }

    [HttpPost("add")]
    [Authorize(Policy = "system:permission:add")]
    [SwaggerOperation(Summary = "添加权限字符", Description = "添加权限字符")]
    public Task<ResponseResult<object>> AddPermission([FromBody] object permissionDTO) {
        throw new NotImplementedException();
    }

    [HttpPost("update")]
    [Authorize(Policy = "system:permission:update")]
    [SwaggerOperation(Summary = "修改权限字符", Description = "修改权限字符")]
    public Task<ResponseResult<object>> UpdatePermission([FromBody] object permissionDTO) {
        throw new NotImplementedException();
    }

    [HttpGet("get/{id}")]
    [Authorize(Policy = "system:permission:get")]
    [SwaggerOperation(Summary = "获取要修改的权限信息", Description = "获取要修改的权限信息")]
    public Task<ResponseResult<object>> GetPermission([
                                                          FromRoute]
                                                      [Required]
                                                      long id) {
        throw new NotImplementedException();
    }

    [HttpDelete("delete/{id}")]
    [Authorize(Policy = "system:permission:delete")]
    [SwaggerOperation(Summary = "删除权限字符", Description = "删除权限字符")]
    public Task<ResponseResult<object>> DeletePermission([
                                                             FromRoute]
                                                         [Required]
                                                         long id) {
        throw new NotImplementedException();
    }
}