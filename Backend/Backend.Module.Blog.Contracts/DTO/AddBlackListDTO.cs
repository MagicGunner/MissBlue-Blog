using System.ComponentModel.DataAnnotations;
using Backend.Common.Attributes;

namespace Backend.Modules.Blog.Contracts.DTO;

/// <summary>
/// 添加黑名单请求 DTO
/// </summary>
public class AddBlackListDto {
    /// <summary>
    /// 用户ID列表
    /// </summary>
    [Required(ErrorMessage = "用户Id不能为空")]
    [MinLength(1, ErrorMessage = "用户Id不能为空")]
    public List<long> UserIds { get; set; }

    /// <summary>
    /// 封禁理由
    /// </summary>
    [Required(ErrorMessage = "封禁理由不能为空")]
    [StringLength(200, ErrorMessage = "封禁理由不能超过200字符")]
    public string Reason { get; set; }

    /// <summary>
    /// 到期时间
    /// </summary>
    [Required(ErrorMessage = "封禁到期时间不能为空")]
    [FutureDate(ErrorMessage = "封禁到期时间必须大于当前时间")]
    public DateTime ExpiresTime { get; set; }
}