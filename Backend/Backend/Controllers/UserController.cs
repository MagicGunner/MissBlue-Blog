using Backend.Common.Results;
using Backend.Contracts.DTO;
using Backend.Contracts.IService;
using Backend.Contracts.VO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[ApiController]
[Route("api/user")]
[Tags("用户相关接口")]
public class UserController(IUserService userService) : ControllerBase {
    /// <summary>
    /// 获取用户列表
    /// </summary>
    /// <returns></returns>
    [HttpGet("list")]
    [Authorize(Policy = "system:user:list")]
    public async Task<ResponseResult<List<UserListVO>>> ListAll() {
        var list = await userService.ListAllAsync();
        return new ResponseResult<List<UserListVO>>(list.Count > 0, list);
    }

    [HttpPost("/register")]
    public async Task<ResponseResult<object>> Register([FromBody] UserRegisterDTO userRegisterDto) {
        var result = await userService.RegisterAsync(userRegisterDto, HttpContext);
        return new ResponseResult<object>(result);
    }
}