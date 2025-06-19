namespace Backend.Modules.Blog.Contracts.DTO;

public class SearchFavoriteDTO {
    /// <summary>
    /// 收藏的用户姓名
    /// </summary>
    public string UserName { get; set; }

    /// <summary>
    /// 收藏类型 (1：文章，2：留言板)
    /// </summary>
    public int? Type { get; set; }

    /// <summary>
    /// 是否有效 (0否 1是)
    /// </summary>
    public int? IsCheck { get; set; }

    /// <summary>
    /// 开始时间（字符串格式，建议实际项目中用 DateTime 类型）
    /// </summary>
    public string StartTime { get; set; }

    /// <summary>
    /// 结束时间（字符串格式，建议实际项目中用 DateTime 类型）
    /// </summary>
    public string EndTime { get; set; }
}