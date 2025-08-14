using Backend.Contracts.DTO;
using Backend.Contracts.VO;

namespace Backend.Contracts.IService;

public interface ITagService {
    Task<long>        AddAsync(TagDTO             tagDTO);
    Task<bool>        DeleteByIdsAsync(List<long> ids);
    Task<bool>        UpdateAsync(TagDTO          tagDTO);
    Task<List<TagVO>> ListAllAsync();
    Task<TagVO?>      GetByIdAsync(long           id);
    Task<List<TagVO>> SearchTagAsync(SearchTagDTO searchTagDTO);
}