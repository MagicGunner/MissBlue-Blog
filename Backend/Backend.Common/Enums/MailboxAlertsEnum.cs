namespace Backend.Common.Enums;

/// <summary>
/// 邮件通知类型枚举
/// </summary>
public class MailboxAlertsEnum {
    public static readonly MailboxAlertsEnum REGISTER = new("register", "欢迎注册Ruyu-Blog", "register-email-template", "注册邮箱");

    public static readonly MailboxAlertsEnum RESET = new("reset", "Ruyu-Blog重置密码", "reset-password-template", "重置密码邮箱");

    public static readonly MailboxAlertsEnum RESET_EMAIL = new("resetEmail", "Ruyu-Blog重置电子邮箱", "reset-email-template", "重置邮箱的邮箱");

    public static readonly MailboxAlertsEnum FRIEND_LINK_APPLICATION = new("friendLinkApplication", "Ruyu-Blog友链申请通知", "link-email-template", "友链申请邮箱");

    public static readonly MailboxAlertsEnum FRIEND_LINK_APPLICATION_PASS = new("friendLinkApplicationPass", "Ruyu-Blog友链审核通知", "email-getThrough-template", "友链审核通知");

    public static readonly MailboxAlertsEnum COMMENT_NOTIFICATION_EMAIL = new("commentNotificationEmail", "Ruyu-Blog--有新的评论", "comment-email-template", "有新的评论的邮箱提醒");

    public static readonly MailboxAlertsEnum REPLY_COMMENT_NOTIFICATION_EMAIL = new("replyCommentNotificationEmail", "Ruyu-Blog--有新的回复", "reply-comment-email-template", "有新的回复的邮箱提醒");

    public static readonly MailboxAlertsEnum MESSAGE_NOTIFICATION_EMAIL = new("messageNotificationEmail", "Ruyu-Blog--有新的留言", "message-email-template", "有新的留言提醒");

    // 属性
    public string CodeStr      { get; }
    public string Subject      { get; }
    public string TemplateName { get; }
    public string Desc         { get; }

    private MailboxAlertsEnum(string codeStr, string subject, string templateName, string desc) {
        CodeStr = codeStr;
        Subject = subject;
        TemplateName = templateName;
        Desc = desc;
    }

    /// <summary>
    /// 通过 codeStr 查找
    /// </summary>
    public static MailboxAlertsEnum? FromCode(string code) => All.FirstOrDefault(e => e.CodeStr == code);

    /// <summary>
    /// 所有枚举项
    /// </summary>
    public static readonly List<MailboxAlertsEnum> All = [
                                                             REGISTER,
                                                             RESET,
                                                             RESET_EMAIL,
                                                             FRIEND_LINK_APPLICATION,
                                                             FRIEND_LINK_APPLICATION_PASS,
                                                             COMMENT_NOTIFICATION_EMAIL,
                                                             REPLY_COMMENT_NOTIFICATION_EMAIL,
                                                             MESSAGE_NOTIFICATION_EMAIL
                                                         ];
}