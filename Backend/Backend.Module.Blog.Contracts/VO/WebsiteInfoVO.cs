namespace Backend.Modules.Blog.Contracts.VO;

/// <summary>
/// 网站信息视图对象（WebsiteInfoVO）
/// </summary>
public class WebsiteInfoVO {
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
    /// 侧面公告
    /// </summary>
    public string SidebarAnnouncement { get; set; }

    /// <summary>
    /// 备案信息
    /// </summary>
    public string RecordInfo { get; set; }

    /// <summary>
    /// 开始运行时间
    /// </summary>
    public DateTime StartTime { get; set; }

    /// <summary>
    /// 文章的最后更新时间
    /// </summary>
    public DateTime LastUpdateTime { get; set; }

    /// <summary>
    /// 文章数目
    /// </summary>
    public long ArticleCount { get; set; }

    /// <summary>
    /// 分类数
    /// </summary>
    public long CategoryCount { get; set; }

    /// <summary>
    /// 评论数
    /// </summary>
    public long CommentCount { get; set; }

    /// <summary>
    /// 全站字数
    /// </summary>
    public long WordCount { get; set; }

    /// <summary>
    /// 访问次数
    /// </summary>
    public long VisitCount { get; set; }
}