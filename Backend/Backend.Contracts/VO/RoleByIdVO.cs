namespace Backend.Contracts.VO;

/// <summary>
/// 角色详情视图对象（RoleByIdVO）
/// </summary>
public class RoleByIdVO {
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
    /// 排序值
    /// </summary>
    public long OrderNum { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string Remark { get; set; }

    /// <summary>
    /// 状态（0：正常，1：停用）
    /// </summary>
    public int Status { get; set; }

    /// <summary>
    /// 角色拥有的菜单权限ID列表
    /// </summary>
    public List<long> MenuIds { get; set; }
}