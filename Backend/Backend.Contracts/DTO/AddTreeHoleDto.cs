using System.ComponentModel.DataAnnotations;

namespace Backend.Contracts.DTO;

public class AddTreeHoleDto {
    [Required(ErrorMessage = "内容不能为空")]
    public string Content { get; set; }
}