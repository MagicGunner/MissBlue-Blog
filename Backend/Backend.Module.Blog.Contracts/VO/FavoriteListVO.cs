namespace Backend.Modules.Blog.Contracts.VO;

/// <summary>
/// 收藏列表视图对象（FavoriteListVO）
/// </summary>
public class FavoriteListVO {
    /// <summary>
    /// 收藏ID
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// 收藏的用户姓名
    /// </summary>
    public string UserName { get; set; }

    /// <summary>
    /// 收藏类型（1：文章，2：留言板）
    /// </summary>
    public int Type { get; set; }

    /// <summary>
    /// 收藏内容
    /// </summary>
    public string Content { get; set; }

    /// <summary>
    /// 是否有效（0：否，1：是）
    /// </summary>
    public int IsCheck { get; set; }

    /// <summary>
    /// 收藏时间
    /// </summary>
    public DateTime CreateTime { get; set; }
}