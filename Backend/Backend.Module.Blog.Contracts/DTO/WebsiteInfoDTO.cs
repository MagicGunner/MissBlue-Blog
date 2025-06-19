using System.ComponentModel.DataAnnotations;

namespace Backend.Modules.Blog.Contracts.DTO;

public class WebsiteInfoDTO {
    /// <summary>网站名称</summary>
    [MaxLength(30, ErrorMessage = "网站名称字数不能超过30")]
    public string WebsiteName { get; set; }

    /// <summary>头部通知</summary>
    [MaxLength(100, ErrorMessage = "头部通知字数不能超过100")]
    public string HeaderNotification { get; set; }

    /// <summary>侧面公告</summary>
    [MaxLength(1000, ErrorMessage = "侧面公告字数不能超过1000")]
    public string SidebarAnnouncement { get; set; }

    /// <summary>备案信息</summary>
    [MaxLength(100, ErrorMessage = "备案信息字数不能超过100")]
    public string RecordInfo { get; set; }

    /// <summary>开始运行时间</summary>
    public DateTime? StartTime { get; set; }
}