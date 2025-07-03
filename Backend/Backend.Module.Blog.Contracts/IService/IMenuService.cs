using Backend.Modules.Blog.Contracts.VO;

namespace Backend.Modules.Blog.Contracts.IService;

public interface IMenuService {
    Task<List<MenuVO>> GetMenuList(int typeId, string? userName = null, int status = 0);
}