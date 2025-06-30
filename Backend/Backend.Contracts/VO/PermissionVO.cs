namespace Backend.Contracts.VO;

/// <summary>
/// 权限视图对象（PermissionVO）
/// </summary>
public class PermissionVO {
    /// <summary>
    /// 权限表ID
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// 描述
    /// </summary>
    public string PermissionDesc { get; set; }

    /// <summary>
    /// 权限字符
    /// </summary>
    public string PermissionKey { get; set; }

    /// <summary>
    /// 菜单名称
    /// </summary>
    public string MenuName { get; set; }
}