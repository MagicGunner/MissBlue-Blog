using Backend.Domain.IRepository;
using Backend.Modules.Blog.Domain.Entities;

namespace Backend.Modules.Blog.Domain.IRepository;

public interface ICategoryRepository : IBaseRepositories<Category> {
    Task<Category> GetById(long categoryId);

    /// <summary>
    /// 返回分类ID和分类名的字典
    /// </summary>
    /// <param name="categoryIds">分类ID</param>
    /// <returns></returns>
    Task<Dictionary<long, string>> GetNameDic(List<long> categoryIds);

    /// <summary>
    /// 返回键位分类ID，值为该分类下文章的数量
    /// </summary>
    /// <param name="categoryIds"></param>
    /// <returns></returns>
    Task<Dictionary<long, long>> GetCountOfCategoryDic(List<long> categoryIds);

    Task<List<Category>> SearchCategory(string categoryName, string startTime, string endTime);
}