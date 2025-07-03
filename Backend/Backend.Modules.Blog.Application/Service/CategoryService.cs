using AutoMapper;
using Backend.Application.Service;
using Backend.Common.Results;
using Backend.Contracts;
using Backend.Contracts.IService;
using Backend.Domain;
using Backend.Domain.IRepository;
using Backend.Modules.Blog.Contracts.DTO;
using Backend.Modules.Blog.Contracts.IService;
using Backend.Modules.Blog.Contracts.VO;
using Backend.Modules.Blog.Domain.Entities;
using SqlSugar;

namespace Backend.Modules.Blog.Application.Service;

public class CategoryService(IMapper mapper, IBaseRepositories<Category> baseRepositories) : BaseServices<Category>(mapper, baseRepositories), ICategoryService {
    private readonly IMapper                _mapper = mapper;
    public async     Task<long>             Add(CategoryDto    categoryDto)                     => await Add(_mapper.Map<Category>(categoryDto));
    public async     Task<bool>             Update(CategoryDto categoryDto)                     => await Update(_mapper.Map<Category>(categoryDto));
    public async     Task<List<CategoryVO>> ListAll()                                           => await Query<CategoryVO>();
    public async     Task<CategoryVO?>      GetById(long                     id)                => (await Query<CategoryVO>(i => i.Id == id)).FirstOrDefault();
    public async     Task<List<CategoryVO>> SearchCategory(SearchCategoryDTO searchCategoryDto) => await Query<CategoryVO>(category => category.CategoryName == searchCategoryDto.CategoryName);
}