namespace Backend.Modules.Blog.Contracts.VO;

/// <summary>
/// 登录日志视图对象（LoginLogVO）
/// </summary>
public class LoginLogVO {
    /// <summary>
    /// 日志编号
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// 用户名称
    /// </summary>
    public string UserName { get; set; }

    /// <summary>
    /// 登录 IP
    /// </summary>
    public string Ip { get; set; }

    /// <summary>
    /// 登录地址
    /// </summary>
    public string Address { get; set; }

    /// <summary>
    /// 浏览器
    /// </summary>
    public string Browser { get; set; }

    /// <summary>
    /// 操作系统
    /// </summary>
    public string Os { get; set; }

    /// <summary>
    /// 登录类型（0：前台，1：后台，2：非法登录）
    /// </summary>
    public int Type { get; set; }

    /// <summary>
    /// 登录状态（0：成功，1：失败）
    /// </summary>
    public int State { get; set; }

    /// <summary>
    /// 登录信息
    /// </summary>
    public string Message { get; set; }

    /// <summary>
    /// 用户登录时间
    /// </summary>
    public DateTime LoginTime { get; set; }
}