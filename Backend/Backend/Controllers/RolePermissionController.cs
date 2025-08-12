using System.ComponentModel.DataAnnotations;
using Backend.Common.Attributes;
using Backend.Common.Results;
using Backend.Contracts.DTO;
using Backend.Modules.Blog.Contracts.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Backend.Controllers;

[ApiController]
[Route("api/role/permission")]
[SwaggerTag("用户角色相关接口")]
public class RolePermissionController(IRolePermissionService rolePermissionService) : ControllerBase {
    [HttpGet("role/list")]
    [Authorize(Policy = "system:permission:role:list")]
    [AccessLimit(60, 30)]
    [SwaggerOperation(Summary = "查询权限的角色列表", Description = "查询权限的角色列表")]
    public async Task<ResponseResult<object>> SelectPermissionIdRole([FromQuery] [Required(ErrorMessage = "角色id不能为空")] long    permissionId,
                                                                     [FromQuery]                                       string? roleName,
                                                                     [FromQuery]                                       string? roleKey) {
        return ResponseHandler<object>.Create(await rolePermissionService.GetRoleByPermissionId(permissionId, roleName, roleKey, 0));
    }

    [HttpGet("not/role/list")]
    [Authorize(Policy = "system:permission:role:not:list")]
    [AccessLimit(60, 30)]
    [SwaggerOperation(Summary = "查询没有该权限的角色列表", Description = "查询没有该权限的角色列表")]
    public async Task<ResponseResult<object>> SelectPermissionNotRole([FromQuery] [Required(ErrorMessage = "角色id不能为空")] long    permissionId,
                                                                      [FromQuery]                                       string? roleName,
                                                                      [FromQuery]                                       string? roleKey) {
        return ResponseHandler<object>.Create(await rolePermissionService.GetRoleByPermissionId(permissionId, roleName, roleKey, 1));
    }

    [HttpPost("add")]
    [Authorize(Policy = "system:permission:role:add")]
    [AccessLimit(60, 30)]
    [SwaggerOperation(Summary = "添加角色权限关系", Description = "添加角色权限关系")]
    public async Task<ResponseResult<object>> AddRolePermission([
                                                                    FromBody]
                                                                [Required]
                                                                RolePermissionDTO rolePermissionDto) {
        return ResponseHandler<object>.Create(await rolePermissionService.Add(rolePermissionDto));
    }

    [HttpDelete("delete")]
    [Authorize(Policy = "system:permission:role:delete")]
    [AccessLimit(60, 30)]
    [SwaggerOperation(Summary = "删除角色权限关系", Description = "删除角色权限关系")]
    public async Task<ResponseResult<object>> DeleteRolePermission([
                                                                       FromBody]
                                                                   [Required]
                                                                   RolePermissionDTO rolePermissionDto) {
        return ResponseHandler<object>.Create(await rolePermissionService.Delete(rolePermissionDto));
    }
}