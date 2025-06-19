using System.ComponentModel.DataAnnotations;

namespace Backend.Modules.Blog.Contracts.DTO;

public class UserCommentDTO {
    /// <summary>
    /// 评论类型 (1文章 2留言板)
    /// </summary>
    [Required(ErrorMessage = "评论类型不能为空")]
    public int Type { get; set; }

    /// <summary>
    /// 类型id
    /// </summary>
    [Required(ErrorMessage = "类型id不能为空")]
    public int TypeId { get; set; }

    /// <summary>
    /// 父评论id
    /// </summary>
    public long? ParentId { get; set; }

    /// <summary>
    /// 回复评论id
    /// </summary>
    public long? ReplyId { get; set; }

    /// <summary>
    /// 评论内容
    /// </summary>
    [Required(ErrorMessage = "评论内容不能为空")]
    public string CommentContent { get; set; }

    /// <summary>
    /// 回复用户的id
    /// </summary>
    public long? ReplyUserId { get; set; }
}