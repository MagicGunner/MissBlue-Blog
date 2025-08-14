using System.Linq.Expressions;
using Backend.Domain.Entity;
using Backend.Domain.Enums;
using Backend.Domain.IRepository;
using Backend.Infrastructure.UnitOfWorks;
using SqlSugar;

namespace Backend.Infrastructure.Repository;

public class FavoriteRepository(IUnitOfWorkManage unitOfWorkManage) : BaseRepositories<Favorite>(unitOfWorkManage), IFavoriteRepository {
    public async Task<Dictionary<long, long>> GetCountDic(FavoriteType type, List<long> typeIds) {
        return (await Db.Queryable<Favorite>()
                        .Where(favorite => favorite.Type == (int)type && typeIds.Contains(favorite.TypeId))
                        .GroupBy(favorite => favorite.TypeId)
                        .Select(favorite => new {
                                                    favorite.TypeId,
                                                    Count = (long)SqlFunc.AggregateCount(favorite.Id)
                                                })
                        .ToListAsync()).ToDictionary(i => i.TypeId, i => i.Count);
    }

    public async Task<long> GetCount(FavoriteType type, long typeId) => await Db.Queryable<Favorite>().Where(f => f.Type == (int)type && f.IsCheck == 1 && f.TypeId == typeId).CountAsync();

    public async Task<bool> SetFavorited(long userId, int type, long typeId) {
        // 判断是否已经设置收藏
        var existing = await Db.Queryable<Favorite>().Where(favorite => favorite.UserId == userId && favorite.Type == type && favorite.TypeId == typeId).FirstAsync();
        if (existing != null) { // 存在记录则表示已经收藏
            return false;
        }

        var favorite = new Favorite {
                                        UserId = userId,
                                        Type = type,
                                        TypeId = typeId,
                                        CreateTime = DateTime.Now
                                    };
        // todo 更新Redis中的收藏数， +1

        return await Db.Insertable(favorite).ExecuteCommandAsync() > 0;
    }

    public async Task<bool> UnSetFavorited(long userId, int type, long typeId) {
        // 抽出通用查询条件表达式
        Expression<Func<Favorite, bool>> predicate = favorite => favorite.UserId == userId && favorite.Type == type && favorite.TypeId == typeId;
        // 判断是否已经设置收藏
        var existing = await Db.Queryable<Favorite>().Where(predicate).FirstAsync();
        if (existing == null) { // 不存在记录则表示没有收藏，不需要取消
            return false;
        }

        // todo 更新Redis中的收藏数， -1

        return await Db.Deleteable<Favorite>().Where(predicate).ExecuteCommandAsync() > 0;
    }

    public async Task<bool> IsFavorited(long userId, int type, long typeId) {
        return await Db.Queryable<Favorite>().Where(favorite => favorite.UserId == userId && favorite.Type == type && favorite.TypeId == typeId).FirstAsync() != null;
    }

    public async Task<bool> SetChecked(long id, int isChecked) {
        var favorite = new Favorite {
                                        Id = id,
                                        IsCheck = isChecked
                                    };
        return await Db.Updateable(favorite)
                       .UpdateColumns(f => f.IsCheck)
                       .ExecuteCommandAsync() > 0;
    }

    public async Task<List<Favorite>> GetBackList(List<long> userIds, int? type, int? isCheck, string? startTime, string? endTime) {
        var query = Db.Queryable<Favorite>().Where(favorite => userIds.Contains(favorite.UserId)); // 1. 用户名模糊查询 -> 映射为 UserId 条件
        if (type != null) query = query.Where(favorite => favorite.Type == type.Value);
        if (isCheck != null) query = query.Where(favorite => favorite.IsCheck == isCheck.Value);
        if (startTime != null && DateTime.TryParse(startTime, out var start)) query = query.Where(favorite => favorite.CreateTime >= start);
        if (endTime != null && DateTime.TryParse(endTime, out var end)) query = query.Where(favorite => favorite.CreateTime <= end);
        return await query.ToListAsync();
    }

    public async Task<bool> Delete(List<long> ids) => await Db.Deleteable<Favorite>().In(ids).ExecuteCommandAsync() > 0;
}