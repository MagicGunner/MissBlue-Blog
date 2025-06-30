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
public class AuthController(IUserService userService) : ControllerBase {
    [HttpPost("login")]
    public ResponseResult<TokenInfoVO> Login([FromBody] LoginRequestDto loginRequestDto) {
        if (!userService.ValidateUser(loginRequestDto.UserName, loginRequestDto.Password)) {
            return new ResponseResult<TokenInfoVO>(false, msg: "用户名密码错误");
        }

        var permissions = userService.GetUserPermissions(loginRequestDto.UserName);

        var claims = new List<Claim> {
                                         new(ClaimTypes.Name, loginRequestDto.UserName)
                                     };
        claims.AddRange(permissions.Select(perm => new Claim("Permission", perm)));

        var key = new SymmetricSecurityKey("SuperSecretKey123456"u8.ToArray());
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
}