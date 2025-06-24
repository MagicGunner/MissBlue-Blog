using Backend.Common.Results;
using Backend.Modules.Blog.Contracts.DTO;
using Backend.Modules.Blog.Contracts.VO;

namespace Backend.Modules.Blog.Contracts.Interface;

public interface ITagService {
    Task<List<TagVO>> ListAllTagAsync();

    Task<ResponseResult<object>> AddTagAsync(TagDTO tagDTO);

    Task<List<TagVO>> SearchTagAsync(SearchTagDTO searchTagDTO);

    Task<TagVO?> GetTagByIdAsync(long id);

    Task<ResponseResult<object>> AddOrUpdateTagAsync(TagDTO tagDTO);

    Task<ResponseResult<object>> DeleteTagByIdsAsync(List<long> ids);
}