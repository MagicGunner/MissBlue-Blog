namespace Backend.Modules.Blog.Contracts.VO;

/// <summary>
/// 文章详情视图模型
/// </summary>
public class ArticleDetailVO {
    /// <summary>
    /// 文章ID
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// 作者ID
    /// </summary>
    public long UserId { get; set; }

    /// <summary>
    /// 分类名称
    /// </summary>
    public string CategoryName { get; set; }

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
    /// 文章内容
    /// </summary>
    public string ArticleContent { get; set; }

    /// <summary>
    /// 类型（1原创 2转载 3翻译）
    /// </summary>
    public int ArticleType { get; set; }

    /// <summary>
    /// 是否置顶（0否 1是）
    /// </summary>
    public int IsTop { get; set; }

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
    /// 上一篇文章ID
    /// </summary>
    public long? PreArticleId { get; set; }

    /// <summary>
    /// 上一篇文章标题
    /// </summary>
    public string PreArticleTitle { get; set; }

    /// <summary>
    /// 下一篇文章标题
    /// </summary>
    public string NextArticleTitle { get; set; }

    /// <summary>
    /// 下一篇文章ID
    /// </summary>
    public long? NextArticleId { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreateTime { get; set; }

    /// <summary>
    /// 更新时间
    /// </summary>
    public DateTime UpdateTime { get; set; }
}