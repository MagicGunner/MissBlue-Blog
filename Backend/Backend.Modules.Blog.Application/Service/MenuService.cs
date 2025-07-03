using AutoMapper;
using Backend.Application.Interface;
using Backend.Application.Service;
using Backend.Domain.Entity;
using Backend.Domain.IRepository;
using Backend.Modules.Blog.Contracts.IService;
using Backend.Modules.Blog.Contracts.VO;
using Backend.Modules.Blog.Domain.Entities;

namespace Backend.Modules.Blog.Application.Service;

public class MenuService(IMapper mapper, IBaseRepositories<Menu> baseRepositories, ICurrentUser currentUser) : BaseServices<Menu>(mapper, baseRepositories), IMenuService {
    public async Task<List<MenuVO>> GetMenuList(int typeId, string? userName = null, int status = 0) {
        var query = Db.Queryable<Menu>().OrderBy(m => m.OrderNum);

        if (typeId == 0) {
            var userId = currentUser.UserId;

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

            if (menuIds.Count > 0)
                query = query.Where(m => menuIds.Contains(m.Id));

            query = query.Where(m => m.IsDisable == 0);
        } else if (typeId == 1 && (!string.IsNullOrWhiteSpace(userName) || status != 0)) {
            if (status is 0 or 1)
                query = query.Where(m => m.IsDisable == status);

            if (!string.IsNullOrWhiteSpace(userName))
                query = query.Where(m => m.Title.Contains(userName));
        }

        var menus = await query.ToListAsync();

        return menus.Select(menu => {
                                var menuVo = mapper.Map<MenuVO>(menu);
                                menuVo.Affix = menu.Affix == 1;
                                menuVo.HideInMenu = menu.HideInMenu == 1;
                                menuVo.KeepAlive = menu.KeepAlive == 1;
                                menuVo.IsDisable = menu.IsDisable == 1;
                                return menuVo;
                            })
                    .ToList();
    }
}