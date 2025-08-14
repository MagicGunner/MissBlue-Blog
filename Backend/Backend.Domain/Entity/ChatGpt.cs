using SqlSugar;

namespace Backend.Domain.Entity;

[SugarTable("t_chat_gpt")]
public class ChatGpt : RootEntity {
    /// <summary>
    /// 所属用户 ID
    /// </summary>
    public long UserId { get; set; }

    /// <summary>
    /// 会话内容（建议存储为 JSON 字符串）
    /// </summary>
    public string Conversation { get; set; }

    /// <summary>
    /// 是否有效（0：无效，1：有效）
    /// </summary>
    public int IsCheck { get; set; }

    /// <summary>
    /// 创建时间（插入时自动填充）
    /// </summary>
    [SugarColumn(IsOnlyIgnoreInsert = false, IsOnlyIgnoreUpdate = true)]
    public DateTime CreateTime { get; set; }

    /// <summary>
    /// 修改时间（插入和更新时自动填充）
    /// </summary>
    [SugarColumn(IsOnlyIgnoreInsert = false, IsOnlyIgnoreUpdate = false)]
    public DateTime UpdateTime { get; set; }

    /// <summary>
    /// 是否删除（0：未删除，1：已删除）
    /// 启用逻辑删除需配合 db.QueryFilter
    /// </summary>
    public int IsDeleted { get; set; }
}