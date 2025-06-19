namespace Backend.Modules.Blog.Contracts.VO;

/// <summary>
/// 根据ID获取菜单详情视图对象
/// </summary>
public class MenuByIdVO {
    /// <summary>
    /// 唯一ID
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// 标题
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// 图标
    /// </summary>
    public string Icon { get; set; }

    /// <summary>
    /// 关联的角色ID列表
    /// </summary>
    public List<long> RoleId { get; set; }

    /// <summary>
    /// 路由类型
    /// </summary>
    public long RouterType { get; set; }

    /// <summary>
    /// 路由路径
    /// </summary>
    public string Path { get; set; }

    /// <summary>
    /// 绑定的组件（默认：Iframe、RouteView、ComponentError）
    /// </summary>
    public string Component { get; set; }

    /// <summary>
    /// 父菜单的重定向地址（默认重定向到第一个子菜单）
    /// </summary>
    public string Redirect { get; set; }

    /// <summary>
    /// 是否固定页签（0 否，1 是）
    /// </summary>
    public int Affix { get; set; }

    /// <summary>
    /// 父级菜单ID
    /// </summary>
    public long ParentId { get; set; }

    /// <summary>
    /// 是否隐藏当前菜单（0 否，1 是）
    /// </summary>
    public int HideInMenu { get; set; }

    /// <summary>
    /// iframe 模式下的跳转 URL（不得与 Path 重复）
    /// </summary>
    public string Url { get; set; }

    /// <summary>
    /// 是否缓存（0 否，1 是）
    /// </summary>
    public int KeepAlive { get; set; }

    /// <summary>
    /// 全连接跳转模式（_blank | _self | _parent）
    /// </summary>
    public string Target { get; set; }

    /// <summary>
    /// 排序值
    /// </summary>
    public int OrderNum { get; set; }

    /// <summary>
    /// 是否禁用（0 否，1 是）
    /// </summary>
    public int IsDisable { get; set; }
}