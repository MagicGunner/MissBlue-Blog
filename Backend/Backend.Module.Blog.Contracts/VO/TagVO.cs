namespace Backend.Modules.Blog.Contracts.VO;

/// <summary>
/// 标签视图对象（TagVO）
/// </summary>
public class TagVO {
    /// <summary>
    /// 标签ID
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// 标签名称
    /// </summary>
    public string TagName { get; set; }

    /// <summary>
    /// 标签下的文章数量
    /// </summary>
    public long ArticleCount { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreateTime { get; set; }

    /// <summary>
    /// 更新时间
    /// </summary>
    public DateTime UpdateTime { get; set; }
}