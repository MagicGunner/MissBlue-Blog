using Backend.Infrastructure.Repository;
using Backend.Infrastructure.UnitOfWorks;
using Backend.Modules.Blog.Domain.Entities;
using Backend.Modules.Blog.Domain.IRepository;

namespace Backend.Modules.Blog.Infrastructure.Repository;

public class CategoryRepository(IUnitOfWorkManage unitOfWorkManage) : BaseRepositories<Category>(unitOfWorkManage), ICategoryRepository {
    public async Task<Category> GetById(long categoryId) => await Db.Queryable<Category>().Where(category => category.Id == categoryId).SingleAsync();


    public async Task<Dictionary<long, string>> GetNameDic(List<long> categoryIds) =>
        (await Db.Queryable<Category>().In(category => category.Id, categoryIds).ToListAsync())
       .ToDictionary(category => category.Id, category => category.CategoryName);
}