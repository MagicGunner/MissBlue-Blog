using SqlSugar;

namespace Backend.Domain.Entities;

[SugarTable("t_user_message")]
public class UserMessage {
    /// <summary>
    /// 主键 ID，自增
    /// </summary>
    [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
    public long Id { get; set; }

    /// <summary>
    /// 留言用户 ID
    /// </summary>
    public long UserId { get; set; }

    /// <summary>
    /// 留言内容
    /// </summary>
    public string Content { get; set; }

    /// <summary>
    /// 是否通过审核（0 否，1 是）
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