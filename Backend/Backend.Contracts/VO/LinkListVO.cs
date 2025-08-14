namespace Backend.Contracts.VO;

/// <summary>
/// 友链列表视图对象（LinkListVO）
/// </summary>
public class LinkListVO {
    /// <summary>
    /// 友链表 ID
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// 用户名称
    /// </summary>
    public string UserName { get; set; }

    /// <summary>
    /// 网站名称
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 网站地址
    /// </summary>
    public string Url { get; set; }

    /// <summary>
    /// 网站描述
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// 网站背景
    /// </summary>
    public string Background { get; set; }

    /// <summary>
    /// 邮箱地址
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreateTime { get; set; }

    /// <summary>
    /// 审核状态（0：未通过，1：已通过）
    /// </summary>
    public int IsCheck { get; set; }
}