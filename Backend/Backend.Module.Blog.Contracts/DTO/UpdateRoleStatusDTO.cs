using System.ComponentModel.DataAnnotations;

namespace Backend.Modules.Blog.Contracts.DTO;

public class UpdateRoleStatusDTO {
    [Required(ErrorMessage = "id不能为空")]
    public long Id { get; set; }

    [Range(0, 1, ErrorMessage = "状态必须是 0 或 1")]
    public int Status { get; set; }
}