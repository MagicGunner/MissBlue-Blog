namespace Backend.Contracts.VO;

/// <summary>
/// 热门文章视图对象（HotArticleVO）
/// </summary>
public class HotArticleVO {
    /// <summary>
    /// 文章ID
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// 文章标题
    /// </summary>
    public string ArticleTitle { get; set; }

    /// <summary>
    /// 访问量
    /// </summary>
    public long VisitCount { get; set; }
}