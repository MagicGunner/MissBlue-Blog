namespace Backend.Modules.Blog.Contracts.VO;

/// <summary>
/// 留言列表视图对象（LeaveWordListVO）
/// </summary>
public class UserMessageListVO {
    /// <summary>
    /// 留言ID
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// 留言用户名称
    /// </summary>
    public string UserName { get; set; }

    /// <summary>
    /// 留言内容
    /// </summary>
    public string Content { get; set; }

    /// <summary>
    /// 是否通过（0：否，1：是）
    /// </summary>
    public int IsCheck { get; set; }

    /// <summary>
    /// 留言时间
    /// </summary>
    public DateTime CreateTime { get; set; }

    /// <summary>
    /// 更新时间
    /// </summary>
    public DateTime UpdateTime { get; set; }
}