using Backend.Domain.IRepository;
using Backend.Modules.Blog.Domain.Entities;

namespace Backend.Modules.Blog.Domain.IRepository;

public interface ICategoryRepository : IBaseRepositories<Category> {
    Task<string> GetNameByIdAsync(long categoryId);
}