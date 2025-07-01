using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Backend.Common.Results;
using Backend.Contracts.DTO;
using Backend.Contracts.IService;
using Backend.Contracts.VO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Backend.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController(IUserService userService, IConfiguration configuration) : ControllerBase {
    [HttpPost("login")]
    public async Task<ResponseResult<TokenInfoVO>> Login([FromBody] LoginRequestDto loginRequestDto) {
        var result = await userService.ValidateUser(loginRequestDto.UserName, loginRequestDto.Password);
        if (!result) {
            return new ResponseResult<TokenInfoVO>(result, msg: "账号密码错误");
        }

        var permissions = await userService.GetUserPermissions(loginRequestDto.UserName);

        var claims = new List<Claim> { new(ClaimTypes.Name, loginRequestDto.UserName) };
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
}