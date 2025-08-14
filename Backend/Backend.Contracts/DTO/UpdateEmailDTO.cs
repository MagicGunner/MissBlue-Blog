using System.ComponentModel.DataAnnotations;

namespace Backend.Contracts.DTO;

public class UpdateEmailDTO {
    /// <summary>
    /// 验证码
    /// </summary>
    [Required(ErrorMessage = "验证码不能为空")]
    public string Code { get; set; }

    /// <summary>
    /// 邮箱
    /// </summary>
    [EmailAddress(ErrorMessage = "邮箱格式不正确")]
    [MinLength(4, ErrorMessage = "邮箱长度不能小于4个字符")]
    public string Email { get; set; }

    /// <summary>
    /// 密码
    /// </summary>
    [Required(ErrorMessage = "密码不能为空")]
    public string Password { get; set; }
}