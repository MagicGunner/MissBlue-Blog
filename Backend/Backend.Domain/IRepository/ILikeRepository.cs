using Backend.Domain.Entity;
using Backend.Domain.Enums;

namespace Backend.Domain.IRepository;

public interface ILikeRepository : IBaseRepositories<Like> {
    Task<Dictionary<long, long>> GetCountDic(LikeType type,   List<long> typeIds);
    Task<long>                   GetCount(LikeType    type,   long       typeId);
    Task<List<Like>>             IsLike(long          userId, int        type, long? typeId);
    Task<bool>                   SetLiked(long        userId, int        type, long  typeId);
    Task<bool>                   UnSetLiked(long      userId, int        type, long  typeId);
}