namespace Backend.Modules.Blog.Contracts.VO;

/// <summary>
/// 菜单视图对象（MenuVO）
/// </summary>
public class MenuVO {
    /// <summary>
    /// 唯一ID
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// 菜单标题
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// 图标
    /// </summary>
    public string Icon { get; set; }

    /// <summary>
    /// 路径
    /// </summary>
    public string Path { get; set; }

    /// <summary>
    /// 绑定组件（默认：Iframe、RouteView、ComponentError）
    /// </summary>
    public string Component { get; set; }

    /// <summary>
    /// 父菜单重定向地址（默认重定向到第一个子菜单）
    /// </summary>
    public string Redirect { get; set; }

    /// <summary>
    /// 是否固定页签
    /// </summary>
    public bool Affix { get; set; }

    /// <summary>
    /// 父级菜单ID
    /// </summary>
    public long ParentId { get; set; }

    /// <summary>
    /// 路由名称（用于页面保活）
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 是否隐藏菜单
    /// </summary>
    public bool HideInMenu { get; set; }

    /// <summary>
    /// iframe 模式下的跳转 URL（不能与 Path 重复）
    /// </summary>
    public string Url { get; set; }

    /// <summary>
    /// 是否缓存页面
    /// </summary>
    public bool KeepAlive { get; set; }

    /// <summary>
    /// 打开方式（_blank、_self、_parent）
    /// </summary>
    public string Target { get; set; }

    /// <summary>
    /// 排序值
    /// </summary>
    public int OrderNum { get; set; }

    /// <summary>
    /// 是否禁用
    /// </summary>
    public bool IsDisable { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreateTime { get; set; }
}