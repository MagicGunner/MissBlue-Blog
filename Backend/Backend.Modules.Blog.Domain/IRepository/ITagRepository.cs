using Backend.Domain;
using Backend.Domain.IRepository;
using Backend.Modules.Blog.Domain.Entities;

namespace Backend.Modules.Blog.Domain.IRepository;

public interface ITagRepository : IBaseRepositories<Tag> {
    Task<Dictionary<long, string>> GetNameDic(List<long>        tagIds);
    Task<Dictionary<long, Tag>>    GetDicByArticleId(List<long> articleIds);
    Task<List<Tag>>                GetByIds(List<long>          tagIds);
}