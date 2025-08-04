using Backend.Domain.IRepository;
using Backend.Modules.Blog.Domain.Entities;

namespace Backend.Modules.Blog.Domain.IRepository;

public interface ILinkRepository : IBaseRepositories<Link> {
    Task<List<Link>> GetLinkList();
    Task<List<Link>> GetBackLinkList(string? userName, string? name, int? isCheck, string? startTime, string? endTime);

    Task<bool> SetIsChecked(long linkId, int isChecked);
}