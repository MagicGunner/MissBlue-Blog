using Backend.Infrastructure.Repository;
using Backend.Infrastructure.UnitOfWorks;
using Backend.Modules.Blog.Domain.Entities;
using Backend.Modules.Blog.Domain.IRepository;

namespace Backend.Modules.Blog.Infrastructure.Repository;

public class CategoryRepository(IUnitOfWorkManage unitOfWorkManage) : BaseRepositories<Category>(unitOfWorkManage), ICategoryRepository {
    public async Task<Dictionary<long, string>> GetNameDic(List<long> categoryIds) =>
        (await Db.Queryable<Category>().In(category => category.Id, categoryIds).ToListAsync())
       .ToDictionary(category => category.Id, category => category.CategoryName);

    public async Task<Dictionary<long, long>> GetCountOfCategoryDic(List<long> categoryIds) {
        var articles = await Db.Queryable<Article>().In(a => a.CategoryId, categoryIds).ToListAsync();
        var result = new Dictionary<long, long>();
        foreach (var article in articles) {
            if (result.TryGetValue(article.CategoryId, out var count)) result[article.CategoryId] = count + 1;
            else result[article.CategoryId] = 1;
        }

        return result;
    }

    public async Task<List<Category>> SearchCategory(string categoryName, string startTime, string endTime) {
        var query = Db.Queryable<Category>();
        // 1. 按名称模糊匹配
        if (!string.IsNullOrWhiteSpace(categoryName)) {
            query = query.Where(c => c.CategoryName.Contains(categoryName));
        }

        if (DateTime.TryParse(startTime, out var start)) query = query.Where(c => c.CreateTime >= start);
        if (DateTime.TryParse(startTime, out var end)) query = query.Where(c => c.CreateTime <= end);
        return await query.ToListAsync();
    }
}