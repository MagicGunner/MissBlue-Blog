using System.ComponentModel.DataAnnotations;

namespace Backend.Modules.Blog.Contracts.DTO;

public class TreeHoleIsCheckDTO {
    /// <summary>
    /// 树洞 ID（必填）
    /// </summary>
    [Required(ErrorMessage = "树洞id不能为空")]
    public long Id { get; set; }

    /// <summary>
    /// 是否通过（必填）
    /// </summary>
    [Required(ErrorMessage = "是否通过不能为空")]
    public int IsCheck { get; set; }
}