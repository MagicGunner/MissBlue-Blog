using Backend.Domain.Entity;
using Backend.Infrastructure.Repository;
using Backend.Infrastructure.UnitOfWorks;
using Backend.Modules.Blog.Domain.IRepository;

namespace Backend.Modules.Blog.Infrastructure.Repository;

public class RoleRepository(IUnitOfWorkManage unitOfWorkManage) : BaseRepositories<Role>(unitOfWorkManage), IRoleRepository {
    public async Task<List<Role>> SelectAll() {
        return await Db.Queryable<Role>().Where(r => r.Status == 0).ToListAsync();
    }
}