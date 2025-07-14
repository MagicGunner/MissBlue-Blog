using Backend.Domain;
using Backend.Domain.IRepository;
using Backend.Modules.Blog.Domain.Entities;

namespace Backend.Modules.Blog.Domain.IRepository;

public interface ITagRepository : IBaseRepositories<Tag> {
    Task<Dictionary<long, string>> GetNameDic(List<long> tagIds);

    /// <summary>
    /// 获取相关Article的所有Tag
    /// </summary>
    /// <param name="articleIds"></param>
    /// <returns></returns>
    Task<Dictionary<long, List<Tag>>> GetDicByArticleId(List<long> articleIds);

    Task<List<Tag>> GetByIds(List<long> tagIds);
}