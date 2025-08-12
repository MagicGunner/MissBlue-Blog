using Backend.Common.Record;
using Backend.Contracts.DTO;
using Backend.Contracts.IService;
using Backend.Contracts.VO;

namespace Backend.Modules.Blog.Contracts.IService;

public interface IRolePermissionService {
    Task<List<RoleAllVO>> GetRoleByPermissionId(long permissionId, string? roleName, string? roleKey, int type);

    Task<BoolResult> Add(RolePermissionDTO rolePermissionDto);

    Task<BoolResult> Delete(RolePermissionDTO rolePermissionDto);
}