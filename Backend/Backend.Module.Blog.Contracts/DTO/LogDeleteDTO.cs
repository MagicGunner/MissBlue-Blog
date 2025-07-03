using System.ComponentModel.DataAnnotations;

namespace Backend.Modules.Blog.Contracts.DTO;

public class LogDeleteDTO {
    /// <summary>
    /// 日志 ID 集合
    /// </summary>
    [Required(ErrorMessage = "日志ID列表不能为空")]
    public List<long> Ids { get; set; }
}