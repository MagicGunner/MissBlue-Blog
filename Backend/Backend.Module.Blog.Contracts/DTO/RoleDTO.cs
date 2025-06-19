using System.ComponentModel.DataAnnotations;

namespace Backend.Modules.Blog.Contracts.DTO;

public class RoleDTO {
    // 角色id
    public long? Id { get; set; }

    // 角色名称
    [Required(ErrorMessage = "角色名称不能为空")]
    public string RoleName { get; set; }

    // 角色字符
    [Required(ErrorMessage = "角色标识不能为空")]
    public string RoleKey { get; set; }

    // 状态（0：正常，1：停用）
    [Required(ErrorMessage = "状态不能为空")]
    public int Status { get; set; }

    // 顺序
    [Required(ErrorMessage = "排序号不能为空")]
    public long OrderNum { get; set; }

    // 备注
    public string Remark { get; set; }

    // 权限菜单ID集合
    public List<long> MenuIds { get; set; }
}