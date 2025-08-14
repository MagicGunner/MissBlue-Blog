using System.ComponentModel;

namespace Backend.Domain.Enums;

public enum LikeType {
    [Description("点赞：文章")]
    Article = 1,

    [Description("点赞：评论")]
    Comment = 2,

    [Description("点赞：留言")]
    LeaveWord = 3
}