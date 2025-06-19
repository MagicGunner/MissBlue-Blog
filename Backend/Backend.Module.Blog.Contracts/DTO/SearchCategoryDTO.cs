namespace Backend.Modules.Blog.Contracts.DTO;

public class SearchCategoryDTO {
    /// <summary>
    /// 分类名称
    /// </summary>
    public string CategoryName { get; set; }

    /// <summary>
    /// 开始时间（可选，建议使用 DateTime? 类型）
    /// </summary>
    public string StartTime { get; set; }

    /// <summary>
    /// 结束时间（可选，建议使用 DateTime? 类型）
    /// </summary>
    public string EndTime { get; set; }
}