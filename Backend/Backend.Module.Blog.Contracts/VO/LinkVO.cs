namespace Backend.Modules.Blog.Contracts.VO;

/// <summary>
/// 友链视图对象（LinkVO）
/// </summary>
public class LinkVO {
    /// <summary>
    /// 友链表 ID
    /// </summary>
    public long Id { get; set; }

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
    /// 用户头像
    /// </summary>
    public string Avatar { get; set; }
}