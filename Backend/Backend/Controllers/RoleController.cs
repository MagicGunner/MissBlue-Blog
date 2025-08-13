using System.ComponentModel.DataAnnotations;
using Backend.Common.Attributes;
using Backend.Common.Results;
using Backend.Contracts.DTO;
using Backend.Contracts.VO;
using Backend.Modules.Blog.Contracts.DTO;
using Backend.Modules.Blog.Contracts.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Backend.Controllers;

[ApiController]
[Route("api/role")]
[SwaggerTag("角色相关接口")]
public class RoleController(IRoleService roleService) : ControllerBase {
    [HttpGet("list")]
    [AccessLimit(60, 30)] // 👈 限流参数
    [Authorize(Policy = "system:role:list")]
    [SwaggerOperation(Summary = "获取角色列表", Description = "获取角色列表")]
    public async Task<ResponseResult<List<RoleAllVO>>> ListAll() {
        return ResponseHandler<List<RoleAllVO>>.Create(await roleService.Get(null));
    }

    [HttpPost("update/status")]
    [AccessLimit(60, 30)] // 👈 限流参数
    [Authorize(Policy = "system:role:status:update")]
    [SwaggerOperation(Summary = "更新角色状态", Description = "更新角色状态")]
    public async Task<ResponseResult<object>> UpdateRoleStatus([FromBody] UpdateRoleStatusDTO roleDto) {
        return ResponseHandler<object>.Create(await roleService.UpdateStatus(roleDto));
    }

    [HttpGet("get/{id}")]
    [AccessLimit(60, 30)] // 👈 限流参数
    [Authorize(Policy = "system:role:get")]
    [SwaggerOperation(Summary = "获取修改角色信息", Description = "获取修改角色信息")]
    public async Task<ResponseResult<RoleByIdVO?>> GetRoleById([FromRoute] long id) {
        return ResponseHandler<RoleByIdVO?>.Create(await roleService.GetById(id));
    }

    [HttpPut("update")]
    [AccessLimit(60, 30)] // 👈 限流参数
    [Authorize(Policy = "system:role:update")]
    [SwaggerOperation(Summary = "修改角色信息", Description = "修改角色信息")]
    public async Task<ResponseResult<object>> UpdateRole([FromBody] RoleDTO roleDto) {
        return ResponseHandler<object>.Create(await roleService.UpdateOrInsert(roleDto));
    }

    /// <summary>新增标签（文章列表用）</summary>
    [HttpPut("add")]
    [AccessLimit(60, 30)] // 👈 限流参数
    [Authorize(Policy = "system:role:add")]
    [SwaggerOperation(Summary = "添加角色信息", Description = "添加角色信息")]
    public async Task<ResponseResult<object>> AddRole([FromBody] [Required] RoleDTO roleDto) {
        roleDto.Id = null;
        return ResponseHandler<object>.Create(await roleService.UpdateOrInsert(roleDto));
    }

    [HttpDelete("delete")]
    [AccessLimit(60, 30)] // 👈 限流参数
    [Authorize(Policy = "system:role:delete")]
    [SwaggerOperation(Summary = "根据id删除角色", Description = "根据id删除角色")]
    public async Task<ResponseResult<object>> DeleteRole([FromBody] RoleDeleteDTO roleDeleteDto) {
        return ResponseHandler<object>.Create(await roleService.Delete(roleDeleteDto));
    }
    

    [HttpPost("search")]
    [AccessLimit(60, 30)] // 👈 限流参数
    [Authorize(Policy = "system:role:search")]
    [SwaggerOperation(Summary = "根据条件搜索角色", Description = "根据条件搜索角色")]
    public async Task<ResponseResult<List<RoleAllVO>>> SearchRoles([FromBody] RoleSearchDTO roleSearchDto) {
        return ResponseHandler<List<RoleAllVO>>.Create(await roleService.Get(roleSearchDto));
    }
}