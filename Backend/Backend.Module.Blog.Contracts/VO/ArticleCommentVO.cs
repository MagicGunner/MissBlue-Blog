namespace Backend.Modules.Blog.Contracts.VO;

public class ArticleCommentVO {
    public long     Id             { get; set; }
    public int      CommentType    { get; set; }
    public int      TypeId         { get; set; }
    public long?    ParentId       { get; set; }
    public long?    ReplyId        { get; set; }
    public string   CommentContent { get; set; }
    public long     CommentUserId  { get; set; }
    public long?    ReplyUserId    { get; set; }
    public DateTime CreateTime     { get; set; }

    public string CommentUserNickname { get; set; }
    public string CommentUserAvatar   { get; set; }
    public string ReplyUserNickname   { get; set; }

    public long LikeCount          { get; set; }
    public long ChildCommentCount  { get; set; }
    public long ParentCommentCount { get; set; }

    public List<ArticleCommentVO> ChildComment { get; set; }
}