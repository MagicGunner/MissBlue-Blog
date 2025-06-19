using SqlSugar;

namespace Backend.Domain.Entities;

[SugarTable("t_photo")]
public class Photo {
    /// <summary>
    /// 自增ID（主键）
    /// </summary>
    [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
    public long Id { get; set; }

    /// <summary>
    /// 创建者用户ID
    /// </summary>
    public long UserId { get; set; }

    /// <summary>
    /// 名称（相册名或照片名）
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 描述说明
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// 类型（1：相册，2：照片）
    /// </summary>
    public int Type { get; set; }

    /// <summary>
    /// 父相册ID（用于树结构或分类）
    /// </summary>
    public long ParentId { get; set; }

    /// <summary>
    /// 图片URL地址
    /// </summary>
    public string Url { get; set; }

    /// <summary>
    /// 是否审核通过（0 否，1 是）
    /// </summary>
    public int IsCheck { get; set; }

    /// <summary>
    /// 图片大小（单位：KB）
    /// </summary>
    public double Size { get; set; }

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