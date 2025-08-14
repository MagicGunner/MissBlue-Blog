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

    public async Task<Dictionary<long, string>> GetNameDicByIds(List<long> userIds) => (await Db.Queryable<User>().In(u => u.Id, userIds).ToListAsync()).ToDictionary(u => u.Id, u => u.Username);

    public async Task<bool> UserExists(string? userName, string email) {
        var query = Db.Queryable<User>();
        if (userName != null) query = query.Where(u => u.Username == userName);
        query = query.Where(u => u.Email == email);
        return await query.AnyAsync();
    }

    public async Task<bool> ResetPassWord(string password, string email) {
        return await Db.Updateable<User>().Where(u => u.Email == email).SetColumns(u => u.Password, password).ExecuteCommandAsync() > 0;
    }

    public async Task<List<User>> GetOrSearch(string? userName, string? email, int? isDisable, DateTime? startTime, DateTime? endTime) {
        var query = Db.Queryable<User>();
        if (userName != null) query = query.Where(user => user.Username.Contains(userName));
        if (email != null) query = query.Where(user => user.Email != null && user.Email.Contains(email));
        if (isDisable.HasValue) query = query.Where(user => user.IsDisable == isDisable.Value);
        if (startTime != null) query = query.Where(user => user.CreateTime >= startTime);
        if (endTime != null) query = query.Where(user => user.CreateTime <= endTime);
        return await query.ToListAsync();
    }

    public async Task<bool> UpdateStatus(long userId, int isDisable) {
        return await Db.Updateable<User>()
                       .SetColumns(u => u.IsDisable == isDisable)
                       .Where(u => u.Id == userId)
                       .ExecuteCommandAsync() > 0;
    }

    public override async Task<bool> Update(User user) {
        return await Db.Updateable<User>()
                       .SetColumns(u => u.Nickname == user.Nickname)
                       .SetColumns(u => u.Gender == user.Gender)
                       .SetColumns(u => u.Avatar == user.Avatar)
                       .SetColumns(u => u.Intro == user.Intro)
                       .Where(u => u.Id == user.Id)
                       .ExecuteCommandAsync() > 0;
    }

    public override async Task<bool> DeleteByIds(List<long> ids) {
        var trans = await Db.Ado.UseTranAsync(async () => {
                                                  if (await DeleteByIds(ids)) {
                                                      // 2) 删除关联关系与业务数据
                                                      await Db.Deleteable<UserRole>()
                                                              .Where(ur => ids.Contains(ur.UserId))
                                                              .ExecuteCommandAsync();

                                                      await Db.Deleteable<Comment>()
                                                              .Where(c => ids.Contains(c.CommentUserId) || ids.Contains(c.ReplyUserId))
                                                              .ExecuteCommandAsync();

                                                      await Db.Deleteable<Like>()
                                                              .Where(l => ids.Contains(l.UserId))
                                                              .ExecuteCommandAsync();

                                                      await Db.Deleteable<Favorite>()
                                                              .Where(f => ids.Contains(f.UserId))
                                                              .ExecuteCommandAsync();

                                                      await Db.Deleteable<Article>()
                                                              .Where(a => ids.Contains(a.UserId))
                                                              .ExecuteCommandAsync();

                                                      await Db.Deleteable<TreeHole>()
                                                              .Where(t => ids.Contains(t.UserId))
                                                              .ExecuteCommandAsync();

                                                      await Db.Deleteable<LeaveWord>()
                                                              .Where(lw => ids.Contains(lw.UserId))
                                                              .ExecuteCommandAsync();

                                                      await Db.Deleteable<ChatGpt>()
                                                              .Where(cg => ids.Contains(cg.UserId))
                                                              .ExecuteCommandAsync();

                                                      await Db.Deleteable<Link>()
                                                              .Where(lk => ids.Contains(lk.UserId))
                                                              .ExecuteCommandAsync();
                                                  }
                                              });
        return trans.IsSuccess;
    }
}