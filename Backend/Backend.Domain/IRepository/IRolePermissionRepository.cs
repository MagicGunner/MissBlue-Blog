using Backend.Domain.Entity;

namespace Backend.Domain.IRepository;

public interface IRolePermissionRepository : IBaseRepositories<RolePermission> {
    Task<List<Role>> GetRoleByPermissionId(long permissionId, string? roleName, string? roleKey, int type);

    Task<bool> Insert(Dictionary<long, long> rolePermissionPairs);
    Task<bool> Delete(Dictionary<long, long> rolePermissionPairs);
}