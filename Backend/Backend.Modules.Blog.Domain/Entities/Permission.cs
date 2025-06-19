using SqlSugar;

namespace Backend.Domain.Entities;

[SugarTable("sys_permission")]
public class Permission {
    /// <summary>
    /// 权限ID（主键，自增）
    /// </summary>
    [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
    public int Id { get; set; }

    /// <summary>
    /// 权限描述（例如“新增用户”）
    /// </summary>
    public string PermissionDesc { get; set; }

    /// <summary>
    /// 权限标识符（例如“user:add”）
    /// </summary>
    public string PermissionKey { get; set; }

    /// <summary>
    /// 所属菜单ID（绑定具体菜单）
    /// </summary>
    public long MenuId { get; set; }

    /// <summary>
    /// 创建时间（插入时自动填充）
    /// </summary>
    [SugarColumn(IsOnlyIgnoreInsert = false, IsOnlyIgnoreUpdate = true)]
    public DateTime CreateTime { get; set; }

    /// <summary>
    /// 更新时间（插入和更新时自动填充）
    /// </summary>
    [SugarColumn(IsOnlyIgnoreInsert = false, IsOnlyIgnoreUpdate = false)]
    public DateTime UpdateTime { get; set; }

    /// <summary>
    /// 是否删除（0：未删除，1：已删除）
    /// </summary>
    public int IsDeleted { get; set; }
}