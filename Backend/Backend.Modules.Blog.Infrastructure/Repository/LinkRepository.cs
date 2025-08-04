using Backend.Common.Enums;
using Backend.Domain.Entity;
using Backend.Infrastructure.Repository;
using Backend.Infrastructure.UnitOfWorks;
using Backend.Modules.Blog.Domain.Entities;
using Backend.Modules.Blog.Domain.IRepository;

namespace Backend.Modules.Blog.Infrastructure.Repository;

public class LinkRepository(IUnitOfWorkManage unitOfWorkManage) : BaseRepositories<Link>(unitOfWorkManage), ILinkRepository {
    public Task<List<Link>> GetLinkList() => Query(l => l.IsCheck == SQLConst.STATUS_PUBLIC);

    public async Task<List<Link>> GetBackLinkList(string? userName, string? name, int? isCheck, string? startTime, string? endTime) {
        var query = Db.Queryable<Link>();
        if (!string.IsNullOrEmpty(userName)) {
            var userIds = await Db.Queryable<User>()
                                  .Where(u => u.Username.Contains(userName))
                                  .Select(u => u.Id)
                                  .ToListAsync();
            query = query.In(l => l.UserId, userIds);
        }

        if (!string.IsNullOrEmpty(name)) {
            query = query.Where(l => l.Name.Contains(name));
        }

        if (isCheck.HasValue) {
            query = query.Where(l => l.IsCheck == isCheck.Value);
        }

        if (!string.IsNullOrEmpty(startTime)) {
            query = query.Where(l => l.CreateTime >= DateTime.Parse(startTime));
        }

        if (!string.IsNullOrEmpty(endTime)) {
            query = query.Where(l => l.CreateTime <= DateTime.Parse(endTime));
        }

        return await query.ToListAsync();
    }

    public Task<bool> SetIsChecked(long linkId, int isChecked) {
        return Db.Updateable<Link>()
                 .SetColumns(l => l.IsCheck == isChecked)
                 .Where(l => l.Id == linkId)
                 .ExecuteCommandHasChangeAsync();
    }
}