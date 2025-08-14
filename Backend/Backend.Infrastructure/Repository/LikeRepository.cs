using System.Linq.Expressions;
using Backend.Domain.Entity;
using Backend.Domain.Enums;
using Backend.Domain.IRepository;
using Backend.Infrastructure.UnitOfWorks;
using SqlSugar;

namespace Backend.Infrastructure.Repository;

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

    public async Task<long> GetCount(LikeType type, long typeId) => await Db.Queryable<Like>().Where(l => l.Type == (int)type && l.TypeId == typeId).CountAsync();

    public async Task<List<Like>> IsLike(long userId, int type, long? typeId) {
        var query = Db.Queryable<Like>().Where(like => like.UserId == userId && like.Type == type);
        if (type != (int)LikeType.Comment && typeId.HasValue) {
            query = query.Where(like => like.TypeId == typeId.Value);
        }

        return await query.ToListAsync();
    }

    public async Task<bool> SetLiked(long userId, int type, long typeId) {
        // 查询是否已经点赞
        var existing = await Db.Queryable<Like>().Where(like => like.UserId == userId && like.Type == type && like.TypeId == typeId).FirstAsync();
        if (existing != null) { // 存在点赞记录返回失败
            return false;
        }

        var like = new Like {
                                UserId = userId,
                                Type = type,
                                TypeId = typeId,
                                CreateTime = DateTime.Now
                            };
        // todo 如果是文章点赞类型，更新 Redis 点赞数
        // if (type == (int)LikeEnum.LikeTypeArticle) {
        //     await _redisClient.HashIncrementAsync(RedisConst.ARTICLE_LIKE_COUNT, typeId.ToString(), 1);
        // }

        return await Db.Insertable(like).ExecuteCommandAsync() > 0;
    }

    public async Task<bool> UnSetLiked(long userId, int type, long typeId) {
        Expression<Func<Like, bool>> predicate = like => like.UserId == userId && like.Type == type && like.TypeId == typeId;

        // 查询是否已经点赞
        var existing = await Db.Queryable<Like>().Where(predicate).FirstAsync();
        if (existing == null) { // 不存在点赞记录返回失败
            return false;
        }

        // todo 如果是文章类型，更新 Redis 点赞数 -1
        // if (type == (int)LikeEnum.LikeTypeArticle)
        // {
        //     await _redisClient.HashIncrementAsync(RedisConst.ARTICLE_LIKE_COUNT, typeId.ToString(), 1);
        // }
        return await Db.Deleteable<Like>().Where(predicate).ExecuteCommandAsync() > 0;
    }
}