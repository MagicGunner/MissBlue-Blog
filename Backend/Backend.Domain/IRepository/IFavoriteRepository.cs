using Backend.Domain.Entity;
using Backend.Domain.Enums;

namespace Backend.Domain.IRepository;

public interface IFavoriteRepository : IBaseRepositories<Favorite> {
    Task<Dictionary<long, long>> GetCountDic(FavoriteType type,    List<long> typeIds);
    Task<long>                   GetCount(FavoriteType    type,    long       typeId);
    Task<bool>                   SetFavorited(long        userId,  int        type, long typeId);
    Task<bool>                   UnSetFavorited(long      userId,  int        type, long typeId);
    Task<bool>                   IsFavorited(long         userId,  int        type, long typeId);
    Task<bool>                   SetChecked(long          id,      int        isChecked);
    Task<List<Favorite>>         GetBackList(List<long>   userIds, int?       type, int? isCheck, string? startTime, string? endTime);
    Task<bool>                   Delete(List<long>        ids);
}