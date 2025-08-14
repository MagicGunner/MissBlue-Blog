namespace Backend.Contracts.VO;

/// <summary>
/// 文章卡片视图对象（ArticleVO）
/// </summary>
public class ArticleVO {
    /// <summary>
    /// 文章ID
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// 分类名称
    /// </summary>
    public string? CategoryName { get; set; }

    /// <summary>
    /// 文章标签
    /// </summary>
    public List<string>? Tags { get; set; }

    /// <summary>
    /// 文章缩略图
    /// </summary>
    public string? ArticleCover { get; set; }

    /// <summary>
    /// 文章标题
    /// </summary>
    public string? ArticleTitle { get; set; }

    /// <summary>
    /// 文章内容
    /// </summary>
    public string? ArticleContent { get; set; }

    /// <summary>
    /// 类型（1：原创，2：转载，3：翻译）
    /// </summary>
    public int ArticleType { get; set; }

    /// <summary>
    /// 访问量
    /// </summary>
    public long VisitCount { get; set; }

    /// <summary>
    /// 评论数
    /// </summary>
    public long CommentCount { get; set; }

    /// <summary>
    /// 点赞数
    /// </summary>
    public long LikeCount { get; set; }

    /// <summary>
    /// 收藏数
    /// </summary>
    public long FavoriteCount { get; set; }

    /// <summary>
    /// 文章创建时间
    /// </summary>
    public DateTime CreateTime { get; set; }

    /// <summary>
    /// 文章更新时间
    /// </summary>
    public DateTime UpdateTime { get; set; }
}