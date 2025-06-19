using System.ComponentModel.DataAnnotations;

namespace Backend.Modules.Blog.Contracts.DTO;

public class UserUpdateDTO {
    /// <summary>用户昵称</summary>
    [Required(ErrorMessage = "用户昵称不能为空")]
    public string Nickname { get; set; }

    /// <summary>用户性别</summary>
    [Required(ErrorMessage = "用户性别不能为空")]
    public int Gender { get; set; }

    /// <summary>用户头像</summary>
    [Required(ErrorMessage = "用户头像不能为空")]
    public string Avatar { get; set; }

    /// <summary>个人简介</summary>
    [Required(ErrorMessage = "个人简介不能为空")]
    public string Intro { get; set; }
}