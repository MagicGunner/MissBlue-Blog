using Backend.Domain.Entity;
using SqlSugar;

namespace Backend.Modules.Blog.Domain.Entities;

[SugarTable("t_category")]
public class Category : RootEntity {
    // 分类名
    public string CategoryName { get; set; }

    // 创建时间（插入时自动填充）
    [SugarColumn(IsOnlyIgnoreInsert = false, IsOnlyIgnoreUpdate = true)]
    public DateTime CreateTime { get; set; }

    // 更新时间（插入和更新时自动填充）
    [SugarColumn(IsOnlyIgnoreInsert = false, IsOnlyIgnoreUpdate = false)]
    public DateTime UpdateTime { get; set; }

    // 是否删除（0：未删除，1：已删除）
    public int IsDeleted { get; set; }
}