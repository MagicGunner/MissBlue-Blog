namespace Backend.Modules.Blog.Contracts.VO;

/// <summary>
/// 相关文章视图对象（RelatedArticleVO）
/// </summary>
public class RelatedArticleVO {
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
    /// 文章创建时间
    /// </summary>
    public DateTime CreateTime { get; set; }
}