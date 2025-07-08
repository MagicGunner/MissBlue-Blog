using Backend.Infrastructure.Repository;
using Backend.Infrastructure.UnitOfWorks;
using Backend.Modules.Blog.Domain.Entities;
using Backend.Modules.Blog.Domain.IRepository;

namespace Backend.Modules.Blog.Infrastructure.Repository;

public class TagRepository(IUnitOfWorkManage unitOfWorkManage) : BaseRepositories<Tag>(unitOfWorkManage), ITagRepository {
    public async Task<Dictionary<long, string>> GetNameDic(List<long> tagIds) =>
        (await Db.Queryable<Tag>().In(tag => tag.Id, tagIds).ToListAsync())
       .ToDictionary(tag => tag.Id, tag => tag.TagName);

    public async Task<Dictionary<long, Tag>> GetDicByArticleId(List<long> articleIds) {
        var result = new Dictionary<long, Tag>();
        var articleTags = await Db.Queryable<ArticleTag>().In(at => at.ArticleId, articleIds).ToListAsync();
        var tagIds = articleTags.Select(at => at.TagId).ToList();
        var tagDic = await Db.Queryable<Tag>().In(tag => tag.Id, tagIds).ToListAsync();

        foreach (var articleId in articleIds) {
        }
    }

    public async Task<List<Tag>> GetByIds(List<long> tagIds) => await Db.Queryable<Tag>().In(tag => tag.Id, tagIds).ToListAsync();
}