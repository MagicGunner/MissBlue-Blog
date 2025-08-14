using Backend.Domain.Entity;
using Backend.Domain.Enums;

namespace Backend.Domain.IRepository;

public interface ICommentRepository : IBaseRepositories<Comment> {
    Task<bool>                   AddComment(Comment      comment);
    Task<Dictionary<long, long>> GetCountDic(CommentType type,      List<long> typeIds);
    Task<long>                   GetCount(CommentType    type,      long       typeId);
    Task<List<Comment>>          GetBackList(string?     userName,  string?    content, int? type, int? isCheck);
    Task<Comment?>               SetChecked(long         commentId, int        isChecked);
}