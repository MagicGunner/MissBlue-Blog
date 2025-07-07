using Backend.Domain.Entity;
using Backend.Domain.IRepository;
using Backend.Infrastructure.Repository;
using Backend.Infrastructure.UnitOfWorks;
using Backend.Modules.Blog.Domain.Entities;
using Backend.Modules.Blog.Domain.IRepository;

namespace Backend.Modules.Blog.Infrastructure.Repository;

public class LeaveWordRepository(IUnitOfWorkManage unitOfWorkManage) : BaseRepositories<LeaveWord>(unitOfWorkManage), ILeaveWordRepository {
    public async Task<List<LeaveWord>> GetBackList(string userName, int isCheck = 1, string? startTime = null, string? endTime = null) {
        var query = Db.Queryable<LeaveWord>();
        // 模糊查用户ID列表
        var userIds = await Db.Queryable<User>()
                              .Where(u => u.Username.Contains(userName))
                              .Select(u => u.Id)
                              .ToListAsync();
        query = userIds.Count != 0 ? query.Where(lw => userIds.Contains(lw.UserId)) : query.Where(lw => false); // 保证用户名对应UserId不存在时查不到任何数据
        query = query.Where(lw => lw.IsCheck == isCheck);

        if (!string.IsNullOrWhiteSpace(startTime) && DateTime.TryParse(startTime, out var start)) {
            query = query.Where(lw => lw.CreateTime >= start);
        }

        if (!string.IsNullOrWhiteSpace(endTime) && DateTime.TryParse(endTime, out var end)) {
            query = query.Where(lw => lw.CreateTime <= end);
        }

        return await query.ToListAsync();
    }

    public async Task<List<LeaveWord>> GetList(string? id) {
        var query = Db.Queryable<LeaveWord>().Where(lw => lw.IsCheck == 1);
        if (!string.IsNullOrWhiteSpace(id)) {
            query = query.Where(lw => lw.Id.ToString() == id);
        }

        return await query.OrderByDescending(lw => lw.CreateTime).ToListAsync();
    }

    public async Task<Dictionary<long, string>> GetContentDic(List<long> userIds) =>
        (await Db.Queryable<LeaveWord>().Where(leaveWord => userIds.Contains(leaveWord.UserId)).ToListAsync())
       .ToDictionary(leaveWord => leaveWord.Id, leaveWord => leaveWord.Content);
}