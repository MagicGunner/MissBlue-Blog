namespace Backend.Modules.Blog.Contracts.VO;

/// <summary>
/// 用户详细信息视图对象（UserDetailsVO）
/// </summary>
public class UserDetailsVO {
    /// <summary>
    /// 用户ID
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// 用户昵称
    /// </summary>
    public string Nickname { get; set; }

    /// <summary>
    /// 用户名
    /// </summary>
    public string Username { get; set; }

    /// <summary>
    /// 用户角色列表
    /// </summary>
    public List<string> Roles { get; set; }

    /// <summary>
    /// 用户性别
    /// </summary>
    public int Gender { get; set; }

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
    /// 用户注册方式（1：邮箱/姓名，2：Gitee，3：GitHub）
    /// </summary>
    public int RegisterType { get; set; }

    /// <summary>
    /// 用户注册IP
    /// </summary>
    public string RegisterIp { get; set; }

    /// <summary>
    /// 用户注册地址
    /// </summary>
    public string RegisterAddress { get; set; }

    /// <summary>
    /// 用户登录方式（1：邮箱/姓名，2：QQ，3：Gitee，4：GitHub）
    /// </summary>
    public int LoginType { get; set; }

    /// <summary>
    /// 用户登录IP
    /// </summary>
    public string LoginIp { get; set; }

    /// <summary>
    /// 用户登录地址
    /// </summary>
    public string LoginAddress { get; set; }

    /// <summary>
    /// 是否禁用（0：否，1：是）
    /// </summary>
    public int IsDisable { get; set; }

    /// <summary>
    /// 最近登录时间
    /// </summary>
    public DateTime LoginTime { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreateTime { get; set; }

    /// <summary>
    /// 更新时间
    /// </summary>
    public DateTime UpdateTime { get; set; }
}