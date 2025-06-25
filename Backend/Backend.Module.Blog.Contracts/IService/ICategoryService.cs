using Backend.Common.Results;
using Backend.Modules.Blog.Contracts.DTO;
using Backend.Modules.Blog.Contracts.VO;

namespace Backend.Modules.Blog.Contracts.IService;

public interface ICategoryService {
    Task<ResponseResult<object>> AddAsync(CategoryDto        categoryDTO);
    Task<ResponseResult<object>> DeleteByIdsAsync(List<long> ids);
    Task<ResponseResult<object>> UpdateAsync(CategoryDto     categoryDTO);
    Task<List<CategoryVO>>       ListAllAsync();
    Task<CategoryVO?>            GetByIdAsync(long                     id);
    Task<List<CategoryVO>>       SearchCategoryAsync(SearchCategoryDTO searchCategoryDto);
}