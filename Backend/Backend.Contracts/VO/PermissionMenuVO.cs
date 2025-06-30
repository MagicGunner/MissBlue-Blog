namespace Backend.Contracts.VO;

/// <summary>
/// 权限菜单视图对象
/// </summary>
public class PermissionMenuVO : IEquatable<PermissionMenuVO> {
    /// <summary>
    /// 权限表ID
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// 菜单名称
    /// </summary>
    public string MenuName { get; set; }

    /// <summary>
    /// 菜单ID（用于去重）
    /// </summary>
    public long MenuId { get; set; }

    public override bool Equals(object obj) => Equals(obj as PermissionMenuVO);

    public bool Equals(PermissionMenuVO other) => other != null && MenuId == other.MenuId;

    public override int GetHashCode() => MenuId.GetHashCode();
}