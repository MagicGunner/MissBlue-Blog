namespace Backend.Contracts.VO;

/// <summary>
/// 文章列表视图对象（ArticleListVO）
/// </summary>
public class ArticleListVO {
    /// <summary>
    /// 文章ID
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// 分类ID
    /// </summary>
    public long CategoryId { get; set; }

    /// <summary>
    /// 作者ID
    /// </summary>
    public long UserId { get; set; }

    /// <summary>
    /// 作者名称
    /// </summary>
    public string UserName { get; set; }

    /// <summary>
    /// 类型（1：原创，2：转载，3：翻译）
    /// </summary>
    public int ArticleType { get; set; }

    /// <summary>
    /// 分类名称
    /// </summary>
    public string CategoryName { get; set; }

    /// <summary>
    /// 标签名称列表
    /// </summary>
    public List<string> TagsName { get; set; }

    /// <summary>
    /// 文章缩略图
    /// </summary>
    public string ArticleCover { get; set; }

    /// <summary>
    /// 文章标题
    /// </summary>
    public string ArticleTitle { get; set; }

    /// <summary>
    /// 是否置顶（0：否，1：是）
    /// </summary>
    public int IsTop { get; set; }

    /// <summary>
    /// 文章状态（1：公开，2：私密，3：草稿）
    /// </summary>
    public int Status { get; set; }

    /// <summary>
    /// 访问量
    /// </summary>
    public long VisitCount { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreateTime { get; set; }
}