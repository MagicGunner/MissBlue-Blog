namespace Backend.Contracts.VO;

/// <summary>
/// 随机文章视图对象（RandomArticleVO）
/// </summary>
public class RandomArticleVO {
    /// <summary>
    /// 文章ID
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// 文章缩略图
    /// </summary>
    public string ArticleCover { get; set; }

    /// <summary>
    /// 文章标题
    /// </summary>
    public string ArticleTitle { get; set; }

    /// <summary>
    /// 文章访问量
    /// </summary>
    public long VisitCount { get; set; }

    /// <summary>
    /// 文章创建时间
    /// </summary>
    public DateTime CreateTime { get; set; }
}