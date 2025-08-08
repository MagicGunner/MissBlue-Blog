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

    public async Task<Menu?> GetById(long id) {
        return await Db.Queryable<Menu>().InSingleAsync(id);
    }

    public async Task<List<RoleMenu>> GetRoleMenuByMenuIds(long menuId) {
        return await Db.Queryable<RoleMenu>()
                       .Where(rm => rm.MenuId == menuId)
                       .ToListAsync();
        ;
    }
}