using AutoMapper;
using Backend.Common.Record;
using Backend.Common.Results;
using Backend.Contracts.DTO;
using Backend.Contracts.IService;
using Backend.Contracts.VO;
using Backend.Domain.Entity;
using Backend.Domain.IRepository;

namespace Backend.Application.Service;

public class UserRoleService(IMapper mapper, IBaseRepositories<UserRole> baseRepositories, IUserRoleRepository userRoleRepository)
    : BaseServices<UserRole>(mapper, baseRepositories), IUserRoleService {
    private IMapper _mapper = mapper;

    public async Task<BoolResult> AddUserRole(UserRoleDTO dto) {
        return await userRoleRepository.AddUserRole(dto.RoleId, dto.UserIds);
    }

    public async Task<BoolResult> AddRoleUser(RoleUserDTO dto) {
        return await userRoleRepository.AddRoleUser(dto.RoleId, dto.UserId);
    }

    public async Task<List<RoleUserVO>> GetUserByRoleId(long roleId, string? username, string? email, int type) {
        return _mapper.Map<List<RoleUserVO>>(await userRoleRepository.GetUserByRoleId(roleId, username, email, type));
    }

    public async Task<List<RoleAllVO>> GetRoleByUserId(long userId, string? roleName, string? roleKey, int type) {
        return _mapper.Map<List<RoleAllVO>>(await userRoleRepository.GetRoleByUserId(userId, roleName, roleKey, type));
    }

    public async Task<BoolResult> DeleteByUserRole(UserRoleDTO dto) {
        return await userRoleRepository.DeleteByUserRole(dto.RoleId, dto.UserIds);
    }

    public async Task<BoolResult> DeleteByRoleUser(RoleUserDTO dto) {
        return await userRoleRepository.DeleteByRoleUser(dto.RoleId, dto.UserId);
    }
}