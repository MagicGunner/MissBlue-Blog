using Backend.Domain.IRepository;
using Backend.Modules.Blog.Domain.Entities;
using Backend.Modules.Blog.Domain.Enums;

namespace Backend.Modules.Blog.Domain.IRepository;

public interface IFavoriteRepository : IBaseRepositories<Favorite> {
    Task<Dictionary<long, long>> GetCountDic(CommentType type, List<long> typeIds);
}