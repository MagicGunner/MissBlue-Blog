using System.ComponentModel.DataAnnotations;

namespace Backend.Modules.Blog.Contracts.DTO;

public class PermissionDTO {
    // 权限表 ID
    public int? Id { get; set; }

    // 描述
    [Required(ErrorMessage = "权限描述不能为空")]
    public string PermissionDesc { get; set; }

    // 权限字符
    [Required(ErrorMessage = "权限字符不能为空")]
    public string PermissionKey { get; set; }

    // 所在菜单
    [Required(ErrorMessage = "所在菜单不能为空")]
    public long? PermissionMenuId { get; set; }
}