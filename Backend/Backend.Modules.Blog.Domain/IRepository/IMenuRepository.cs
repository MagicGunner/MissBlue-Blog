using Backend.Domain.Entity;
using Backend.Domain.IRepository;
using Backend.Modules.Blog.Domain.Entities;

namespace Backend.Modules.Blog.Domain.IRepository;

public interface IMenuRepository : IBaseRepositories<Menu> {
    Task<List<Menu>>                    GetMenuList(long?         userId, int typeId, string? userName, int? status);
    Task<List<RoleMenu>>                GetRoleMenuByMenuIds(long menuId);
    Task<(bool isSuccess, string? msg)> Delete(long               id);
}