using System.ComponentModel.DataAnnotations;

namespace Backend.Contracts.DTO;

public class LoginLogDeleteDTO {
    [Required(ErrorMessage = "删除ID列表不能为空")]
    public List<long> Ids { get; set; }
}