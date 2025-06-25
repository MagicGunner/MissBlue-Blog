using Backend.Domain.Entity;
using SqlSugar;

namespace Backend.Modules.Blog.Domain.Entities;

[SugarTable("t_favorite")]
public class Favorite : RootEntity {
    /// <summary>
    /// 收藏的用户ID
    /// </summary>
    public long UserId { get; set; }

    /// <summary>
    /// 收藏类型（1：文章，2：留言板）
    /// </summary>
    public int Type { get; set; }

    /// <summary>
    /// 类型ID（被收藏的文章或留言板的ID）
    /// </summary>
    public long TypeId { get; set; }

    /// <summary>
    /// 是否有效（0：否，1：是）
    /// </summary>
    public int IsCheck { get; set; }

    /// <summary>
    /// 收藏时间（插入时自动填充）
    /// </summary>
    [SugarColumn(IsOnlyIgnoreInsert = false, IsOnlyIgnoreUpdate = true)]
    public DateTime CreateTime { get; set; }
}