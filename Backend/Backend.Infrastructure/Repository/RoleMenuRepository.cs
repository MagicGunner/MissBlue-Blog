using Backend.Domain.Entity;
using Backend.Domain.IRepository;
using Backend.Infrastructure.UnitOfWorks;

namespace Backend.Infrastructure.Repository;

public class RoleMenuRepository(IUnitOfWorkManage unitOfWorkManage) : BaseRepositories<RoleMenu>(unitOfWorkManage), IRoleMenuRepository {
    public async Task<List<RoleMenu>> GetByRoleId(long roleId) {
        return await Db.Queryable<RoleMenu>().Where(rm => rm.RoleId == roleId).ToListAsync();
    }
}