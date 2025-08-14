using Backend.Common.Record;
using Backend.Contracts.DTO;
using Backend.Contracts.VO;

namespace Backend.Contracts.IService;

public interface IRoleService {
    Task<List<RoleVO>>    SelectAll();
    Task<List<RoleAllVO>> Get(RoleSearchDTO?               dto);
    Task<bool>            UpdateStatus(UpdateRoleStatusDTO dto);
    Task<RoleByIdVO?>     GetById(long                     id);
    Task<BoolResult>      UpdateOrInsert(RoleDTO           roleDto);
    Task<bool>            Delete(RoleDeleteDTO             roleDeleteDto);
}