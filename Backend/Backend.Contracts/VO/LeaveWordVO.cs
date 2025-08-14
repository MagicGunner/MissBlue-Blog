namespace Backend.Contracts.VO;

/// <summary>
/// 留言视图对象（LeaveWordVO）
/// </summary>
public class LeaveWordVO {
    /// <summary>
    /// 留言ID
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// 留言用户ID
    /// </summary>
    public long UserId { get; set; }

    /// <summary>
    /// 留言内容
    /// </summary>
    public string Content { get; set; }

    /// <summary>
    /// 留言时间
    /// </summary>
    public DateTime CreateTime { get; set; }

    /// <summary>
    /// 用户昵称
    /// </summary>
    public string Nickname { get; set; }

    /// <summary>
    /// 用户头像
    /// </summary>
    public string Avatar { get; set; }

    /// <summary>
    /// 留言评论数量
    /// </summary>
    public long CommentCount { get; set; }

    /// <summary>
    /// 留言点赞数量
    /// </summary>
    public long LikeCount { get; set; }

    /// <summary>
    /// 留言收藏数量
    /// </summary>
    public long FavoriteCount { get; set; }
}