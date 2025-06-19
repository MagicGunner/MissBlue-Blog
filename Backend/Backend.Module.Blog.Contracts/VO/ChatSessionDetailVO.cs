namespace Backend.Modules.Blog.Contracts.VO;

/// <summary>
/// 会话详情视图对象（ChatSessionDetailVO）
/// </summary>
public class ChatSessionDetailVO {
    /// <summary>
    /// 会话ID
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// 会话内容
    /// </summary>
    public string Conversation { get; set; }

    /// <summary>
    /// 会话创建时间
    /// </summary>
    public DateTime CreateTime { get; set; }
}