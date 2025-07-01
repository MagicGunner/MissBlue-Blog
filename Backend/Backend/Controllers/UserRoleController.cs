using Backend.Common.Results;
using Backend.Contracts.DTO;
using Backend.Contracts.IService;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[ApiController]
[Route("user/role")]
[Tags("用户角色接口")]
public class UserRoleController(IUserRoleService userRoleService) : ControllerBase {
    [HttpPost]
    public async Task<ResponseResult<object>> Add([FromBody] UserRoleDTO userRoleDto) => await userRoleService.Add(userRoleDto);
}