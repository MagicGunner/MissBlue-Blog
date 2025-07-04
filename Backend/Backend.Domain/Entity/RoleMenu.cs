﻿using SqlSugar;

namespace Backend.Domain.Entity;

[SugarTable("sys_role_menu")]
public class RoleMenu : RootEntity {
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