namespace Backend.Contracts.DTO;

public class SearchArticleDTO {
    /// <summary>
    /// 分类 ID
    /// </summary>
    public long? CategoryId { get; set; }

    /// <summary>
    /// 文章标题
    /// </summary>
    public string? ArticleTitle { get; set; }

    /// <summary>
    /// 文章状态 (1公开 2私密 3草稿)
    /// </summary>
    public int? Status { get; set; }

    /// <summary>
    /// 是否置顶 (0否 1是)
    /// </summary>
    public int? IsTop { get; set; }
}