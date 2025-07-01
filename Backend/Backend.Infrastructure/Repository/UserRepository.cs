using Backend.Domain.Entity;
using Backend.Domain.IRepository;
using Backend.Infrastructure.UnitOfWorks;

namespace Backend.Infrastructure.Repository;

public class UserRepository(IUnitOfWorkManage unitOfWorkManage) : BaseRepositories<User>(unitOfWorkManage), IUserRepository {
    public async Task<bool> ValidateUser(string userName, string password) {
        var user = await GetUserByName(userName);
        return user != null && BCrypt.Net.BCrypt.Verify(password, user.Password);
    }

    public async Task<List<Permission>> GetUserPermissions(string userName) {
        var user = await GetUserByName(userName);
        if (user == null) {
            return [];
        }

        var userId = user.Id;
        var userRole = (await Db.Queryable<UserRole>().Where(userRole => userRole.UserId == userId).ToListAsync()).FirstOrDefault();
        if (userRole == null) {
            return [];
        }

        var rolePermissions = await Db.Queryable<RolePermission>().Where(rolePermission => rolePermission.RoleId == userRole.RoleId).ToListAsync();
        if (rolePermissions == null) {
            return [];
        }

        var permissionIds = rolePermissions.Select(i => i.PermissionId).ToList();
        return await Db.Queryable<Permission>().Where(permission => permissionIds.Contains(permission.Id)).ToListAsync();
    }

    public async Task<User?> GetUserByName(string userName) {
        return (await Db.Queryable<User>().Where(user => user.Username == userName).ToListAsync()).FirstOrDefault();
    }
}