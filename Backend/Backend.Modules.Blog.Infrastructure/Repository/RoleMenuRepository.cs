using Backend.Domain.Entity;
using Backend.Infrastructure.Repository;
using Backend.Infrastructure.UnitOfWorks;
using Backend.Modules.Blog.Domain.IRepository;

namespace Backend.Modules.Blog.Infrastructure.Repository;

public class RoleMenuRepository(IUnitOfWorkManage unitOfWorkManage) : BaseRepositories<RoleMenu>(unitOfWorkManage), IRoleMenuRepository {
    public async Task<List<RoleMenu>> GetByRoleId(long roleId) {
        return await Db.Queryable<RoleMenu>().Where(rm => rm.RoleId == roleId).ToListAsync();
    }
}