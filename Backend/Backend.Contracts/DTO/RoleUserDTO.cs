using System.ComponentModel.DataAnnotations;

namespace Backend.Contracts.DTO;

public class RoleUserDTO {
    [Required(ErrorMessage = "用户不能为空")]
    public List<long> UserId { get; set; }

    [Required(ErrorMessage = "选择的角色不能为空")]
    public List<long> RoleId { get; set; }
}