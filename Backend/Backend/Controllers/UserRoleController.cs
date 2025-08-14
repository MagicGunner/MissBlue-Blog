using System.ComponentModel.DataAnnotations;
using Backend.Common.Attributes;
using Backend.Common.Results;
using Backend.Contracts.DTO;
using Backend.Contracts.IService;
using Backend.Contracts.VO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Backend.Controllers;

[ApiController]
[Route("api/user/role")]
[SwaggerTag("用户角色相关接口")]
public class UserRoleController(IUserRoleService userRoleService) : ControllerBase {
    [HttpGet("user/list")]
    [AccessLimit(60, 30)] // 👈 限流参数
    [Authorize(Policy = "system:user:role:list")]
    [SwaggerOperation(Summary = "查询拥有角色的用户列表", Description = "查询拥有角色的用户列表")]
    public async Task<ResponseResult<List<RoleUserVO>>> SelectUser([FromQuery, SwaggerParameter("角色ID", Required = true)] long    roleId,
                                                                   [FromQuery, SwaggerParameter("用户名")]                   string? username,
                                                                   [FromQuery, SwaggerParameter("邮箱")]                    string? email) {
        return ResponseHandler<List<RoleUserVO>>.Create(await userRoleService.GetUserByRoleId(roleId, username, email, 0));
    }

    [HttpGet("not/user/list")]
    [AccessLimit(60, 30)] // 👈 限流参数
    [Authorize(Policy = "system:not:role:user:list")]
    [SwaggerOperation(Summary = "查询未拥有角色的用户列表", Description = "查询未拥有角色的用户列表")]
    public async Task<ResponseResult<List<RoleUserVO>>> SelectNotUserByRole([FromQuery, SwaggerParameter("角色ID", Required = true)] long    roleId,
                                                                            [FromQuery, SwaggerParameter("用户名")]                   string? username,
                                                                            [FromQuery, SwaggerParameter("邮箱")]                    string? email) {
        return ResponseHandler<List<RoleUserVO>>.Create(await userRoleService.GetUserByRoleId(roleId, username, email, 1));
    }

    [HttpPost("add")]
    [AccessLimit(60, 30)] // 👈 限流参数
    [Authorize(Policy = "system:user:role:add")]
    [SwaggerOperation(Summary = "添加用户角色关系", Description = "添加用户角色关系")]
    public async Task<ResponseResult<object>> AddUserRole([FromBody] UserRoleDTO userRoleDto) => ResponseHandler<object>.Create(await userRoleService.AddUserRole(userRoleDto));

    [HttpDelete("delete")]
    [AccessLimit(60, 30)] // 👈 限流参数
    [Authorize(Policy = "system:user:role:delete")]
    [SwaggerOperation(Summary = "删除用户角色关系", Description = "删除用户角色关系")]
    public async Task<ResponseResult<object>> DeleteUserRole([FromBody] UserRoleDTO userRoleDto) {
        return ResponseHandler<object>.Create(await userRoleService.DeleteByUserRole(userRoleDto));
    }

    [HttpGet("role/list")]
    [AccessLimit(60, 30)] // 👈 限流参数
    [Authorize(Policy = "system:role:user:list")]
    [SwaggerOperation(Summary = "查询用户拥有权限的角色列表", Description = "查询用户拥有权限的角色列表")]
    public async Task<ResponseResult<List<RoleAllVO>>> SelectPermissionIdRole([FromQuery, SwaggerParameter("用户ID", Required = true)] long    userId,
                                                                              [FromQuery, SwaggerParameter("角色名")]                   string? roleName,
                                                                              [FromQuery, SwaggerParameter("角色键")]                   string? roleKey) {
        return ResponseHandler<List<RoleAllVO>>.Create(await userRoleService.GetRoleByUserId(userId, roleName, roleKey, 0));
    }

    [HttpGet("not/role/list")]
    [AccessLimit(60, 30)] // 👈 限流参数
    [Authorize(Policy = "system:user:role:not:list")]
    [SwaggerOperation(Summary = "查询没有该用户的角色列表", Description = "查询没有该用户的角色列表")]
    public async Task<ResponseResult<List<RoleAllVO>>> SelectUserNotRole([FromQuery, SwaggerParameter("用户ID", Required = true)] long    userId,
                                                                         [FromQuery, SwaggerParameter("角色名")]                   string? roleName,
                                                                         [FromQuery, SwaggerParameter("角色键")]                   string? roleKey) {
        return ResponseHandler<List<RoleAllVO>>.Create(await userRoleService.GetRoleByUserId(userId, roleName, roleKey, 1));
    }

    [HttpPost("user/add")]
    [AccessLimit(60, 30)] // 👈 限流参数
    [Authorize(Policy = "system:user:role:add")]
    [SwaggerOperation(Summary = "添加角色用户关系", Description = "添加角色用户关系")]
    public async Task<ResponseResult<object>> AddRoleUser([FromBody] RoleUserDTO roleUserDto) {
        return ResponseHandler<object>.Create(await userRoleService.AddRoleUser(roleUserDto));
    }

    [HttpDelete("user/delete")]
    [AccessLimit(60, 30)] // 👈 限流参数
    [Authorize(Policy = "system:user:role:delete")]
    [SwaggerOperation(Summary = "删除角色用户关系", Description = "删除角色用户关系")]
    public async Task<ResponseResult<object>> DeleteRoleUser([FromBody] RoleUserDTO roleUserDto) {
        return ResponseHandler<object>.Create(await userRoleService.DeleteByRoleUser(roleUserDto));
    }
}