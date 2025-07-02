using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Backend.Common.Attributes;
using Backend.Common.Results;
using Backend.Contracts.DTO;
using Backend.Contracts.IService;
using Backend.Contracts.VO;
using Backend.Extensions.ServiceExtensions;
using Backend.Modules.Blog.Contracts.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Annotations;

namespace Backend.Controllers;

[ApiController]
[Route("api/user")]
[Tags("用户相关接口")]
public class UserController(IUserService userService, IConfiguration configuration) : ControllerBase {
    [HttpGet("auth/info")]
    [SwaggerOperation(Summary = "获取当前登录用户信息", Description = "111111")]
    public async Task<ResponseResult<UserAccountVO?>> GetInfo() => new(true, await userService.GetAccountById());

    [HttpPost("auth/update")]
    [AccessLimit(60, 30)] // 👈 限流参数
    public async Task<ResponseResult<object>> UpdateUser([FromBody] UserUpdateDTO userUpdateDto) => new(await userService.UpdateUser(userUpdateDto));

    [HttpPost("auth/upload/avatar")]
    [AccessLimit(60, 3)] // 👈 限流参数
    public Task<ResponseResult<string>> UpdateAvatar(IFormFile avatarFile) {
        throw new NotImplementedException();
    }

    [HttpPost("auth/update/email")]
    [AccessLimit(60, 30)] // 👈 限流参数
    public Task<ResponseResult<object>> UpdateEmail([FromBody] UpdateEmailDTO updateEmailDto) {
        throw new NotImplementedException();
    }


    [HttpPost("login")]
    public async Task<ResponseResult<TokenInfoVO>> Login([FromBody] LoginRequestDto loginRequestDto) {
        var userId = await userService.ValidateUser(loginRequestDto.UserName, loginRequestDto.Password);
        if (userId < 0) {
            return new ResponseResult<TokenInfoVO>(false, msg: "账号密码错误");
        }

        var permissions = await userService.GetUserPermissions(loginRequestDto.UserName);

        var claims = new List<Claim> {
                                         new(ClaimTypes.NameIdentifier, userId.ToString()),
                                         new(ClaimTypes.Name, loginRequestDto.UserName)
                                     };
        claims.AddRange(permissions.Select(perm => new Claim("Permission", perm.PermissionKey)));

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:Secret"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(issuer: "MissBlue",
                                         audience: "Client",
                                         claims: claims,
                                         expires: DateTime.Now.AddHours(6),
                                         signingCredentials: creds);

        var jwt = new JwtSecurityTokenHandler().WriteToken(token);
        return new ResponseResult<TokenInfoVO>(true, new TokenInfoVO { Token = jwt });
    }

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