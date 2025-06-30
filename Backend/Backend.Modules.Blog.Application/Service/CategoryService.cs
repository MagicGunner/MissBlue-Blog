using AutoMapper;
using Backend.Common.Results;
using Backend.Contracts;
using Backend.Contracts.IService;
using Backend.Domain;
using Backend.Modules.Blog.Contracts.DTO;
using Backend.Modules.Blog.Contracts.IService;
using Backend.Modules.Blog.Contracts.VO;
using Backend.Modules.Blog.Domain.Entities;
using SqlSugar;

namespace Backend.Modules.Blog.Application.Service;

public class CategoryService(IMapper mapper, IBaseRepositories<Category> baseRepositories, IBaseServices<Category> baseServices) : ICategoryService {
    public async Task<long> AddAsync(CategoryDto categoryDto) => await baseServices.Add(mapper.Map<Category>(categoryDto));

    public async Task<bool>             DeleteByIdsAsync(List<long> ids)         => await baseServices.DeleteByIds(ids);
    public async Task<bool>             UpdateAsync(CategoryDto     categoryDto) => await baseServices.Update(mapper.Map<Category>(categoryDto));
    public async Task<List<CategoryVO>> ListAllAsync()                           => await baseServices.Query<CategoryVO>();
    public async Task<CategoryVO?>      GetByIdAsync(long id)                    => (await baseServices.Query<CategoryVO>(i => i.Id == id)).FirstOrDefault();

    public async Task<List<CategoryVO>> SearchCategoryAsync(SearchCategoryDTO searchCategoryDto) =>
        await baseServices.Query<CategoryVO>(category => category.CategoryName == searchCategoryDto.CategoryName);
}