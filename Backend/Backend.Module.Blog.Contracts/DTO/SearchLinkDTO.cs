namespace Backend.Modules.Blog.Contracts.DTO;

public class SearchLinkDTO {
    /// <summary>
    /// 用户名称
    /// </summary>
    public string UserName { get; set; }

    /// <summary>
    /// 网站名称
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 是否通过 (0 否, 1 是)
    /// </summary>
    public int? IsCheck { get; set; }

    /// <summary>
    /// 开始时间（可替换为 DateTime? 类型）
    /// </summary>
    public string StartTime { get; set; }

    /// <summary>
    /// 结束时间（可替换为 DateTime? 类型）
    /// </summary>
    public string EndTime { get; set; }
}