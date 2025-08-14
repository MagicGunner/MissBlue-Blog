using Backend.Domain.Entity;

namespace Backend.Domain.IRepository;

public interface IRoleMenuRepository : IBaseRepositories<RoleMenu> {
    Task<List<RoleMenu>> GetByRoleId(long roleId);
}