using Backend.Modules.Blog.Contracts.DTO;
using Backend.Modules.Blog.Contracts.VO;

namespace Backend.Modules.Blog.Contracts.IService;

public interface ICommentService {
    Task<(bool IsSuccess, string? Msg)>  AddComment(UserCommentDTO     userCommentDto);
    Task<PageVO<List<ArticleCommentVO>>> GetComment(int                type, int typeId, int pageNum, int pageSize);
    Task<List<CommentListVO>>            GetBackList(SearchCommentDTO? dto);
    Task<bool>                           IsChecked(CommentIsCheckDTO   isCheckDto);

    Task<(bool isSuccess, string? msg)> Delete(long commentId);
}