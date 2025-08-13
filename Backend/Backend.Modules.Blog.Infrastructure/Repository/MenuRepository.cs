using Backend.Domain.Entity;
using Backend.Infrastructure.Repository;
using Backend.Infrastructure.UnitOfWorks;
using Backend.Modules.Blog.Domain.Entities;
using Backend.Modules.Blog.Domain.IRepository;

namespace Backend.Modules.Blog.Infrastructure.Repository;

public class MenuRepository(IUnitOfWorkManage unitOfWorkManage) : BaseRepositories<Menu>(unitOfWorkManage), IMenuRepository {
    public async Task<List<Menu>> GetMenuList(long? userId, int typeId, string? userName, int? status) {
        var query = Db.Queryable<Menu>().OrderBy(m => m.OrderNum);

        if (typeId != 0) {
            if (typeId == 1 && (!string.IsNullOrWhiteSpace(userName) || status != 0)) {
                if (status is 0 or 1) query = query.Where(m => m.IsDisable == status);

                if (!string.IsNullOrWhiteSpace(userName)) query = query.Where(m => m.Title.Contains(userName));
            }
        } else {
            var roleIds = await Db.Queryable<UserRole>()
                                  .Where(ur => ur.UserId == userId)
                                  .Select(ur => ur.RoleId)
                                  .ToListAsync();

            var menuIds = await Db.Queryable<RoleMenu>()
                                  .Where(rm => roleIds.Contains(rm.RoleId))
                                  .Select(rm => rm.MenuId)
                                  .ToListAsync();

            var roleMenuAllIds = await Db.Queryable<RoleMenu>().Select(rm => rm.MenuId).ToListAsync();
            var menuAllIds = await Db.Queryable<Menu>().Select(m => m.Id).ToListAsync();

            var noRoleMenuIds = menuAllIds.Except(roleMenuAllIds).ToList();
            menuIds.AddRange(noRoleMenuIds);

            if (menuIds.Count > 0) query = query.Where(m => menuIds.Contains(m.Id));

            query = query.Where(m => m.IsDisable == 0);
        }

        return await query.ToListAsync();
    }

    public async Task<List<RoleMenu>> GetRoleMenuByMenuIds(long menuId) {
        return await Db.Queryable<RoleMenu>()
                       .Where(rm => rm.MenuId == menuId)
                       .ToListAsync();
        ;
    }

    public async Task<(bool isSuccess, string? msg)> Delete(long id) {
        // 先检查是否存在未删除的子菜单
        var childCount = await Db.Queryable<Menu>()
                                 .CountAsync(m => m.ParentId == id);
        if (childCount > 0) return (false, "请先删除子菜单");

        var tran = await Db.Ado.UseTranAsync(async () => {
                                                 var menuRows = await Db.Deleteable<Menu>()
                                                                        .Where(m => m.Id == id)
                                                                        .ExecuteCommandAsync();
                                                 if (menuRows == 0) {
                                                     return (false, "菜单不存在或已被删除");
                                                 }

                                                 // 删除关联的角色菜单
                                                 var roleMenuRows = await Db.Deleteable<RoleMenu>()
                                                                            .Where(rm => rm.MenuId == id)
                                                                            .ExecuteCommandAsync();
                                                 // 删除关联的权限
                                                 var permissionRows = await Db.Deleteable<Permission>()
                                                                              .Where(p => p.MenuId == id)
                                                                              .ExecuteCommandAsync();
                                                 return (true, "删除成功");
                                             });
        return tran.Data;
    }
}