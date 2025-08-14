using Backend.Contracts.DTO;
using Backend.Contracts.VO;

namespace Backend.Contracts.IService;

public interface IMenuService {
    Task<List<MenuVO>>                  GetMenuList(int typeId, string? userName, int? status);
    Task<bool>                          Add(MenuDTO     menuDto);
    Task<MenuByIdVO?>                   GetById(long    id);
    Task<bool>                          Update(MenuDTO  menuDto);
    Task<(bool isSuccess, string? msg)> Delete(long     id);
}