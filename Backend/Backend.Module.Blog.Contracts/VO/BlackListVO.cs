using Backend.Common.IP;

namespace Backend.Modules.Blog.Contracts.VO;

/// <summary>
/// 黑名单列表视图对象（BlackListVO）
/// </summary>
public class BlackListVO {
    /// <summary>
    /// 表主键ID
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// 用户名称
    /// </summary>
    public string UserName { get; set; }

    /// <summary>
    /// 封禁理由
    /// </summary>
    public string Reason { get; set; }

    /// <summary>
    /// 自动封禁、IP信息
    /// </summary>
    public BlackListIpInfo IpInfo { get; set; }

    /// <summary>
    /// 封禁类型
    /// </summary>
    public int Type { get; set; }

    /// <summary>
    /// 封禁时间
    /// </summary>
    public DateTime BannedTime { get; set; }

    /// <summary>
    /// 到期时间
    /// </summary>
    public DateTime ExpiresTime { get; set; }

    /// <summary>
    /// 更新时间
    /// </summary>
    public DateTime UpdateTime { get; set; }
}