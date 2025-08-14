using System.ComponentModel.DataAnnotations;

namespace Backend.Contracts.DTO;

public class ArticleDto {
    /// <summary>
    /// 文章 ID（更新时使用）
    /// </summary>
    public long? Id { get; set; }

    /// <summary>
    /// 分类 ID
    /// </summary>
    [Required(ErrorMessage = "分类id不能为空")]
    public long CategoryId { get; set; }

    /// <summary>
    /// 标签 ID 列表
    /// </summary>
    [Required(ErrorMessage = "标签id不能为空")]
    public List<long> TagId { get; set; }

    /// <summary>
    /// 文章封面图 URL
    /// </summary>
    [Required(ErrorMessage = "文章缩略图不能为空")]
    public string ArticleCover { get; set; }

    /// <summary>
    /// 文章标题
    /// </summary>
    [Required(ErrorMessage = "文章标题不能为空")]
    public string ArticleTitle { get; set; }

    /// <summary>
    /// 文章内容
    /// </summary>
    [Required(ErrorMessage = "文章内容不能为空")]
    public string ArticleContent { get; set; }

    /// <summary>
    /// 文章类型（1原创，2转载，3翻译）
    /// </summary>
    [Required(ErrorMessage = "文章类型不能为空")]
    public int ArticleType { get; set; }

    /// <summary>
    /// 是否置顶（0 否，1 是）
    /// </summary>
    [Required(ErrorMessage = "是否置顶不能为空")]
    public int IsTop { get; set; }

    /// <summary>
    /// 文章状态（1公开，2私密，3草稿）
    /// </summary>
    [Required(ErrorMessage = "文章状态不能为空")]
    public int Status { get; set; }
}