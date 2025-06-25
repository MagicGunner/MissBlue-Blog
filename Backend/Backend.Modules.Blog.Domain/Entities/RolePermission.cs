using Backend.Domain.Entity;
using SqlSugar;

namespace Backend.Modules.Blog.Domain.Entities;

[SugarTable("sys_role_permission")]
public class RolePermission : RootEntity {
    /// <summary>
    /// 角色ID
    /// </summary>
    public long RoleId { get; set; }

    /// <summary>
    /// 权限ID
    /// </summary>
    public long PermissionId { get; set; }

    /// <summary>
    /// 构造函数
    /// </summary>
    public RolePermission() { }

    public RolePermission(long roleId, long permissionId) {
        RoleId = roleId;
        PermissionId = permissionId;
    }
}