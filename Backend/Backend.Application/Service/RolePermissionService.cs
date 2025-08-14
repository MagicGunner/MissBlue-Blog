using AutoMapper;
using Backend.Common.Record;
using Backend.Contracts.DTO;
using Backend.Contracts.IService;
using Backend.Contracts.VO;
using Backend.Domain.Entity;
using Backend.Domain.IRepository;

namespace Backend.Application.Service;

public class RolePermissionService(IMapper mapper, IBaseRepositories<RolePermission> baseRepositories, IRolePermissionRepository rolePermissionRepository)
    : BaseServices<RolePermission>(mapper, baseRepositories), IRolePermissionService {
    private readonly IMapper _mapper = mapper;

    public async Task<List<RoleAllVO>> GetRoleByPermissionId(long permissionId, string? roleName, string? roleKey, int type) {
        var roles = await rolePermissionRepository.GetRoleByPermissionId(permissionId, roleName, roleKey, type);
        return roles.Select(r => _mapper.Map<RoleAllVO>(r)).ToList();
    }

    public async Task<BoolResult> Add(RolePermissionDTO rolePermissionDto) {
        // 基本校验
        if (rolePermissionDto.RoleId == null || rolePermissionDto.RoleId.Count == 0 || rolePermissionDto.PermissionId == null || rolePermissionDto.PermissionId.Count == 0) {
            return new BoolResult(false, "角色ID或权限ID不能为空");
        }

        // 组合去重（防止相同的(roleId, permissionId)重复提交）
        var pairs = rolePermissionDto.RoleId
                                     .SelectMany(rid => rolePermissionDto.PermissionId.Select(pid => new {
                                                                                                             rid,
                                                                                                             pid
                                                                                                         }))
                                     .Distinct()
                                     .ToDictionary(rp => rp.rid, rp => rp.pid);
        var result = await rolePermissionRepository.Insert(pairs);
        return new BoolResult(result, result ? "添加成功" : "添加失败");
    }

    public async Task<BoolResult> Delete(RolePermissionDTO rolePermissionDto) {
        if (rolePermissionDto.PermissionId == null || rolePermissionDto.PermissionId.Count == 0 ||
            rolePermissionDto.RoleId == null || rolePermissionDto.RoleId.Count == 0) {
            return new BoolResult(false, "角色ID或权限ID不能为空");
        }

        var result = await rolePermissionRepository.Delete(rolePermissionDto.RoleId
                                                                            .SelectMany(rid => rolePermissionDto.PermissionId.Select(pid => new {
                                                                                                                                         rid,
                                                                                                                                         pid
                                                                                                                                     }))
                                                                            .Distinct()
                                                                            .ToDictionary(rp => rp.rid, rp => rp.pid));
        return new BoolResult(result, result ? "添加成功" : "添加失败");
    }
}