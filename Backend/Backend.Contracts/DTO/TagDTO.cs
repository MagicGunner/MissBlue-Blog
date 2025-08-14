using System.ComponentModel.DataAnnotations;

namespace Backend.Contracts.DTO;

public class TagDTO {
    /// <summary>
    /// 标签ID
    /// </summary>
    public long? Id { get; set; }

    /// <summary>
    /// 标签名称（必填，最长20个字符）
    /// </summary>
    [Required(ErrorMessage = "标签名称不能为空")]
    [MaxLength(20, ErrorMessage = "标签名称长度不能超过20")]
    public string TagName { get; set; }
}