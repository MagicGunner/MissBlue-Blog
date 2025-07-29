using Backend.Common.Results;
using Backend.Modules.Blog.Contracts.DTO;
using Backend.Modules.Blog.Contracts.VO;

namespace Backend.Modules.Blog.Contracts.IService;

public interface ICategoryService {
    Task<List<CategoryVO>> ListAll();
    Task<bool>             Add(CategoryDto                  categoryDto);
    Task<bool>             DeleteByIds(List<long>           ids);
    Task<bool>             Update(CategoryDto               categoryDto);
    Task<CategoryVO?>      GetById(long                     id);
    Task<List<CategoryVO>> SearchCategory(SearchCategoryDTO searchCategoryDto);
}