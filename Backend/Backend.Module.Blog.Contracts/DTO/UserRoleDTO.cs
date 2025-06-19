using System.ComponentModel.DataAnnotations;

namespace Backend.Modules.Blog.Contracts.DTO;

public class UserRoleDTO {
    [Required(ErrorMessage = "角色不能为空")]
    public long RoleId { get; set; }

    [Required(ErrorMessage = "选择的用户不能为空")]
    public List<long> UserId { get; set; }
}