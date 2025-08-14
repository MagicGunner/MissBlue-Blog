using Backend.Common.Record;
using Backend.Domain.Entity;
using Backend.Domain.IRepository;
using Backend.Infrastructure.UnitOfWorks;

namespace Backend.Infrastructure.Repository;

public class UserRoleRepository(IUnitOfWorkManage unitOfWorkManage) : BaseRepositories<UserRole>(unitOfWorkManage), IUserRoleRepository {
    public async Task<List<UserRole>> GetUserId(long userId) {
        return await Db.Queryable<UserRole>().Where(userRole => userRole.UserId == userId).ToListAsync();
    }

    public async Task<List<User>> GetUserByRoleId(long roleId, string? username, string? email, int type) {
        // 1. 获取角色对应的用户ID列表
        var userIdsWidthRole = await Db.Queryable<UserRole>()
                                       .Where(ur => ur.RoleId == roleId)
                                       .Select(ur => ur.UserId)
                                       .ToListAsync();
        var query = Db.Queryable<User>();
        if (type == 0) {
            if (userIdsWidthRole == null || userIdsWidthRole.Count == 0) return [];

            query = query.In(u => u.Id, userIdsWidthRole);
        } else {
            if (userIdsWidthRole.Count > 0) query = query.Where(u => !userIdsWidthRole.Contains(u.Id));
            // 若 userIdsWithRole 为空，表示所有用户都“不含该角色”，保持 query 不加限制即可
        }

        // 3) 可选条件
        if (!string.IsNullOrWhiteSpace(username)) query = query.Where(u => u.Username.Contains(username));
        if (!string.IsNullOrWhiteSpace(email)) query = query.Where(u => u.Email != null && u.Email.Contains(email));

        return await query.ToListAsync();
    }

    public async Task<List<Role>> GetRoleByUserId(long userId, string? roleName, string? roleKey, int type) {
        var roleIds = await Db.Queryable<UserRole>().Where(ur => ur.UserId == userId).Select(ur => ur.RoleId).ToListAsync();
        if (type == 1) {
            var idsWithRole = roleIds;                                                                                                           // 存在权限的角色
            roleIds = await Db.Queryable<Role>().Where(r => idsWithRole != null && !idsWithRole.Contains(r.Id)).Select(r => r.Id).ToListAsync(); // 不存在权限的角色
        }

        if (roleIds == null) return []; // 如果没有角色ID，直接返回空列表

        var query = Db.Queryable<Role>().In(r => r.Id, roleIds);
        // 3) 可选条件
        if (!string.IsNullOrWhiteSpace(roleName)) query = query.Where(r => r.RoleName != null && r.RoleName.Contains(roleName));
        if (!string.IsNullOrWhiteSpace(roleKey)) query = query.Where(r => r.RoleKey != null && r.RoleKey.Contains(roleKey));

        return await query.ToListAsync();
    }

    public async Task<BoolResult> AddUserRole(long roleId, List<long> userIds) {
        var transResult = await Db.Ado.UseTranAsync(async Task<BoolResult> () => {
                                                        // 1. 查询已有的用户角色
                                                        var existUserRoles = await Db.Queryable<UserRole>()
                                                                                     .Where(userRole => userRole.RoleId == roleId && userIds.Contains(userRole.UserId))
                                                                                     .ToListAsync();

                                                        var existUserIds = existUserRoles.Select(ur => ur.UserId).ToList();

                                                        // 2. 过滤出未分配的用户
                                                        var notExistUserIds = userIds.Where(id => !existUserIds.Contains(id)).ToList();

                                                        if (notExistUserIds.Count == 0) {
                                                            return new BoolResult(true, "全部用户已经拥有该角色，无需再次分配");
                                                        }

                                                        // 3. 构建新的UserRole对象
                                                        var newUserRoles = notExistUserIds.Select(id => new UserRole {
                                                                                                                         UserId = id,
                                                                                                                         RoleId = roleId
                                                                                                                     })
                                                                                          .ToList();

                                                        // 4. 批量插入
                                                        var result = await Db.Insertable(newUserRoles).ExecuteCommandAsync();

                                                        return result > 0
                                                                   ? new BoolResult(false, "分配成功")
                                                                   : new BoolResult(true, "分配失败");
                                                    });
        return transResult.IsSuccess ? transResult.Data : new BoolResult(false, "数据事务失败:" + transResult.ErrorException.Message);
    }

    public async Task<BoolResult> AddRoleUser(List<long> roleIds, List<long> userIds) {
        // 1) 组合去重（避免重复插入相同 (roleId, userId) ）
        var userRoles = roleIds.Distinct()
                               .SelectMany(rid => userIds.Distinct()
                                                         .Select(uid => new UserRole {
                                                                                         UserId = uid,
                                                                                         RoleId = rid
                                                                                     }))
                               .ToList();
        var trans = await Db.Ado.UseTranAsync(async () => {
                                                  // 如果存在已有的,先删除
                                                  await Db.Deleteable<UserRole>()
                                                          .In(ur => ur.UserId, userIds)
                                                          .In(ur => ur.RoleId, roleIds)
                                                          .ExecuteCommandAsync();
                                                  if (userRoles.Count > 0) await Add(userRoles);
                                              });
        return new BoolResult(trans.IsSuccess, trans.IsSuccess ? "添加成功" : "添加失败");
    }

    public async Task<BoolResult> DeleteByUserRole(long roleId, List<long> userIds) {
        return await Db.Deleteable<UserRole>().Where(ur => ur.RoleId == roleId).In(ur => ur.UserId, userIds).ExecuteCommandHasChangeAsync()
                   ? new BoolResult(true, "删除成功")
                   : new BoolResult(false, "删除失败");
    }

    public async Task<BoolResult> DeleteByRoleUser(List<long> roleIds, List<long> userIds) {
        return await Db.Deleteable<UserRole>()
                       .In(ur => ur.RoleId, roleIds)
                       .In(ur => ur.UserId, userIds)
                       .ExecuteCommandHasChangeAsync()
                   ? new BoolResult(true, "删除成功")
                   : new BoolResult(false, "删除失败");
    }
}