namespace Backend.Contracts.VO;

/// <summary>
/// 分类下的文章视图对象
/// </summary>
public class CategoryArticleVO {
    /// <summary>
    /// 文章ID
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// 分类ID
    /// </summary>
    public long CategoryId { get; set; }

    /// <summary>
    /// 文章标签列表
    /// </summary>
    public List<TagVO> Tags { get; set; }

    /// <summary>
    /// 文章缩略图
    /// </summary>
    public string ArticleCover { get; set; }

    /// <summary>
    /// 文章标题
    /// </summary>
    public string ArticleTitle { get; set; }

    /// <summary>
    /// 访问量
    /// </summary>
    public long VisitCount { get; set; }

    /// <summary>
    /// 文章创建时间
    /// </summary>
    public DateTime CreateTime { get; set; }
}