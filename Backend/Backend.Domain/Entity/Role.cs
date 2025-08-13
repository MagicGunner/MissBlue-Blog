using SqlSugar;

namespace Backend.Domain.Entity;

[SugarTable("sys_role")]
public class Role : RootEntity {
    /// <summary>
    /// 角色名称（如“管理员”、“编辑”）
    /// </summary>
    public string RoleName { get; set; }

    /// <summary>
    /// 角色标识（如“admin”、“editor”）
    /// </summary>
    public string? RoleKey { get; set; }

    /// <summary>
    /// 是否删除（0：未删除，1：已删除）
    /// </summary>
    public int IsDeleted { get; set; }

    /// <summary>
    /// 角色状态（0：正常，1：停用）
    /// </summary>
    public int Status { get; set; }

    /// <summary>
    /// 排序值（数字越小越靠前）
    /// </summary>
    public long OrderNum { get; set; }

    /// <summary>
    /// 备注信息
    /// </summary>
    public string Remark { get; set; }

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
}