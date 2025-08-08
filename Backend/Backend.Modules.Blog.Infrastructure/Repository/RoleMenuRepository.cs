using Backend.Domain.Entity;
using Backend.Infrastructure.Repository;
using Backend.Infrastructure.UnitOfWorks;
using Backend.Modules.Blog.Domain.IRepository;

namespace Backend.Modules.Blog.Infrastructure.Repository;

public class RoleMenuRepository(IUnitOfWorkManage unitOfWorkManage) : BaseRepositories<RoleMenu>(unitOfWorkManage), IRoleMenuRepository {
}