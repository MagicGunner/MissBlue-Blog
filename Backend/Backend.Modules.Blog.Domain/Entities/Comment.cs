using SqlSugar;

namespace Backend.Modules.Blog.Domain.Entities;

[SugarTable("t_comment")]
public class Comment : RootEntity<long> {
    /// <summary>
    /// 评论类型（1：文章，2：留言板）
    /// </summary>
    public int Type { get; set; }

    /// <summary>
    /// 类型关联ID（如文章ID、留言板ID）
    /// </summary>
    public int TypeId { get; set; }

    /// <summary>
    /// 父评论ID（顶级为0）
    /// </summary>
    public long ParentId { get; set; }

    /// <summary>
    /// 回复评论ID
    /// </summary>
    public long ReplyId { get; set; }

    /// <summary>
    /// 评论内容
    /// </summary>
    public string CommentContent { get; set; }

    /// <summary>
    /// 评论用户ID
    /// </summary>
    public long CommentUserId { get; set; }

    /// <summary>
    /// 回复用户ID
    /// </summary>
    public long ReplyUserId { get; set; }

    /// <summary>
    /// 是否通过审核（0：否，1：是）
    /// </summary>
    public int IsCheck { get; set; }

    /// <summary>
    /// 创建时间（自动填充）
    /// </summary>
    [SugarColumn(IsOnlyIgnoreInsert = false, IsOnlyIgnoreUpdate = true)]
    public DateTime CreateTime { get; set; }

    /// <summary>
    /// 更新时间（自动填充）
    /// </summary>
    [SugarColumn(IsOnlyIgnoreInsert = false, IsOnlyIgnoreUpdate = false)]
    public DateTime UpdateTime { get; set; }

    /// <summary>
    /// 是否删除（0：未删除，1：已删除）
    /// </summary>
    public int IsDeleted { get; set; }
}