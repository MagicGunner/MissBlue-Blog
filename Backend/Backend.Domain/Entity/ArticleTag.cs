using SqlSugar;

namespace Backend.Domain.Entity;

[SugarTable("t_article_tag")]
public class ArticleTag : RootEntity {
    // 文章ID
    public long ArticleId { get; set; }

    // 标签ID
    public long TagId { get; set; }

    // 创建时间，在插入时自动填充
    [SugarColumn(IsOnlyIgnoreInsert = false, IsOnlyIgnoreUpdate = true)]
    public DateTime CreateTime { get; set; }

    // 是否删除（0未删，1已删）
    public int IsDeleted { get; set; }
}