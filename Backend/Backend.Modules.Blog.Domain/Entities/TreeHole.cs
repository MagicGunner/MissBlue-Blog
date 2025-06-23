using SqlSugar;

namespace Backend.Modules.Blog.Domain.Entities;

[SugarTable("t_tree_hole")]
public class TreeHole : RootEntity<long> {
    /// <summary>
    /// 发布用户ID
    /// </summary>
    public long UserId { get; set; }

    /// <summary>
    /// 树洞内容
    /// </summary>
    public string Content { get; set; }

    /// <summary>
    /// 是否审核通过（0 否，1 是）
    /// </summary>
    public int IsCheck { get; set; }

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