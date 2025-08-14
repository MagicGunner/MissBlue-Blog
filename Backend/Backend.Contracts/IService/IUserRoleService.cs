using Backend.Common.Record;
using Backend.Common.Results;
using Backend.Contracts.DTO;
using Backend.Contracts.VO;

namespace Backend.Contracts.IService;

public interface IUserRoleService {
    Task<BoolResult> AddUserRole(UserRoleDTO dto);
    Task<BoolResult> AddRoleUser(RoleUserDTO dto);

    Task<List<RoleUserVO>> GetUserByRoleId(long         roleId, string? username, string? email,   int type);
    Task<List<RoleAllVO>>  GetRoleByUserId(long         userId, string? roleName, string? roleKey, int type);
    Task<BoolResult>       DeleteByUserRole(UserRoleDTO dto);
    Task<BoolResult>       DeleteByRoleUser(RoleUserDTO dto);
}