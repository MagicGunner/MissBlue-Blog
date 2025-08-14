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
[Route("api/menu")]
[SwaggerTag("菜单相关接口")]
public class MenuController(IMenuService menuService, IRoleService roleService) : ControllerBase {
    [HttpGet("list/{typeId}")]
    [AccessLimit(60, 30)]
    [Authorize(Policy = "system:menu:list")]
    [SwaggerOperation(Summary = "获取管理菜单列表", Description = "获取管理菜单列表")]
    public async Task<ResponseResult<List<MenuVO>>> List([FromRoute] int typeId) {
        var list = await menuService.GetMenuList(typeId, null, null);
        return ResponseHandler<List<MenuVO>>.Create(list);
    }

    [HttpGet("search/list/{typeId}")]
    [AccessLimit(60, 30)]
    [Authorize(Policy = "system:search:menu:list")]
    [SwaggerOperation(Summary = "搜索管理菜单列表", Description = "搜索管理菜单列表")]
    public async Task<ResponseResult<List<MenuVO>>> SearchMenu([FromRoute] int typeId, [FromQuery] string username, [FromQuery] int? status) {
        var list = await menuService.GetMenuList(typeId, username, status);
        return ResponseHandler<List<MenuVO>>.Create(list);
    }

    [HttpGet("role/list")]
    [AccessLimit(60, 5)]
    [Authorize(Policy = "system:menu:role:list")]
    [SwaggerOperation(Summary = "获取修改菜单角色列表", Description = "获取修改菜单角色列表")]
    public async Task<ResponseResult<List<RoleVO>>> SelectAll() {
        var result = await roleService.SelectAll();
        return ResponseHandler<List<RoleVO>>.Create(result);
    }

    [HttpGet("router/list/{typeId}")]
    [AccessLimit(60, 30)]
    [SwaggerOperation(Summary = "获取路由菜单列表", Description = "获取路由菜单列表")]
    public async Task<ResponseResult<List<MenuVO>>> RouterList([FromRoute] int typeId) {
        var list = await menuService.GetMenuList(typeId, null, null);
        return ResponseHandler<List<MenuVO>>.Create(list);
    }

    [HttpPost]
    [AccessLimit(60, 30)]
    [Authorize(Policy = "system:menu:add")]
    [SwaggerOperation(Summary = "添加菜单", Description = "添加菜单")]
    public async Task<ResponseResult<object>> Add([FromBody] [Required] MenuDTO menuDto) => ResponseHandler<object>.Create(await menuService.Add(menuDto));

    [HttpGet("{id}")]
    [AccessLimit(60, 30)]
    [Authorize(Policy = "system:menu:select")]
    [SwaggerOperation(Summary = "根据id查询菜单信息", Description = "根据id查询菜单信息")]
    public async Task<ResponseResult<MenuByIdVO>> GetById([FromRoute] [Required] long id) {
        var result = await menuService.GetById(id);
        return ResponseHandler<MenuByIdVO>.Create(result);
    }

    [HttpPut]
    [AccessLimit(60, 30)]
    [Authorize(Policy = "system:menu:update")]
    [SwaggerOperation(Summary = "修改菜单", Description = "修改菜单")]
    public async Task<ResponseResult<object>> Update([FromBody] [Required] MenuDTO menuDto) {
        return ResponseHandler<object>.Create(await menuService.Update(menuDto));
    }

    [HttpDelete("{id}")]
    [AccessLimit(60, 30)]
    [Authorize(Policy = "system:menu:delete")]
    [SwaggerOperation(Summary = "根据id删除菜单", Description = "根据id删除菜单")]
    public async Task<ResponseResult<string>> Delete([FromRoute] [Required] long id) {
        return ResponseHandler<string>.Create(await menuService.Delete(id));
    }
}