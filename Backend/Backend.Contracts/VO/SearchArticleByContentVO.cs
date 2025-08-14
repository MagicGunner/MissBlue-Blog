namespace Backend.Contracts.VO;

/// <summary>
/// 根据内容搜索的文章视图对象（SearchArticleByContentVO）
/// </summary>
public class SearchArticleByContentVO {
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

    /// <summary>
    /// 分类名称
    /// </summary>
    public string CategoryName { get; set; }

    /// <summary>
    /// 文章内容
    /// </summary>
    public string ArticleContent { get; set; }
}