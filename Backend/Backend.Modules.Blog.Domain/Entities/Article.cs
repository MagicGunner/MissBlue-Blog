using Backend.Domain.Entity;
using SqlSugar;

namespace Backend.Modules.Blog.Domain.Entities;

[SugarTable("t_article")]
public class Article : RootEntity {
    public long UserId { get; set; }

    public long CategoryId { get; set; }

    public string? ArticleCover { get; set; }

    public string? ArticleTitle { get; set; }

    public string? ArticleContent { get; set; }

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

    [SugarColumn(IsOnlyIgnoreInsert = false, IsOnlyIgnoreUpdate = true)]
    public DateTime CreateTime { get; set; }

    [SugarColumn(IsOnlyIgnoreInsert = true, IsOnlyIgnoreUpdate = false)]
    public DateTime UpdateTime { get; set; }

    public long IsDeleted { get; set; }
}