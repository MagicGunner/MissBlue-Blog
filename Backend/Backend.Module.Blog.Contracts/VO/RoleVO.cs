namespace Backend.Modules.Blog.Contracts.VO;

/// <summary>
/// 角色视图对象（RoleVO）
/// </summary>
public class RoleVO {
    /// <summary>
    /// 角色ID
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// 角色名称
    /// </summary>
    public string RoleName { get; set; }
}