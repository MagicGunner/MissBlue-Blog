using System.ComponentModel.DataAnnotations;

namespace Backend.Contracts.DTO;

public class LinkIsCheckDTO {
    /// <summary>
    /// 友链 ID
    /// </summary>
    [Required(ErrorMessage = "友链id不能为空")]
    public long Id { get; set; }

    /// <summary>
    /// 是否通过（0 否，1 是）
    /// </summary>
    [Required(ErrorMessage = "是否通过不能为空")]
    public int IsCheck { get; set; }
}