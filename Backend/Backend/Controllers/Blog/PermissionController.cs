using System.ComponentModel.DataAnnotations;
using Backend.Common.Results;
using Backend.Contracts.DTO;
using Backend.Contracts.IService;
using Backend.Contracts.VO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Backend.Controllers.Blog;

[ApiController]
[Route("api/permission")]
[SwaggerTag("权限字符相关接口")]
public class PermissionController(IPermissionService permissionService) : ControllerBase {
    [HttpGet("list")]
    [Authorize(Policy = "system:permission:list")]
    [SwaggerOperation(Summary = "所有权限", Description = "所有权限")]
    public async Task<ResponseResult<object>> List() {
        return ResponseHandler<object>.Create(await permissionService.Get(null, null, null));
    }

    [HttpGet("search")]
    [Authorize(Policy = "system:permission:search")]
    [SwaggerOperation(Summary = "搜索权限", Description = "搜索权限")]
    public async Task<ResponseResult<object>> SearchPermission([
                                                                   FromQuery]
                                                               string? permissionDesc,
                                                               [FromQuery] string? permissionKey,
                                                               [FromQuery] long?   permissionMenuId) {
        return ResponseHandler<object>.Create(await permissionService.Get(permissionDesc, permissionKey, permissionMenuId));
    }

    [HttpGet("menu")]
    [Authorize(Policy = "system:permission:menu:list")]
    [SwaggerOperation(Summary = "查询所有权限所在菜单", Description = "查询所有权限所在菜单")]
    public async Task<ResponseResult<List<PermissionMenuVO>>> Menu() {
        return ResponseHandler<List<PermissionMenuVO>>.Create(await permissionService.GetPermissionMenuList());
    }

    [HttpPost("add")]
    [Authorize(Policy = "system:permission:add")]
    [SwaggerOperation(Summary = "添加权限字符", Description = "添加权限字符")]
    public async Task<ResponseResult<object>> AddPermission([FromBody] PermissionDTO permissionDto) {
        return ResponseHandler<object>.Create(await permissionService.UpdateOrInsert(permissionDto));
    }

    [HttpPost("update")]
    [Authorize(Policy = "system:permission:update")]
    [SwaggerOperation(Summary = "修改权限字符", Description = "修改权限字符")]
    public async Task<ResponseResult<object>> UpdatePermission([FromBody] PermissionDTO permissionDto) {
        return ResponseHandler<object>.Create(await permissionService.UpdateOrInsert(permissionDto));
    }

    [HttpGet("get/{id}")]
    [Authorize(Policy = "system:permission:get")]
    [SwaggerOperation(Summary = "获取要修改的权限信息", Description = "获取要修改的权限信息")]
    public async Task<ResponseResult<PermissionDTO>> GetPermission([FromRoute] [Required] long id) {
        return ResponseHandler<PermissionDTO>.Create(await permissionService.GetById(id));
    }

    [HttpDelete("delete/{id}")]
    [Authorize(Policy = "system:permission:delete")]
    [SwaggerOperation(Summary = "删除权限字符", Description = "删除权限字符")]
    public async Task<ResponseResult<object>> DeletePermission([
                                                                   FromRoute]
                                                               [Required]
                                                               long id) {
        return ResponseHandler<object>.Create(await permissionService.Delete(id));
    }
}