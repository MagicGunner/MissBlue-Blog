using System.ComponentModel.DataAnnotations;
using Backend.Common.Results;
using Backend.Contracts.DTO;
using Backend.Contracts.VO;
using Backend.Modules.Blog.Contracts.DTO;
using Backend.Modules.Blog.Contracts.IService;
using Backend.Modules.Blog.Contracts.VO;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[ApiController]
[Route("api/role")]
[Tags("角色相关接口")]
public class RoleController(IRoleService roleService) : ControllerBase {
    /// <summary>新增标签（文章列表用）</summary>
    [HttpPut]
    // [Authorize(Policy = "Permission:blog:tag:add")]
    public async Task<ResponseResult<object>> AddRole([FromBody] [Required] RoleDTO roleDto) => new(await roleService.AddAsync(roleDto) > 0);

    /// <summary>删除角色</summary>
    [HttpDelete("back/delete")]
    // [Authorize(Policy = "Permission:blog:tag:delete")]
    public async Task<ResponseResult<object>> DeleteRole([FromBody] List<long> ids) => new(await roleService.DeleteByIdsAsync(ids));

    [HttpPut("update/status")]
    public async Task<ResponseResult<object>> UpdateRoleStatus([FromBody] UpdateRoleStatusDTO roleDto) => new(await roleService.UpdateStatusAsync(roleDto));

    [HttpGet("get/{id}")]
    public async Task<ResponseResult<RoleByIdVO?>> GetRoleById([FromRoute] long id) {
        var roleByIdVo = await roleService.GetByIdAsync(id);
        return new ResponseResult<RoleByIdVO?>(roleByIdVo != null, roleByIdVo);
    }

    [HttpPost("search")]
    public async Task<ResponseResult<List<RoleAllVO>>> SearchRoles([FromBody] RoleSearchDTO roleSearchDto) {
        var list = await roleService.SearchAsync(roleSearchDto);
        return new ResponseResult<List<RoleAllVO>>(list.Count > 0, list);
    }

    /// <summary>
    /// 获取角色列表
    /// </summary>
    /// <returns></returns>
    [HttpGet("list")]
    public async Task<ResponseResult<List<RoleAllVO>>> ListAll() {
        var list = await roleService.ListAllAsync();
        return new ResponseResult<List<RoleAllVO>>(list.Count > 0, list);
    }
}