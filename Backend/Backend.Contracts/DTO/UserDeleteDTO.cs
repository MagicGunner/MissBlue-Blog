using System.ComponentModel.DataAnnotations;

namespace Backend.Contracts.DTO;

public class UserDeleteDTO {
    /// <summary>
    /// 用户ID列表
    /// </summary>
    [Required(ErrorMessage = "用户ID列表不能为空")]
    public List<long> Ids { get; set; }
}