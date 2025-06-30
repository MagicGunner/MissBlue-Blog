using Backend.Common.Results;
using Backend.Contracts.DTO;
using Backend.Contracts.VO;
using Backend.Modules.Blog.Contracts.DTO;
using Backend.Modules.Blog.Contracts.VO;

namespace Backend.Modules.Blog.Contracts.IService;

public interface IRoleService {
    Task<long>            AddAsync(RoleDTO                      roleDto);
    Task<bool>            DeleteByIdsAsync(List<long>           ids);
    Task<bool>            UpdateStatusAsync(UpdateRoleStatusDTO roleStatusDto);
    Task<bool>            UpdateAsync(RoleDTO                   roleDto);
    Task<RoleByIdVO?>     GetByIdAsync(long                     id);
    Task<List<RoleAllVO>> SearchAsync(RoleSearchDTO             roleSearchDto);
    Task<List<RoleAllVO>> ListAllAsync();
}