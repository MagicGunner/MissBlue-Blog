using System.ComponentModel.DataAnnotations;

namespace Backend.Contracts.DTO;

public class RoleDeleteDTO {
    [Required(ErrorMessage = "角色ID列表不能为空")]
    public List<long> Ids { get; set; }
}