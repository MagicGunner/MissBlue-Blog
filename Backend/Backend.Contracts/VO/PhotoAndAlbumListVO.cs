namespace Backend.Contracts.VO;

/// <summary>
/// 照片及相册列表视图对象
/// </summary>
public class PhotoAndAlbumListVO {
    /// <summary>
    /// 自增ID
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// 创建者ID
    /// </summary>
    public long UserId { get; set; }

    /// <summary>
    /// 名称
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 描述
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// 类型（1：相册，2：照片）
    /// </summary>
    public int Type { get; set; }

    /// <summary>
    /// 父相册ID
    /// </summary>
    public long ParentId { get; set; }

    /// <summary>
    /// 图片地址
    /// </summary>
    public string Url { get; set; }

    /// <summary>
    /// 是否通过（0：否，1：是）
    /// </summary>
    public int IsCheck { get; set; }

    /// <summary>
    /// 照片体积大小（KB）
    /// </summary>
    public double Size { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreateTime { get; set; }

    /// <summary>
    /// 相册封面
    /// </summary>
    public string AlbumCover { get; set; }
}