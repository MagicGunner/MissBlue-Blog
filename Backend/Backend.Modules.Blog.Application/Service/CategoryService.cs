using AutoMapper;
using Backend.Common.Results;
using Backend.Contracts;
using Backend.Domain;
using Backend.Modules.Blog.Contracts.DTO;
using Backend.Modules.Blog.Contracts.IService;
using Backend.Modules.Blog.Contracts.VO;
using Backend.Modules.Blog.Domain.Entities;
using SqlSugar;

namespace Backend.Modules.Blog.Application.Service;

public class CategoryService(IMapper mapper, IBaseRepositories<Category> baseRepositories, IBaseServices<Category, CategoryVO> baseServices) : ICategoryService {
    public ISqlSugarClient Db => baseRepositories.Db;

    public async Task<ResponseResult<object>> AddAsync(CategoryDto categoryDto) {
        var category = mapper.Map<Category>(categoryDto);
        var categoryId = await baseServices.AddAsync(category);
        return categoryId > 0 ? ResponseResult<object>.Success(categoryId) : ResponseResult<object>.Failure(msg: "添加失败");
    }

    public async Task<ResponseResult<object>> DeleteByIdsAsync(List<long> ids)                    => await baseServices.DeleteByIdsAsync(ids);
    public async Task<ResponseResult<object>> UpdateAsync(CategoryDto     categoryDto)            => await baseServices.UpdateAsync(mapper.Map<Category>(categoryDto));
    public async Task<List<CategoryVO>>       ListAllAsync()                                      => await baseServices.QueryAsync();
    public async Task<CategoryVO?>            GetByIdAsync(long                id)                => (await baseServices.QueryAsync(i => i.Id == id)).FirstOrDefault();
    public async Task<List<CategoryVO>> SearchCategoryAsync(SearchCategoryDTO searchCategoryDTO) => await baseServices.QueryAsync(category => category.CategoryName == searchCategoryDTO.CategoryName);
}