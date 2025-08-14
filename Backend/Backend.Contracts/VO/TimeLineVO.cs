namespace Backend.Contracts.VO;

/// <summary>
/// 文章时间线视图对象（TimeLineVO）
/// </summary>
public class TimeLineVO {
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
    /// 文章内容
    /// </summary>
    public string ArticleContent { get; set; }

    /// <summary>
    /// 文章创建时间
    /// </summary>
    public DateTime CreateTime { get; set; }
}