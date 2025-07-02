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
    public Task<ResponseResult<List<RoleAllVO>>> ListAll() {
        // var list = await roleService.ListAllAsync();
        // return new ResponseResult<List<RoleAllVO>>(list.Count > 0, list);
        throw new NotImplementedException();
    }

    [HttpPost("update/status")]
    [AccessLimit(60, 30)] // 👈 限流参数
    [Authorize(Policy = "system:role:status:update")]
    [SwaggerOperation(Summary = "更新角色状态", Description = "更新角色状态")]
    public Task<ResponseResult<object>> UpdateRoleStatus([FromBody] UpdateRoleStatusDTO roleDto) {
        // new(await roleService.UpdateStatusAsync(roleDto));
        throw new NotImplementedException();
    }

    [HttpGet("get/{id}")]
    [AccessLimit(60, 30)] // 👈 限流参数
    [Authorize(Policy = "system:role:get")]
    [SwaggerOperation(Summary = "获取修改角色信息", Description = "获取修改角色信息")]
    public Task<ResponseResult<RoleByIdVO?>> GetRoleById([FromRoute] long id) {
        // var roleByIdVo = await roleService.GetByIdAsync(id);
        // return new ResponseResult<RoleByIdVO?>(roleByIdVo != null, roleByIdVo);
        throw new NotImplementedException();
    }

    [HttpPut("update")]
    [AccessLimit(60, 30)] // 👈 限流参数
    [Authorize(Policy = "system:role:update")]
    [SwaggerOperation(Summary = "修改角色信息", Description = "修改角色信息")]
    public Task<ResponseResult<RoleByIdVO?>> UpdateRole([FromBody] RoleDTO role) {
        throw new NotImplementedException();
    }

    /// <summary>新增标签（文章列表用）</summary>
    [HttpPut("add")]
    [AccessLimit(60, 30)] // 👈 限流参数
    [Authorize(Policy = "system:role:add")]
    [SwaggerOperation(Summary = "添加角色信息", Description = "添加角色信息")]
    public Task<ResponseResult<object>> AddRole([FromBody] [Required] RoleDTO roleDto) {
        // new(await roleService.AddAsync(roleDto) > 0);
        throw new NotImplementedException();
    }

    [HttpDelete("delete")]
    [AccessLimit(60, 30)] // 👈 限流参数
    [Authorize(Policy = "system:role:delete")]
    [SwaggerOperation(Summary = "根据id删除角色", Description = "根据id删除角色")]
    public Task<ResponseResult<object>> DeleteRole([FromBody] RoleDeleteDTO roleDeleteDto) {
        throw new NotImplementedException();
    }
    

    [HttpPost("search")]
    [AccessLimit(60, 30)] // 👈 限流参数
    [Authorize(Policy = "system:role:search")]
    [SwaggerOperation(Summary = "根据条件搜索角色", Description = "根据条件搜索角色")]
    public Task<ResponseResult<List<RoleAllVO>>> SearchRoles([FromBody] RoleSearchDTO roleSearchDto) {
        // var list = await roleService.SearchAsync(roleSearchDto);
        // return new ResponseResult<List<RoleAllVO>>(list.Count > 0, list);
        throw new NotImplementedException();
    }

   
}