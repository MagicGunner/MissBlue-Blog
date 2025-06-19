namespace Backend.Modules.Blog.Contracts.DTO;

public class SearchUserMessageDTO {
    /// <summary>
    /// 留言用户
    /// </summary>
    public string UserName { get; set; }

    /// <summary>
    /// 是否通过 (0 否, 1 是)
    /// </summary>
    public int? IsCheck { get; set; }

    /// <summary>
    /// 开始时间（建议改为 DateTime? 类型，如果是字符串就保留）
    /// </summary>
    public string StartTime { get; set; }

    /// <summary>
    /// 结束时间（建议改为 DateTime? 类型，如果是字符串就保留）
    /// </summary>
    public string EndTime { get; set; }
}