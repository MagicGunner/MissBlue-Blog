using Backend.Common.Results;
using Backend.Modules.Blog.Contracts.DTO;
using Backend.Modules.Blog.Contracts.VO;

namespace Backend.Modules.Blog.Contracts.IService;

public interface ITagService {
    Task<long>        AddAsync(TagDTO             tagDTO);
    Task<bool>        DeleteByIdsAsync(List<long> ids);
    Task<bool>        UpdateAsync(TagDTO          tagDTO);
    Task<List<TagVO>> ListAllAsync();
    Task<TagVO?>      GetByIdAsync(long           id);
    Task<List<TagVO>> SearchTagAsync(SearchTagDTO searchTagDTO);
}