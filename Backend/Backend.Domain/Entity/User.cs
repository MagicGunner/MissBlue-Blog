using SqlSugar;

namespace Backend.Domain.Entity;

[SugarTable("sys_user")]
public class User : RootEntity {
    /// <summary>
    /// 用户昵称
    /// </summary>
    public string Nickname { get; set; }

    /// <summary>
    /// 用户名（登录名）
    /// </summary>
    public string Username { get; set; }

    /// <summary>
    /// 登录密码（建议加密存储）
    /// </summary>
    public string Password { get; set; }

    /// <summary>
    /// 性别（0：未定义，1：男，2：女）
    /// </summary>
    public int Gender { get; set; }

    /// <summary>
    /// 用户头像URL
    /// </summary>
    public string Avatar { get; set; }

    /// <summary>
    /// 个人简介
    /// </summary>
    public string Intro { get; set; }

    /// <summary>
    /// 用户邮箱
    /// </summary>
    public string? Email { get; set; }

    /// <summary>
    /// 注册方式（0：邮箱/用户名，1：Gitee，2：GitHub）
    /// </summary>
    public int RegisterType { get; set; }

    /// <summary>
    /// 注册IP
    /// </summary>
    public string RegisterIp { get; set; }

    /// <summary>
    /// 注册地理位置
    /// </summary>
    public string RegisterAddress { get; set; }

    /// <summary>
    /// 登录方式（0：邮箱/用户名，1：Gitee，2：GitHub）
    /// </summary>
    public int LoginType { get; set; }

    /// <summary>
    /// 登录IP
    /// </summary>
    public string LoginIp { get; set; }

    /// <summary>
    /// 登录地址
    /// </summary>
    public string LoginAddress { get; set; }

    /// <summary>
    /// 是否禁用（0 否，1 是）
    /// </summary>
    public int IsDisable { get; set; }

    /// <summary>
    /// 最近登录时间
    /// </summary>
    public DateTime? LoginTime { get; set; }

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