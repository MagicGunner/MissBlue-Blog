using Backend.Infrastructure.Repository;
using Backend.Infrastructure.UnitOfWorks;
using Backend.Modules.Blog.Domain.Entities;
using Backend.Modules.Blog.Domain.Enums;
using Backend.Modules.Blog.Domain.IRepository;
using SqlSugar;

namespace Backend.Modules.Blog.Infrastructure.Repository;

public class LikeRepository(IUnitOfWorkManage unitOfWorkManage) : BaseRepositories<Like>(unitOfWorkManage), ILikeRepository {
    public async Task<Dictionary<long, long>> GetCountDic(LikeType type, List<long> typeIds) {
        return (await Db.Queryable<Like>()
                        .Where(like => like.Type == (int)type && typeIds.Contains(like.TypeId))
                        .GroupBy(like => like.TypeId)
                        .Select(like => new {
                                                like.TypeId,
                                                Count = (long)SqlFunc.AggregateCount(like.Id)
                                            })
                        .ToListAsync()).ToDictionary(i => i.TypeId, i => i.Count);
    }

    public Task<List<Like>> IsLike(long userId, int type, long? typeId) {
        var query = Db.Queryable<Like>().Where(like => like.UserId == userId && like.Type == type);
        if (type is (int)LikeType.Comment or (int)LikeType.LeaveWord) {
            if (type == (int)LikeType.LeaveWord && typeId.HasValue) query = query.Where(like => like.TypeId == typeId.Value);
        }
    }
}