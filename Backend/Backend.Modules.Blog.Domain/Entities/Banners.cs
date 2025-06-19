using SqlSugar;

namespace Backend.Domain.Entities;

[SugarTable("t_banners")]
public class Banners {
    // 主键ID，自增
    [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
    public long Id { get; set; }

    // 图片路径
    public string Path { get; set; }

    // 图片大小（字节）
    public long Size { get; set; }

    // 图片类型（MIME）
    public string Type { get; set; }

    // 上传人ID
    public long UserId { get; set; }

    // 图片排序
    public int SortOrder { get; set; }

    // 创建时间（插入时自动填充）
    [SugarColumn(IsOnlyIgnoreInsert = false, IsOnlyIgnoreUpdate = true)]
    public DateTime CreateTime { get; set; }
}