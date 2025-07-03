using Backend.Infrastructure.Repository;
using Backend.Infrastructure.UnitOfWorks;
using Backend.Modules.Blog.Domain.Entities;
using Backend.Modules.Blog.Domain.IRepository;

namespace Backend.Modules.Blog.Infrastructure.Repository;

public class CategoryRepository(IUnitOfWorkManage unitOfWorkManage) : BaseRepositories<Category>(unitOfWorkManage), ICategoryRepository {
    public async Task<string> GetNameByIdAsync(long categoryId) => (await Db.Queryable<Category>().Where(category => category.Id == categoryId).SingleAsync()).CategoryName;
}