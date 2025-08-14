namespace Backend.Contracts.DTO;

public class SearchChatGptDTO {
    /// <summary>
    /// 聊天会话的用户姓名
    /// </summary>
    public string UserName { get; set; }

    /// <summary>
    /// 是否有效 (0否 1是)
    /// </summary>
    public int? IsCheck { get; set; }

    /// <summary>
    /// 开始时间（可选，建议使用 DateTime?）
    /// </summary>
    public string StartTime { get; set; }

    /// <summary>
    /// 结束时间（可选，建议使用 DateTime?）
    /// </summary>
    public string EndTime { get; set; }
}