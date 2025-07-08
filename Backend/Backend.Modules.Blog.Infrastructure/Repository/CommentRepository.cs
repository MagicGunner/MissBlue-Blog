using Backend.Infrastructure.Repository;
using Backend.Infrastructure.UnitOfWorks;
using Backend.Modules.Blog.Domain.Entities;
using Backend.Modules.Blog.Domain.Enums;
using Backend.Modules.Blog.Domain.IRepository;
using SqlSugar;

namespace Backend.Modules.Blog.Infrastructure.Repository;

public class CommentRepository(IUnitOfWorkManage unitOfWorkManage) : BaseRepositories<Comment>(unitOfWorkManage), ICommentRepository {
    public async Task<Dictionary<long, long>> GetCountDic(CommentType type, List<long> typeIds) {
        return (await Db.Queryable<Comment>()
                        .Where(comment => comment.Type == (int)type && comment.IsCheck == 1 && typeIds.Contains(comment.TypeId))
                        .GroupBy(comment => comment.TypeId)
                        .Select(comment => new {
                                                   comment.TypeId,
                                                   Count = (long)SqlFunc.AggregateCount(comment.Id)
                                               })
                        .ToListAsync()).ToDictionary(i => i.TypeId, i => i.Count);
    }

    public async Task<long> GetCount(CommentType type, long typeId) => await Db.Queryable<Comment>().Where(c => c.Type == (int)type && c.IsCheck == 1 && c.TypeId == typeId).CountAsync();
}