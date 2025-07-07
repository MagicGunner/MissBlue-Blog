using Backend.Domain.IRepository;
using Backend.Modules.Blog.Domain.Entities;
using Backend.Modules.Blog.Domain.Enums;

namespace Backend.Modules.Blog.Domain.IRepository;

public interface IFavoriteRepository : IBaseRepositories<Favorite> {
    Task<Dictionary<long, long>> GetCountDic(CommentType type, List<long> typeIds);

    Task<bool>           SetFavorited(long      userId,  int  type, long typeId);
    Task<bool>           UnSetFavorited(long    userId,  int  type, long typeId);
    Task<bool>           IsFavorited(long       userId,  int  type, long typeId);
    Task<bool>           SetChecked(long        id,      int  isChecked);
    Task<List<Favorite>> GetBackList(List<long> userIds, int? type, int? isCheck, string? startTime, string? endTime);
    Task<bool>           Delete(List<long>      ids);
}