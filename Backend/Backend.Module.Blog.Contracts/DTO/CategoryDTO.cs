using System.ComponentModel.DataAnnotations;

namespace Backend.Modules.Blog.Contracts.DTO;

/// <summary>
/// 分类 DTO（用于新增/更新）
/// </summary>
public class CategoryDto {
    /// <summary>
    /// 分类 ID（更新时使用）
    /// </summary>
    public long? Id { get; set; }

    /// <summary>
    /// 分类名称
    /// </summary>
    [Required(ErrorMessage = "分类名称不能为空")]
    [StringLength(20, ErrorMessage = "分类名称长度不能超过20")]
    public string CategoryName { get; set; }
}