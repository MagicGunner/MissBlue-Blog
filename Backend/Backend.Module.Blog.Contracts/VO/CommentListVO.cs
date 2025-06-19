namespace Backend.Modules.Blog.Contracts.VO;

/// <summary>
/// 评论列表视图对象（CommentListVO）
/// </summary>
public class CommentListVO {
    /// <summary>
    /// 评论ID
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// 评论类型（1文章，2留言板）
    /// </summary>
    public int Type { get; set; }

    /// <summary>
    /// 关联类型ID
    /// </summary>
    public int TypeId { get; set; }

    /// <summary>
    /// 父评论ID
    /// </summary>
    public long? ParentId { get; set; }

    /// <summary>
    /// 回复评论ID
    /// </summary>
    public long? ReplyId { get; set; }

    /// <summary>
    /// 评论内容
    /// </summary>
    public string CommentContent { get; set; }

    /// <summary>
    /// 评论用户名称
    /// </summary>
    public string CommentUserName { get; set; }

    /// <summary>
    /// 是否通过（0 否，1 是）
    /// </summary>
    public int IsCheck { get; set; }

    /// <summary>
    /// 评论时间
    /// </summary>
    public DateTime CreateTime { get; set; }

    /// <summary>
    /// 更新时间
    /// </summary>
    public DateTime UpdateTime { get; set; }
}