using Backend.Common.Record;
using Backend.Contracts.DTO;
using Backend.Contracts.VO;

namespace Backend.Contracts.IService;

public interface IPermissionService {
    Task<List<PermissionVO>>     Get(string? permissionDesc, string? permissionKey, long? permissionMenuId);
    Task<List<PermissionMenuVO>> GetPermissionMenuList();
    Task<BoolResult>             UpdateOrInsert(PermissionDTO permissionDto);
    Task<PermissionDTO>          GetById(long                 id);
    Task<BoolResult>             Delete(long                  id);
}