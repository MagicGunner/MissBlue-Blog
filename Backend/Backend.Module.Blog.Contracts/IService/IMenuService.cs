using Backend.Modules.Blog.Contracts.DTO;
using Backend.Modules.Blog.Contracts.VO;

namespace Backend.Modules.Blog.Contracts.IService;

public interface IMenuService {
    Task<List<MenuVO>> GetMenuList(int typeId, string? userName, int? status);
    Task<bool>         Add(MenuDTO     menuDto);
    Task<MenuByIdVO?>  GetById(long    id);
    Task<bool>         Update(MenuDTO  menuDto);
}