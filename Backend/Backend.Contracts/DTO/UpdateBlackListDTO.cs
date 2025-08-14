using System.ComponentModel.DataAnnotations;

namespace Backend.Contracts.DTO;

public class UpdateBlackListDTO {
    /// <summary>
    /// id
    /// </summary>
    [Required(ErrorMessage = "id不能为空")]
    public long Id { get; set; }

    /// <summary>
    /// 封禁理由
    /// </summary>
    [Required(ErrorMessage = "封禁理由不能为空")]
    public string Reason { get; set; }

    /// <summary>
    /// 封禁到期时间
    /// </summary>
    [Required(ErrorMessage = "封禁到期时间不能为空")]
    public DateTime ExpiresTime { get; set; }
}