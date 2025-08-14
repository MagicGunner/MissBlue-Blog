namespace Backend.Contracts.DTO;

public class SearchTreeHoleDTO {
    /// <summary>
    /// 树洞用户
    /// </summary>
    public string UserName { get; set; }

    /// <summary>
    /// 是否通过（0 否，1 是）
    /// </summary>
    public int? IsCheck { get; set; }

    /// <summary>
    /// 开始时间（建议改为 DateTime? 类型）
    /// </summary>
    public string StartTime { get; set; }

    /// <summary>
    /// 结束时间（建议改为 DateTime? 类型）
    /// </summary>
    public string EndTime { get; set; }
}