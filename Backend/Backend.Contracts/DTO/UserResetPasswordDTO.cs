using System.ComponentModel.DataAnnotations;

namespace Backend.Contracts.DTO;

public class UserResetPasswordDTO {
    [Required(ErrorMessage = "密码不能为空")]
    [StringLength(20, MinimumLength = 6, ErrorMessage = "密码长度应在6到20个字符之间")]
    public string Password { get; set; }

    [Required(ErrorMessage = "验证码不能为空")]
    [StringLength(6, MinimumLength = 6, ErrorMessage = "验证码必须为6位")]
    public string Code { get; set; }

    [Required(ErrorMessage = "邮箱不能为空")]
    [EmailAddress(ErrorMessage = "邮箱格式不正确")]
    [MinLength(4, ErrorMessage = "邮箱长度不能少于4个字符")]
    public string Email { get; set; }
}