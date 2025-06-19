using System.ComponentModel.DataAnnotations;

namespace Backend.Modules.Blog.Contracts.DTO;

/// <summary>
/// 聊天内容项 DTO
/// </summary>
public class ContentDto {
    /// <summary>
    /// 模型类型（限制为指定模型）
    /// </summary>
    [Required(ErrorMessage = "类型不能为空")]
    [RegularExpression(@"(gpt-3.5-turbo-0613|gpt-3.5-turbo-16k-0613|gpt-4|gpt-4-0613)",
                       ErrorMessage = "类型不符合规范")]
    public string Type { get; set; }

    /// <summary>
    /// 内容（最大长度 1314 字符）
    /// </summary>
    [StringLength(1314, ErrorMessage = "内容不能超过 1314 个字符")]
    public string Content { get; set; }
}