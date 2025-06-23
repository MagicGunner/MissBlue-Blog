using SqlSugar;

namespace Backend.Modules.Blog.Domain.Entities;

[SugarTable("t_tag")]
public class Tag : RootEntity<long> {
    /// <summary>
    /// 标签名称
    /// </summary>
    public string TagName { get; set; }

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