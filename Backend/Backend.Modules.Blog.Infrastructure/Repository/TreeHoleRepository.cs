using Backend.Domain.Entity;
using Backend.Infrastructure.Repository;
using Backend.Infrastructure.UnitOfWorks;
using Backend.Modules.Blog.Domain.Entities;
using Backend.Modules.Blog.Domain.IRepository;

namespace Backend.Modules.Blog.Infrastructure.Repository;

public class TreeHoleRepository(IUnitOfWorkManage unitOfWorkManage) : BaseRepositories<TreeHole>(unitOfWorkManage), ITreeHoleRepository {
    public async Task<List<TreeHole>> GetFrontlist() {
        return await Db.Queryable<TreeHole>().Where(th => th.IsCheck == 1).ToListAsync();
    }

    public async Task<List<TreeHole>> GetBackList(string? userName, int? isCheck, string? startTime, string? endTime) {
        var query = Db.Queryable<TreeHole>();

        if (!string.IsNullOrEmpty(userName)) {
            var userIds = (await Db.Queryable<User>().Where(u => u.Username.Contains(userName)).ToListAsync()).Select(u => u.Id).ToList();
            if (userIds.Count == 0) {
                return [];
            }

            query = query.In(th => th.UserId, userIds);
        }

        if (isCheck.HasValue) {
            query = query.Where(th => th.IsCheck == isCheck.Value);
        }

        if (!string.IsNullOrEmpty(startTime)) {
            query = query.Where(th => th.CreateTime >= DateTime.Parse(startTime));
        }

        if (!string.IsNullOrEmpty(endTime)) {
            query = query.Where(th => th.CreateTime <= DateTime.Parse(endTime));
        }

        return await query.ToListAsync();
    }

    public async Task<bool> SetCheck(long id, int isCheck) {
        return await Db.Updateable<TreeHole>()
                       .Where(th => th.Id == id)
                       .SetColumns(th => th.IsCheck == isCheck)
                       .SetColumns(th => th.UpdateTime == DateTime.Now)
                       .ExecuteCommandHasChangeAsync();
    }
}