namespace Backend.Contracts.VO;

/// <summary>
/// 全部角色视图对象（RoleAllVO）
/// </summary>
public class RoleAllVO {
    /// <summary>
    /// 角色ID
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// 角色名称
    /// </summary>
    public string RoleName { get; set; }

    /// <summary>
    /// 角色标识
    /// </summary>
    public string RoleKey { get; set; }

    /// <summary>
    /// 状态（0：正常，1：停用）
    /// </summary>
    public int Status { get; set; }

    /// <summary>
    /// 排序值
    /// </summary>
    public long OrderNum { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreateTime { get; set; }
}