using Backend.Common.Results;
using Backend.Modules.Blog.Contracts.DTO;
using Backend.Modules.Blog.Contracts.VO;

namespace Backend.Modules.Blog.Contracts.IService;

public interface ICategoryService {
    Task<long>             Add(CategoryDto        categoryDTO);
    Task<bool>             DeleteByIds(List<long> ids);
    Task<bool>             Update(CategoryDto     categoryDTO);
    Task<List<CategoryVO>> ListAll();
    Task<CategoryVO?>      GetById(long                     id);
    Task<List<CategoryVO>> SearchCategory(SearchCategoryDTO searchCategoryDto);
}