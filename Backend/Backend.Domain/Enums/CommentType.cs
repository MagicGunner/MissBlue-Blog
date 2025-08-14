using System.ComponentModel;

namespace Backend.Domain.Enums;

public enum CommentType {
    [Description("评论类型(1,文章)")]
    Article = 1,

    [Description("评论类型(2,留言板)")]
    LeaveWord = 2
}