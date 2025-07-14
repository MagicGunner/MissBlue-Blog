using Backend.Infrastructure.Repository;
using Backend.Infrastructure.UnitOfWorks;
using Backend.Modules.Blog.Domain.Entities;
using Backend.Modules.Blog.Domain.IRepository;
using Dm.util;

namespace Backend.Modules.Blog.Infrastructure.Repository;

public class TagRepository(IUnitOfWorkManage unitOfWorkManage) : BaseRepositories<Tag>(unitOfWorkManage), ITagRepository {
    public async Task<Dictionary<long, string>> GetNameDic(List<long> tagIds) =>
        (await Db.Queryable<Tag>().In(tag => tag.Id, tagIds).ToListAsync())
       .ToDictionary(tag => tag.Id, tag => tag.TagName);

    public async Task<Dictionary<long, List<Tag>>> GetDicByArticleId(List<long> articleIds) {
        var articleTags = await Db.Queryable<ArticleTag>().In(at => at.ArticleId, articleIds).ToListAsync();
        var tagIds = articleTags.Select(at => at.TagId).ToList();
        var tags = await Db.Queryable<Tag>().In(tag => tag.Id, tagIds).ToListAsync();
        var tagDic = tags.ToDictionary(tag => tag.Id, tag => tag);
        var articleTagIdDic = new Dictionary<long, List<long>>();
        foreach (var articleTag in articleTags) {
            if (articleTagIdDic.TryGetValue(articleTag.ArticleId, out var value)) {
                value.add(articleTag.TagId);
            } else {
                articleTagIdDic.Add(articleTag.ArticleId, [articleTag.TagId]);
            }
        }

        return articleTagIdDic.ToDictionary(dic => dic.Key, dic => dic.Value.Select(tagId => tagDic[tagId]).ToList());
    }

    public async Task<List<Tag>> GetByIds(List<long> tagIds) => await Db.Queryable<Tag>().In(tag => tag.Id, tagIds).ToListAsync();
}