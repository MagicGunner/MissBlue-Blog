namespace Backend.Modules.Blog.Contracts.VO;

/// <summary>
/// 用户列表视图对象（UserListVO）
/// </summary>
public class UserListVO {
    /// <summary>
    /// 用户ID
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// 用户名
    /// </summary>
    public string Username { get; set; }

    /// <summary>
    /// 用户头像
    /// </summary>
    public string Avatar { get; set; }

    /// <summary>
    /// 用户邮箱
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    /// 用户注册方式（1：邮箱/用户名，2：Gitee，3：GitHub）
    /// </summary>
    public int RegisterType { get; set; }

    /// <summary>
    /// 登录地址
    /// </summary>
    public string LoginAddress { get; set; }

    /// <summary>
    /// 是否禁用（0：否，1：是）
    /// </summary>
    public int IsDisable { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreateTime { get; set; }
}