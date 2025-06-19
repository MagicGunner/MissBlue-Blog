using System.ComponentModel.DataAnnotations;

namespace Backend.Modules.Blog.Contracts.DTO;

/// <summary>
/// 聊天请求 DTO
/// </summary>
public class ChatDto {
    /// <summary>
    /// 聊天内容
    /// </summary>
    [Required(ErrorMessage = "聊天内容不能为空")]
    public List<ContentDto> Content { get; set; }

    /// <summary>
    /// 使用的模型名称
    /// </summary>
    [Required(ErrorMessage = "模型不能为空")]
    public string Model { get; set; }
}