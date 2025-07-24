namespace Backend.Modules.Blog.Contracts.VO;

public class BannersVO {
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
    public DateTime CreateTime { get; set; }
}