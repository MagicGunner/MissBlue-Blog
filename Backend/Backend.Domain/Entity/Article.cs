using SqlSugar;

namespace Backend.Domain.Entity;

[SugarTable("t_article")]
public class Article : RootEntity {
    public long UserId { get; set; }

    public long CategoryId { get; set; }

    public string ArticleCover { get; set; }

    public string ArticleTitle { get; set; }

    public string ArticleContent { get; set; }

    /// <summary>
    /// 类型 (1原创 2转载 3翻译)
    /// </summary>
    public int ArticleType { get; set; }

    /// <summary>
    /// 是否置顶 (0否 1是)
    /// </summary>
    public int IsTop { get; set; }

    /// <summary>
    /// 文章状态 (1公开 2私密 3草稿)
    /// </summary>
    public int Status { get; set; }

    public long VisitCount { get; set; }

    public DateTime CreateTime { get; set; }

    public DateTime UpdateTime { get; set; }

    public long IsDeleted { get; set; }
}