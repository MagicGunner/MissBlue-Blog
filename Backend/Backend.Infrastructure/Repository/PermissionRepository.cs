using Backend.Domain.Entity;
using Backend.Domain.IRepository;
using Backend.Infrastructure.UnitOfWorks;

namespace Backend.Infrastructure.Repository;

public class PermissionRepository(IUnitOfWorkManage unitOfWorkManage) : BaseRepositories<Permission>(unitOfWorkManage), IPermissionRepository {
    public async Task<List<Permission>> GetAllPermissions() => await Query();
}