using Backend.Domain.IRepository;
using Backend.Modules.Blog.Domain.Entities;
using Backend.Modules.Blog.Domain.Enums;

namespace Backend.Modules.Blog.Domain.IRepository;

public interface ILikeRepository : IBaseRepositories<Like> {
    Task<Dictionary<long, long>> GetCountDic(LikeType type,   List<long> typeIds);
    Task<long>                   GetCount(LikeType    type,   long       typeId);
    Task<List<Like>>             IsLike(long          userId, int        type, long? typeId);
    Task<bool>                   SetLiked(long        userId, int        type, long  typeId);
    Task<bool>                   UnSetLiked(long      userId, int        type, long  typeId);
}