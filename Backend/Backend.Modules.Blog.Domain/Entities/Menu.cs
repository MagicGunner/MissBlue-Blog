using SqlSugar;

namespace Backend.Modules.Blog.Domain.Entities;

[SugarTable("sys_menu")]
public class Menu : RootEntity<long> {
    /// <summary>
    /// 菜单标题
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// 图标
    /// </summary>
    public string Icon { get; set; }

    /// <summary>
    /// 路由路径
    /// </summary>
    public string Path { get; set; }

    /// <summary>
    /// 前端绑定组件（如 Iframe、RouteView）
    /// </summary>
    public string Component { get; set; }

    /// <summary>
    /// 父菜单默认跳转地址
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
    /// 路由名称（用于 keep-alive）
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 是否隐藏菜单（0 否，1 是）
    /// </summary>
    public int HideInMenu { get; set; }

    /// <summary>
    /// iframe 模式跳转地址
    /// </summary>
    public string Url { get; set; }

    /// <summary>
    /// 是否隐藏在面包屑（0 否，1 是）
    /// </summary>
    public int HideInBreadcrumb { get; set; }

    /// <summary>
    /// 是否隐藏子菜单（0 否，1 是）
    /// </summary>
    public int HideChildrenInMenu { get; set; }

    /// <summary>
    /// 是否缓存（0 否，1 是）
    /// </summary>
    public int KeepAlive { get; set; }

    /// <summary>
    /// 跳转方式（_blank/_self/_parent）
    /// </summary>
    public string Target { get; set; }

    /// <summary>
    /// 是否禁用（0 否，1 是）
    /// </summary>
    public int IsDisable { get; set; }

    /// <summary>
    /// 排序字段
    /// </summary>
    public int OrderNum { get; set; }

    /// <summary>
    /// 创建时间（自动填充）
    /// </summary>
    [SugarColumn(IsOnlyIgnoreInsert = false, IsOnlyIgnoreUpdate = true)]
    public DateTime CreateTime { get; set; }

    /// <summary>
    /// 更新时间（自动填充）
    /// </summary>
    [SugarColumn(IsOnlyIgnoreInsert = false, IsOnlyIgnoreUpdate = false)]
    public DateTime UpdateTime { get; set; }

    /// <summary>
    /// 是否删除（0 未删除，1 已删除）
    /// </summary>
    public int IsDeleted { get; set; }
}