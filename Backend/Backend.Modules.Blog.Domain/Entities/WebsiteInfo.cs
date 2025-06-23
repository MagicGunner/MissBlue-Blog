using SqlSugar;

namespace Backend.Modules.Blog.Domain.Entities;

[SugarTable("sys_website_info")]
public class WebsiteInfo : RootEntity<long> {
    /// <summary>
    /// 站长头像
    /// </summary>
    public string WebmasterAvatar { get; set; }

    /// <summary>
    /// 站长名称
    /// </summary>
    public string WebmasterName { get; set; }

    /// <summary>
    /// 站长文案
    /// </summary>
    public string WebmasterCopy { get; set; }

    /// <summary>
    /// 站长资料卡背景图
    /// </summary>
    public string WebmasterProfileBackground { get; set; }

    /// <summary>
    /// Gitee 链接
    /// </summary>
    public string GiteeLink { get; set; }

    /// <summary>
    /// GitHub 链接
    /// </summary>
    public string GithubLink { get; set; }

    /// <summary>
    /// 网站名称
    /// </summary>
    public string WebsiteName { get; set; }

    /// <summary>
    /// 头部通知
    /// </summary>
    public string HeaderNotification { get; set; }

    /// <summary>
    /// 侧边公告
    /// </summary>
    public string SidebarAnnouncement { get; set; }

    /// <summary>
    /// 备案信息
    /// </summary>
    public string RecordInfo { get; set; }

    /// <summary>
    /// 网站开始运行时间
    /// </summary>
    public DateTime? StartTime { get; set; }

    /// <summary>
    /// 创建时间（自动填充）
    /// </summary>
    [SugarColumn(IsOnlyIgnoreInsert = false, IsOnlyIgnoreUpdate = true)]
    public DateTime CreateTime { get; set; }

    /// <summary>
    /// 更新时间（自动填充）
    /// </summary>
    [SugarColumn(IsOnlyIgnoreInsert = false, IsOnlyIgnoreUpdate = false)]
    public DateTime UpdateTime { get; set; }

    /// <summary>
    /// 是否删除（0 未删除，1 已删除）
    /// </summary>
    public int IsDeleted { get; set; }
}