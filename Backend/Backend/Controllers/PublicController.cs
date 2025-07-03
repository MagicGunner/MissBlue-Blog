using System.ComponentModel.DataAnnotations;
using Backend.Common.Attributes;
using Backend.Common.Results;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Backend.Controllers;

[ApiController]
[Route("api/public")]
[SwaggerTag("公共接口")]
public class PublicController : ControllerBase {
    [HttpGet("ask-code")]
    [AccessLimit(60, 1)]
    [SwaggerOperation(Summary = "邮件发送", Description = "邮件发送")]
    public Task<ResponseResult<object>> AskVerifyCode([FromQuery] [EmailAddress] string email,
                                                      [FromQuery] [RegularExpression("(register|reset|resetEmail)", ErrorMessage = "邮箱类型错误")]
                                                      string type) {
        throw new NotImplementedException();
    }
}