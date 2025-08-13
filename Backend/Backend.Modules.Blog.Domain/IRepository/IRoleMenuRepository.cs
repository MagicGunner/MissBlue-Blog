using Backend.Domain.Entity;
using Backend.Domain.IRepository;

namespace Backend.Modules.Blog.Domain.IRepository;

public interface IRoleMenuRepository : IBaseRepositories<RoleMenu> {
    Task<List<RoleMenu>> GetByRoleId(long roleId);
}