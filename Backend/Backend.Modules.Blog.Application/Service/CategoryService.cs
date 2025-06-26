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

public class CategoryService(IMapper mapper, IBaseRepositories<Category> baseRepositories, IBaseServices<Category> baseServices) : ICategoryService {
    public async Task<long> AddAsync(CategoryDto categoryDto) => await baseServices.AddAsync(mapper.Map<Category>(categoryDto));

    public async Task<bool>             DeleteByIdsAsync(List<long> ids)         => await baseServices.DeleteByIdsAsync(ids);
    public async Task<bool>             UpdateAsync(CategoryDto     categoryDto) => await baseServices.UpdateAsync(mapper.Map<Category>(categoryDto));
    public async Task<List<CategoryVO>> ListAllAsync()                           => await baseServices.QueryAsync<CategoryVO>();
    public async Task<CategoryVO?>      GetByIdAsync(long id)                    => (await baseServices.QueryAsync<CategoryVO>(i => i.Id == id)).FirstOrDefault();

    public async Task<List<CategoryVO>> SearchCategoryAsync(SearchCategoryDTO searchCategoryDto) =>
        await baseServices.QueryAsync<CategoryVO>(category => category.CategoryName == searchCategoryDto.CategoryName);
}