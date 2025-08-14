namespace Backend.Contracts.DTO;

public class SearchTagDTO {
    /// <summary>
    /// 标签名称
    /// </summary>
    public string TagName { get; set; }

    /// <summary>
    /// 开始时间（建议改为 DateTime?）
    /// </summary>
    public string StartTime { get; set; }

    /// <summary>
    /// 结束时间（建议改为 DateTime?）
    /// </summary>
    public string EndTime { get; set; }
}