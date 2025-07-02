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
[SwaggerTag("用户相关接口")]
public class UserController(IUserService userService, IConfiguration configuration) : ControllerBase {
    #region 登入登出相关

    [HttpPost("login")]
    [SwaggerOperation(Summary = "用户登录", Description = "账号密码登录")]
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
        return new ResponseResult<TokenInfoVO>(true, new TokenInfoVO {
                                                                         Token = jwt
                                                                     });
    }

    #endregion

    [HttpGet("auth/info")]
    [SwaggerOperation(Summary = "获取用户信息", Description = "通过用户ID获取详细的用户信息")]
    public async Task<ResponseResult<UserAccountVO?>> GetInfo() => new(true, await userService.GetAccountById());

    [HttpPost("auth/update")]
    [AccessLimit(60, 30)] // 👈 限流参数
    [SwaggerOperation(Summary = "修改用户信息", Description = "修改用户信息")]
    public async Task<ResponseResult<object>> UpdateUser([FromBody] UserUpdateDTO userUpdateDto) => new(await userService.UpdateUser(userUpdateDto));

    [HttpPost("auth/upload/avatar")]
    [AccessLimit(60, 3)] // 👈 限流参数
    [SwaggerOperation(Summary = "用户头像上传", Description = "用户头像上传")]
    public Task<ResponseResult<string>> UpdateAvatar(IFormFile avatarFile) {
        throw new NotImplementedException();
    }

    [HttpPost("auth/update/email")]
    [AccessLimit(60, 30)] // 👈 限流参数
    [SwaggerOperation(Summary = "修改用户绑定邮箱", Description = "修改用户绑定邮箱")]
    public Task<ResponseResult<object>> UpdateEmail([FromBody] UpdateEmailDTO updateEmailDto) {
        throw new NotImplementedException();
    }

    [HttpPost("auth/third/update/email")]
    [AccessLimit(60, 30)] // 👈 限流参数
    [SwaggerOperation(Summary = "第三方登录用户绑定邮箱", Description = "第三方登录用户绑定邮箱")]
    public Task<ResponseResult<object>> ThirdUpdateEmail([FromBody] UpdateEmailDTO updateEmailDto) {
        throw new NotImplementedException();
    }

    [HttpPost("register")]
    [AccessLimit(60, 30)] // 👈 限流参数
    [SwaggerOperation(Summary = "前台注册", Description = "前台注册")]
    public async Task<ResponseResult<object>> Register([FromBody] UserRegisterDTO userRegisterDto) {
        var result = await userService.Register(userRegisterDto, HttpContext);
        return new ResponseResult<object>(result);
    }

    [HttpPost("reset-confirm")]
    [AccessLimit(60, 30)] // 👈 限流参数
    [SwaggerOperation(Summary = "重置密码-确认邮件", Description = "重置密码-确认邮件")]
    public Task<ResponseResult<object>> ResetConfirm([FromBody] UserResetConfirmDTO userResetConfirmDto) {
        throw new NotImplementedException();
    }

    [HttpPost("reset-password")]
    [AccessLimit(60, 30)] // 👈 限流参数
    [SwaggerOperation(Summary = "重置密码-确认邮件", Description = "重置密码-确认邮件")]
    public Task<ResponseResult<object>> ResetPassword([FromBody] UserResetPasswordDTO userResetPassword) {
        throw new NotImplementedException();
    }
    
    
    
    /// <summary>
    /// 获取用户列表
    /// </summary>
    /// <returns></returns>
    [HttpGet("list")]
    [AccessLimit(60, 30)] // 👈 限流参数
    [Authorize(Policy = "system:user:list")]
    [SwaggerOperation(Summary = "搜索用户列表", Description = "搜索用户列表，需要权限")]
    public async Task<ResponseResult<List<UserListVO>>> ListAll() {
        var list = await userService.ListAllAsync();
        return new ResponseResult<List<UserListVO>>(list.Count > 0, list);
    }

    [HttpPost("update/status")]
    [AccessLimit(60, 30)] // 👈 限流参数
    [Authorize(Policy = "system:user:status:update")]
    [SwaggerOperation(Summary = "更新用户状态", Description = "更新用户状态")]
    public Task<ResponseResult<object>> UpdateStatus([FromBody] UpdateRoleStatusDTO updateRoleStatusDto) {
        throw new NotImplementedException();
    }

    [HttpGet("details/{id}")]
    [AccessLimit(60, 30)] // 👈 限流参数
    [Authorize(Policy = "system:user:details")]
    [SwaggerOperation(Summary = "获取用户详细信息", Description = "获取用户详细信息")]
    public Task<ResponseResult<UserDetailsVO>> GetUserDetails([FromRoute] long id) {
        throw new NotImplementedException();
    }

    [HttpDelete("delete")]
    [AccessLimit(60, 30)] // 👈 限流参数
    [Authorize(Policy = "system:user:delete")]
    [SwaggerOperation(Summary = "删除用户", Description = "删除用户")]
    public Task<ResponseResult<UserDetailsVO>> DeleteUser([FromBody] UserDeleteDTO userDeleteDto) {
        throw new NotImplementedException();
    }
}