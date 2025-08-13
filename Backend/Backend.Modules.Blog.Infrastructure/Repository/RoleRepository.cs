using Backend.Domain.Entity;
using Backend.Infrastructure.Repository;
using Backend.Infrastructure.UnitOfWorks;
using Backend.Modules.Blog.Domain.IRepository;

namespace Backend.Modules.Blog.Infrastructure.Repository;

public class RoleRepository(IUnitOfWorkManage unitOfWorkManage) : BaseRepositories<Role>(unitOfWorkManage), IRoleRepository {
    public async Task<List<Role>> SelectAll() {
        return await Db.Queryable<Role>().Where(r => r.Status == 0).ToListAsync();
    }

    public async Task<List<Role>> Get(string? roleName, string? roleKey, int? status, DateTime? startTime, DateTime? endTime) {
        var query = Db.Queryable<Role>();
        if (!string.IsNullOrEmpty(roleName)) query = query.Where(r => r.RoleName == roleName);
        if (!string.IsNullOrEmpty(roleKey)) query = query.Where(r => r.RoleKey == roleKey);
        if (status.HasValue) query = query.Where(r => r.Status == status.Value);
        if (startTime.HasValue) query = query.Where(r => r.CreateTime >= startTime.Value);
        if (endTime.HasValue) query = query.Where(r => r.CreateTime <= endTime.Value);
        return await query.ToListAsync();
    }
}