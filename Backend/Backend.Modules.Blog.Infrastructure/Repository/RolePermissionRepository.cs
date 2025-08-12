using Backend.Domain.Entity;
using Backend.Infrastructure.Repository;
using Backend.Infrastructure.UnitOfWorks;
using Backend.Modules.Blog.Domain.IRepository;

namespace Backend.Modules.Blog.Infrastructure.Repository;

public class RolePermissionRepository(IUnitOfWorkManage unitOfWorkManage) : BaseRepositories<RolePermission>(unitOfWorkManage), IRolePermissionRepository {
    public async Task<List<Role>> GetRoleByPermissionId(long permissionId, string? roleName, string? roleKey, int type) {
        // 1) 拿到“拥有该权限”的角色ID集合
        var roleIdsWithPerm = await Db.Queryable<RolePermission>()
                                      .Where(rp => rp.PermissionId == permissionId)
                                      .Select(rp => rp.RoleId)
                                      .Distinct()
                                      .ToListAsync();
        // 2) 组装 Role 查询
        var query = Db.Queryable<Role>();

        if (type == 0) {
            // 需要“有该权限”的角色
            if (roleIdsWithPerm.Count == 0) return [];
            query = query.In(r => r.Id, roleIdsWithPerm);
        } else {
            // 需要“没有该权限”的角色
            if (roleIdsWithPerm.Count > 0) query = query.Where(r => !roleIdsWithPerm.Contains(r.Id));
            // 否则 roleIdsWithPerm 为空，则表示所有角色都“不含该权限”，保持 query 不加限制即可
        }

        // 3) 可选筛选条件
        query = query
               .WhereIF(!string.IsNullOrWhiteSpace(roleName), r => r.RoleName.Contains(roleName!))
               .WhereIF(!string.IsNullOrWhiteSpace(roleKey), r => r.RoleKey.Contains(roleKey!));

        return await query.ToListAsync();
    }

    public async Task<bool> Insert(Dictionary<long, long> rolePermissionPairs) {
        var trans = await Db.Ado.UseTranAsync(async () => {
                                                  await Db.Deleteable<RolePermission>()
                                                          .Where(rp => rolePermissionPairs.Keys.Contains(rp.RoleId) && rolePermissionPairs.Values.Contains(rp.PermissionId))
                                                          .ExecuteCommandAsync();
                                                  foreach (var pair in rolePermissionPairs) {
                                                      await Db.Insertable(new RolePermission {
                                                                                                 RoleId = pair.Key,
                                                                                                 PermissionId = pair.Value
                                                                                             })
                                                              .ExecuteCommandAsync();
                                                  }
                                              });
        return trans.IsSuccess;
    }

    public async Task<bool> Delete(Dictionary<long, long> rolePermissionPairs) {
        var trans = await Db.Ado.UseTranAsync(async () => {
                                                  await Db.Deleteable<RolePermission>()
                                                          .Where(rp => rolePermissionPairs.Keys.Contains(rp.RoleId) && rolePermissionPairs.Values.Contains(rp.PermissionId))
                                                          .ExecuteCommandAsync();
                                              });
        return trans.IsSuccess;
    }
}