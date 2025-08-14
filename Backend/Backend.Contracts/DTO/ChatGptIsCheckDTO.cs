using System.ComponentModel.DataAnnotations;

namespace Backend.Contracts.DTO;

/// <summary>
/// ChatGPT审核状态 DTO
/// </summary>
public class ChatGptIsCheckDto {
    /// <summary>
    /// 会话 ID
    /// </summary>
    [Required(ErrorMessage = "会话id不能为空")]
    public long Id { get; set; }

    /// <summary>
    /// 是否通过（0：否，1：是）
    /// </summary>
    [Required(ErrorMessage = "是否有效不能为空")]
    public int IsCheck { get; set; }
}