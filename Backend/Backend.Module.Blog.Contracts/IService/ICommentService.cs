using Backend.Modules.Blog.Contracts.DTO;
using Backend.Modules.Blog.Contracts.VO;

namespace Backend.Modules.Blog.Contracts.IService;

public interface ICommentService {
    Task<(bool IsSuccess, string? Msg)>  AddComment(UserCommentDTO userCommentDto);
    Task<bool>                           DeleteByIds(List<long>    ids);
    Task<bool>                           Update(UserCommentDTO     userCommentDto);
    Task<List<ArticleCommentVO>>         ListAll();
    Task<PageVO<List<ArticleCommentVO>>> GetComment(int                type, int typeId, int pageNum, int pageSize);
    Task<List<CommentListVO>>            Search(SearchCommentDTO       searchCommentDto);
    Task<List<CommentListVO>>            GetBackList(SearchCommentDTO? dto);

    Task<bool> IsChecked(CommentIsCheckDTO isCheckDto);
}