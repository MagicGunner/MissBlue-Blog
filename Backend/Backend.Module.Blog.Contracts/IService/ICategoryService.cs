using Backend.Common.Results;
using Backend.Modules.Blog.Contracts.DTO;
using Backend.Modules.Blog.Contracts.VO;

namespace Backend.Modules.Blog.Contracts.IService;

public interface ICategoryService {
    Task<long>             AddAsync(CategoryDto        categoryDTO);
    Task<bool>             DeleteByIdsAsync(List<long> ids);
    Task<bool>             UpdateAsync(CategoryDto     categoryDTO);
    Task<List<CategoryVO>> ListAllAsync();
    Task<CategoryVO?>      GetByIdAsync(long                     id);
    Task<List<CategoryVO>> SearchCategoryAsync(SearchCategoryDTO searchCategoryDto);
}