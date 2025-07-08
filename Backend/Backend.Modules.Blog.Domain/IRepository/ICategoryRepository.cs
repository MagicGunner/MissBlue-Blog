using Backend.Domain.IRepository;
using Backend.Modules.Blog.Domain.Entities;

namespace Backend.Modules.Blog.Domain.IRepository;

public interface ICategoryRepository : IBaseRepositories<Category> {
    Task<Category>                 GetById(long          categoryId);
    Task<Dictionary<long, string>> GetNameDic(List<long> categoryIds);
}