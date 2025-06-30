using Backend.Common.Results;
using Backend.Modules.Blog.Contracts.IService;
using Backend.Modules.Blog.Contracts.VO;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[ApiController]
[Route("user")]
[Tags("用户相关接口")]
public class UserController(IUserService userService) : ControllerBase {
    /// <summary>
    /// 获取用户列表
    /// </summary>
    /// <returns></returns>
    [HttpGet("list")]
    public async Task<ResponseResult<List<UserListVO>>> ListAll() {
        var list = await userService.ListAllAsync();
        return new ResponseResult<List<UserListVO>>(list.Count > 0, list);
    }

    [HttpPost("login")]
    public Task<ResponseResult<object>> Login([FromForm] LoginRequest request) {
        throw new NotImplementedException();
    }
}