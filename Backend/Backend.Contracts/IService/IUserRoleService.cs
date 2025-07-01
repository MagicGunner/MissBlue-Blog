using Backend.Common.Results;
using Backend.Contracts.DTO;

namespace Backend.Contracts.IService;

public interface IUserRoleService {
    Task<ResponseResult<object>> Add(UserRoleDTO userRoleDto);
}