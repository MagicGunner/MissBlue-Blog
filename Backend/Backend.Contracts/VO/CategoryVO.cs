namespace Backend.Contracts.VO;

/// <summary>
/// 分类视图对象（CategoryVO）
/// </summary>
public class CategoryVO {
    /// <summary>
    /// 分类ID
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// 分类名称
    /// </summary>
    public string CategoryName { get; set; }

    /// <summary>
    /// 分类下的文章数量
    /// </summary>
    public long ArticleCount { get; set; }

    /// <summary>
    /// 标签创建时间
    /// </summary>
    public DateTime CreateTime { get; set; }

    /// <summary>
    /// 标签更新时间
    /// </summary>
    public DateTime UpdateTime { get; set; }
}