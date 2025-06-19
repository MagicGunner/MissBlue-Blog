using SqlSugar;

namespace Backend.Domain.Entities;

[SugarTable("sys_role_menu")]
public class RoleMenu {
    /// <summary>
    /// 主键ID（自增）
    /// </summary>
    [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
    public int Id { get; set; }

    /// <summary>
    /// 角色ID
    /// </summary>
    public long RoleId { get; set; }

    /// <summary>
    /// 菜单ID
    /// </summary>
    public long MenuId { get; set; }

    /// <summary>
    /// 构造函数（用于快速 new RoleMenu(roleId, menuId)）
    /// </summary>
    public RoleMenu() { }

    public RoleMenu(long roleId, long menuId) {
        RoleId = roleId;
        MenuId = menuId;
    }
}