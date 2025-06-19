using System.ComponentModel.DataAnnotations;

namespace Backend.Modules.Blog.Contracts.DTO;

/// <summary>
/// 评论审核状态 DTO
/// </summary>
public class CommentIsCheckDto {
    /// <summary>
    /// 评论 ID
    /// </summary>
    [Required(ErrorMessage = "评论id不能为空")]
    public long Id { get; set; }

    /// <summary>
    /// 是否通过（0：否，1：是）
    /// </summary>
    [Required(ErrorMessage = "是否通过不能为空")]
    public int IsCheck { get; set; }
}