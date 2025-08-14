using Backend.Common.Record;
using Backend.Domain.Entity;

namespace Backend.Domain.IRepository;

public interface IUserRoleRepository : IBaseRepositories<UserRole> {
    Task<List<UserRole>> GetUserId(long       userId);
    Task<List<User>>     GetUserByRoleId(long roleId, string? username, string? email,   int type);
    Task<List<Role>>     GetRoleByUserId(long userId, string? roleName, string? roleKey, int type);

    Task<BoolResult> AddUserRole(long            roleId,  List<long> userIds);
    Task<BoolResult> AddRoleUser(List<long>      roleIds, List<long> userIds);
    Task<BoolResult> DeleteByUserRole(long       roleId,  List<long> userIds);
    Task<BoolResult> DeleteByRoleUser(List<long> roleIds, List<long> userIds);
}