using SqlSugar;

namespace Backend.Domain.Entities;

[SugarTable("sys_user_role")]
public class UserRole {
    /// <summary>
    /// 主键ID（自增）
    /// </summary>
    [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
    public long Id { get; set; }

    /// <summary>
    /// 用户ID
    /// </summary>
    public long UserId { get; set; }

    /// <summary>
    /// 角色ID
    /// </summary>
    public long RoleId { get; set; }

    /// <summary>
    /// 构造函数（用于快速初始化）
    /// </summary>
    public UserRole() { }

    public UserRole(long userId, long roleId) {
        UserId = userId;
        RoleId = roleId;
    }
}