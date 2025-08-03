using Backend.Domain.IRepository;
using Backend.Modules.Blog.Domain.Entities;
using Backend.Modules.Blog.Domain.Enums;

namespace Backend.Modules.Blog.Domain.IRepository;

public interface ICommentRepository : IBaseRepositories<Comment> {
    Task<bool>                   AddComment(Comment      comment);
    Task<Dictionary<long, long>> GetCountDic(CommentType type,      List<long> typeIds);
    Task<long>                   GetCount(CommentType    type,      long       typeId);
    Task<List<Comment>>          GetBackList(string?     userName,  string?    content, int? type, int? isCheck);
    Task<Comment?>               SetChecked(long         commentId, int        isChecked);
}