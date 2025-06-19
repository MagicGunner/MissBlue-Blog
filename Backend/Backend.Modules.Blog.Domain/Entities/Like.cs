using SqlSugar;

namespace Backend.Domain.Entities;

[SugarTable("t_like")]
public class Like {
    /// <summary>
    /// 点赞ID（主键，自增）
    /// </summary>
    [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
    public long Id { get; set; }

    /// <summary>
    /// 点赞用户ID
    /// </summary>
    public long UserId { get; set; }

    /// <summary>
    /// 点赞类型（1：文章，2：评论）
    /// </summary>
    public int Type { get; set; }

    /// <summary>
    /// 点赞对象的ID（如文章ID或评论ID）
    /// </summary>
    public int TypeId { get; set; }

    /// <summary>
    /// 点赞时间（插入时自动填充）
    /// </summary>
    [SugarColumn(IsOnlyIgnoreInsert = false, IsOnlyIgnoreUpdate = true)]
    public DateTime CreateTime { get; set; }

    /// <summary>
    /// 更新时间（插入和更新时自动填充）
    /// </summary>
    [SugarColumn(IsOnlyIgnoreInsert = false, IsOnlyIgnoreUpdate = false)]
    public DateTime UpdateTime { get; set; }
}