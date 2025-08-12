using System.ComponentModel.DataAnnotations;

namespace Backend.Contracts.DTO;

public class RolePermissionDTO {
    [Required(ErrorMessage = "权限不能为空")]
    public List<long>? PermissionId { get; set; }

    [Required(ErrorMessage = "选择的角色不能为空")]
    public List<long>? RoleId { get; set; }
}