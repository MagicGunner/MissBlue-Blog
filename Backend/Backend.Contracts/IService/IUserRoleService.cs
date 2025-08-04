using Backend.Common.Results;
using Backend.Contracts.DTO;

namespace Backend.Contracts.IService;

public interface IUserRoleService {
    Task<(bool isSuccess, string? msg)> Add(UserRoleDTO userRoleDto);
}