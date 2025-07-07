using Backend.Infrastructure.Repository;
using Backend.Infrastructure.UnitOfWorks;
using Backend.Modules.Blog.Domain.Entities;
using Backend.Modules.Blog.Domain.Enums;
using Backend.Modules.Blog.Domain.IRepository;
using SqlSugar;

namespace Backend.Modules.Blog.Infrastructure.Repository;

public class FavoriteRepository(IUnitOfWorkManage unitOfWorkManage) : BaseRepositories<Favorite>(unitOfWorkManage), IFavoriteRepository {
    public async Task<Dictionary<long, long>> GetCountDic(CommentType type, List<long> typeIds) {
        return (await Db.Queryable<Favorite>()
                        .Where(favorite => favorite.Type == (int)type && typeIds.Contains(favorite.TypeId))
                        .GroupBy(favorite => favorite.TypeId)
                        .Select(favorite => new {
                                                    favorite.TypeId,
                                                    Count = (long)SqlFunc.AggregateCount(favorite.Id)
                                                })
                        .ToListAsync()).ToDictionary(i => i.TypeId, i => i.Count);
    }
}