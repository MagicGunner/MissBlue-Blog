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

public class MenuService(IMapper mapper, IBaseRepositories<Menu> baseRepositories, IMenuRepository menuRepository, ICurrentUser currentUser)
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

    public Task<MenuByIdVO> GetById(long id) {
        throw new NotImplementedException();
    }
}