using System.ComponentModel.DataAnnotations;

namespace Backend.Contracts.DTO;

public class UserRegisterDTO {
    [Required(ErrorMessage = "用户名不能为空")]
    [RegularExpression(@"^[a-zA-Z0-9\u4e00-\u9fa5]+$", ErrorMessage = "用户名格式错误")]
    [StringLength(10, MinimumLength = 1, ErrorMessage = "用户名长度必须在1到10之间")]
    public string Username { get; set; }

    [Required(ErrorMessage = "密码不能为空")]
    [StringLength(20, MinimumLength = 6, ErrorMessage = "密码长度必须在6到20之间")]
    public string Password { get; set; }

    [Required(ErrorMessage = "验证码不能为空")]
    [StringLength(6, MinimumLength = 6, ErrorMessage = "验证码必须为6位")]
    public string Code { get; set; }

    [Required(ErrorMessage = "邮箱不能为空")]
    [EmailAddress(ErrorMessage = "邮箱格式不正确")]
    [MinLength(4, ErrorMessage = "邮箱长度不能少于4个字符")]
    public string Email { get; set; }
}