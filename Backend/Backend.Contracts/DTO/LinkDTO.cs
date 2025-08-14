using System.ComponentModel.DataAnnotations;

namespace Backend.Contracts.DTO;

public class LinkDTO {
    /// <summary>
    /// 网站名称
    /// </summary>
    [StringLength(15, ErrorMessage = "网站名称不能超过15个字符")]
    public string Name { get; set; }

    /// <summary>
    /// 网站地址
    /// </summary>
    [StringLength(50, ErrorMessage = "网站地址不能超过50个字符")]
    public string Url { get; set; }

    /// <summary>
    /// 网站描述
    /// </summary>
    [StringLength(30, ErrorMessage = "网站描述不能超过30个字符")]
    public string Description { get; set; }

    /// <summary>
    /// 网站背景
    /// </summary>
    [StringLength(100, ErrorMessage = "网站背景不能超过100个字符")]
    public string Background { get; set; }

    /// <summary>
    /// 邮箱地址
    /// </summary>
    [EmailAddress(ErrorMessage = "邮箱格式不正确")]
    [StringLength(20, ErrorMessage = "邮箱地址不能超过20个字符")]
    public string Email { get; set; }
}