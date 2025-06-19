namespace Backend.Modules.Blog.Contracts.VO;

/// <summary>
/// 前台用户账户视图对象（UserAccountVO）
/// </summary>
public class UserAccountVO {
    /// <summary>
    /// 用户昵称
    /// </summary>
    public string Nickname { get; set; }

    /// <summary>
    /// 用户名
    /// </summary>
    public string Username { get; set; }

    /// <summary>
    /// 用户类型（1：邮箱/姓名，2：Gitee，3：GitHub）
    /// </summary>
    public int RegisterType { get; set; }

    /// <summary>
    /// 用户头像
    /// </summary>
    public string Avatar { get; set; }

    /// <summary>
    /// 个人简介
    /// </summary>
    public string Intro { get; set; }

    /// <summary>
    /// 用户邮箱
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    /// 用户性别
    /// </summary>
    public int Gender { get; set; }

    /// <summary>
    /// 账号角色
    /// </summary>
    public List<string> Roles { get; set; }

    /// <summary>
    /// 账号权限
    /// </summary>
    public List<string> Permissions { get; set; }

    /// <summary>
    /// 用户最近登录时间
    /// </summary>
    public DateTime LoginTime { get; set; }

    /// <summary>
    /// 用户创建时间
    /// </summary>
    public DateTime CreateTime { get; set; }
}