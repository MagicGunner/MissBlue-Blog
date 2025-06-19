namespace Backend.Modules.Blog.Contracts.VO;

/// <summary>
/// 角色用户视图对象（RoleUserVO）
/// </summary>
public class RoleUserVO {
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
    /// 用户邮箱
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    /// 是否禁用（0：否，1：是）
    /// </summary>
    public int IsDisable { get; set; }

    /// <summary>
    /// 用户创建时间
    /// </summary>
    public DateTime CreateTime { get; set; }
}