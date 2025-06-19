namespace Backend.Modules.Blog.Contracts.VO;

/// <summary>
/// 会话视图对象（ChatSessionVO）
/// </summary>
public class ChatSessionVO {
    /// <summary>
    /// 会话ID
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// 会话标题
    /// </summary>
    public string ConversationTitle { get; set; }

    /// <summary>
    /// 会话创建时间
    /// </summary>
    public DateTime CreateTime { get; set; }
}