using Backend.Domain.Entity;
using Backend.Domain.IRepository;
using Backend.Infrastructure.UnitOfWorks;

namespace Backend.Infrastructure.Repository;

public class UserRepository(IUnitOfWorkManage unitOfWorkManage) : BaseRepositories<User>(unitOfWorkManage), IUserRepository {
    public async Task<long> ValidateUser(string userName, string password) {
        var user = await GetUserByName(userName);
        if (user != null && BCrypt.Net.BCrypt.Verify(password, user.Password)) {
            return user.Id;
        }

        return -1;
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

    public async Task<Dictionary<long, User>> GetUserDic(List<long> userIds) {
        return (await Db.Queryable<User>().Where(user => userIds.Contains(user.Id)).ToListAsync()).ToDictionary(user => user.Id, user => user);
    }

    public async Task<List<long>> GetIds(string? userName) {
        var query = Db.Queryable<User>();
        if (userName != null) {
            query = query.Where(user => user.Username == userName);
        }

        return await query.Select(user => user.Id).ToListAsync();
    }
}