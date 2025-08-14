namespace Backend.Contracts.DTO;

public class SearchBlackListDTO {
    /// <summary>
    /// 用户名称
    /// </summary>
    public string UserName { get; set; }

    /// <summary>
    /// 封禁理由
    /// </summary>
    public string Reason { get; set; }

    /// <summary>
    /// 封禁类型
    /// </summary>
    public int? Type { get; set; }

    /// <summary>
    /// 封禁开始时间（字符串格式，建议使用 DateTime? 类型处理时间）
    /// </summary>
    public string StartTime { get; set; }

    /// <summary>
    /// 封禁结束时间
    /// </summary>
    public string EndTime { get; set; }
}