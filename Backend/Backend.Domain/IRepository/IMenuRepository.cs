using Backend.Domain.Entity;

namespace Backend.Domain.IRepository;

public interface IMenuRepository : IBaseRepositories<Menu> {
    Task<List<Menu>>                    GetMenuList(long?         userId, int typeId, string? userName, int? status);
    Task<List<RoleMenu>>                GetRoleMenuByMenuIds(long menuId);
    Task<(bool isSuccess, string? msg)> Delete(long               id);
}