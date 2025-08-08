using AutoMapper;
using Backend.Application.Interface;
using Backend.Application.Service;
using Backend.Domain.Entity;
using Backend.Domain.IRepository;
using Backend.Modules.Blog.Contracts.DTO;
using Backend.Modules.Blog.Contracts.IService;
using Backend.Modules.Blog.Contracts.VO;
using Backend.Modules.Blog.Domain.Entities;
using Backend.Modules.Blog.Domain.IRepository;

namespace Backend.Modules.Blog.Application.Service;

public class MenuService(IMapper                 mapper,
                         IBaseRepositories<Menu> baseRepositories,
                         IMenuRepository         menuRepository,
                         IRoleRepository         roleRepository,
                         IRoleMenuRepository     roleMenuRepository,
                         ICurrentUser            currentUser)
    : BaseServices<Menu>(mapper, baseRepositories), IMenuService {
    private readonly IMapper _mapper = mapper;

    public async Task<List<MenuVO>> GetMenuList(int typeId, string? userName, int? status) {
        var userId = currentUser.UserId;
        var menus = await menuRepository.GetMenuList(userId, typeId, userName, status);
        return menus.Select(menu => {
                                var menuVo = _mapper.Map<MenuVO>(menu);
                                menuVo.Affix = menu.Affix == 1;
                                menuVo.HideInMenu = menu.HideInMenu == 1;
                                menuVo.KeepAlive = menu.KeepAlive == 1;
                                menuVo.IsDisable = menu.IsDisable == 1;
                                return menuVo;
                            })
                    .ToList();
    }

    public async Task<bool> Add(MenuDTO menuDto) {
        menuDto.RouterType ??= 0;
        switch (menuDto.RouterType) {
            case 0:
                if (menuDto.Component == "") menuDto.Component = "RouteView";
                break;
            case 1: menuDto.Component = "Iframe"; break;
            case 2: menuDto.Component = null; break;
        }

        return await Add(_mapper.Map<Menu>(menuDto)) > 0;
    }

    public async Task<MenuByIdVO?> GetById(long id) {
        var menu = await menuRepository.GetById(id);
        if (menu == null) {
            return null;
        }

        var roleMenus = await menuRepository.GetRoleMenuByMenuIds(menu.Id);
        var roles = await roleRepository.GetByIds(roleMenus.Select(rm => rm.RoleId).ToList());
        var vo = _mapper.Map<MenuByIdVO>(menu);
        vo.RoleId = roles.Select(r => r.Id).ToList();
        vo.RouterType = string.IsNullOrWhiteSpace(vo.Component)
                            ? 2L
                            : vo.Component == "Iframe"
                                ? 1L
                                : 0L;
        return vo;
    }

    public async Task<bool> Update(MenuDTO menuDto) {
        // switch (menuDto.RouterType) {
        //     case 3: menuDto.Component = "RouteView"; break;
        //     case 0: menuDto.Redirect = null; break;
        // }
        //
        // switch (menuDto.RouterType) {
        //     case 0 or 3: menuDto.Url = null; break;
        //     case 1:
        //         menuDto.Component = "Iframe";
        //         menuDto.Target = null;
        //         break;
        //     case 2:
        //         menuDto.Component = null;
        //         menuDto.Redirect = null;
        //         break;
        // }

        switch (menuDto.RouterType) {
            case 0:
                menuDto.Redirect = null;
                menuDto.Url = null;
                break;
            case 1:
                menuDto.Component = "Iframe";
                menuDto.Target = null;
                break;
            case 2:
                menuDto.Component = null;
                menuDto.Redirect = null;
                break;
            case 3:
                menuDto.Component = "RouteView";
                menuDto.Url = null;
                break;
        }

        menuDto.ParentId ??= null;
        var menu = _mapper.Map<Menu>(menuDto);
        var result = Db.Ado.UseTranAsync(async () => {
                                             // 删除原有角色关联
                                             await roleMenuRepository.Delete(rm => rm.MenuId == menu.Id);
                                             // 添加新的角色关联
                                             if (menuDto.RoleId != null && menuDto.RoleId.Count != 0) {
                                                 var roleMenus = menuDto.RoleId.Select(roleId => new RoleMenu {
                                                                                                                  RoleId = roleId,
                                                                                                                  MenuId = menu.Id
                                                                                                              })
                                                                        .ToList();
                                                 var insertCount = await roleMenuRepository.Add(roleMenus);
                                             }
                                         });
    }
}