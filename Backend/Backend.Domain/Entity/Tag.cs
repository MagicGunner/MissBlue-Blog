using SqlSugar;

namespace Backend.Domain.Entity;

[SugarTable("t_tag")]
public class Tag : RootEntity {
    /// <summary>
    /// 标签名称
    /// </summary>
    public string TagName { get; set; }

    [SugarColumn(ColumnDescription = "创建时间", IsNullable = false, InsertServerTime = true)]
    public DateTime CreateTime { get; set; }

    [SugarColumn(ColumnDescription = "更新时间", IsNullable = false, InsertServerTime = true, UpdateServerTime = true)]
    public DateTime UpdateTime { get; set; }

    /// <summary>
    /// 是否删除（0：未删除，1：已删除）
    /// </summary>
    public int IsDeleted { get; set; }
}