namespace Backend.Modules.Blog.Contracts.DTO;

public class SearchCommentDTO {
    /// <summary>
    /// 评论用户的名称
    /// </summary>
    public string CommentUserName { get; set; }

    /// <summary>
    /// 评论的内容
    /// </summary>
    public string CommentContent { get; set; }

    /// <summary>
    /// 评论类型 (1文章 2留言板)
    /// </summary>
    public int? Type { get; set; }

    /// <summary>
    /// 是否通过 (0否 1是)
    /// </summary>
    public int? IsCheck { get; set; }
}