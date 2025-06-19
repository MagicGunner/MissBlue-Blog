using SqlSugar;

namespace Backend.Domain.Entities;

[SugarTable("sys_login_log")]
public class LoginLog {
    /// <summary>
    /// 日志编号（主键，自增）
    /// </summary>
    [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
    public long Id { get; set; }

    /// <summary>
    /// 用户名称
    /// </summary>
    public string UserName { get; set; }

    /// <summary>
    /// 登录 IP 地址
    /// </summary>
    public string Ip { get; set; }

    /// <summary>
    /// 登录地址（地理位置）
    /// </summary>
    public string Address { get; set; }

    /// <summary>
    /// 浏览器信息
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
    /// 登录信息（成功/失败描述）
    /// </summary>
    public string Message { get; set; }

    /// <summary>
    /// 创建时间（插入时自动填充）
    /// </summary>
    [SugarColumn(IsOnlyIgnoreInsert = false, IsOnlyIgnoreUpdate = true)]
    public DateTime CreateTime { get; set; }

    /// <summary>
    /// 更新时间（插入和更新时自动填充）
    /// </summary>
    [SugarColumn(IsOnlyIgnoreInsert = false, IsOnlyIgnoreUpdate = false)]
    public DateTime UpdateTime { get; set; }

    /// <summary>
    /// 是否删除（0：未删除，1：已删除）
    /// </summary>
    public int IsDeleted { get; set; }
}