using Backend.Common.Results;
using Backend.Modules.Blog.Contracts.DTO;
using Backend.Modules.Blog.Contracts.VO;

namespace Backend.Modules.Blog.Contracts.IService;

public interface ITagService {
    Task<ResponseResult<object>> AddAsync(TagDTO             tagDTO);
    Task<ResponseResult<object>> DeleteByIdsAsync(List<long> ids);
    Task<ResponseResult<object>> UpdateAsync(TagDTO          tagDTO);
    Task<List<TagVO>>            ListAllAsync();
    Task<TagVO?>                 GetByIdAsync(long           id);
    Task<List<TagVO>>            SearchTagAsync(SearchTagDTO searchTagDTO);
}