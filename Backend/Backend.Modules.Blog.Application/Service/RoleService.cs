using AutoMapper;
using Backend.Application.Service;
using Backend.Common.Record;
using Backend.Contracts.DTO;
using Backend.Contracts.VO;
using Backend.Domain.Entity;
using Backend.Domain.IRepository;
using Backend.Modules.Blog.Contracts.DTO;
using Backend.Modules.Blog.Contracts.IService;
using Backend.Modules.Blog.Domain.IRepository;

namespace Backend.Modules.Blog.Application.Service;

public class RoleService(IMapper                 mapper,
                         IBaseRepositories<Role> baseRepositories,
                         IRoleRepository         roleRepository,
                         IRoleMenuRepository     roleMenuRepository
) : BaseServices<Role>(mapper, baseRepositories), IRoleService {
    private readonly IMapper _mapper = mapper;

    public async Task<List<RoleVO>> SelectAll() {
        var roles = await roleRepository.SelectAll();
        var roleVOs = _mapper.Map<List<RoleVO>>(roles);
        return roleVOs;
    }

    public async Task<List<RoleAllVO>> Get(RoleSearchDTO? dto) {
        var roles = await roleRepository.Get(dto?.RoleName, dto?.RoleKey, dto?.Status, dto?.CreateTimeStart, dto?.CreateTimeEnd);
        return _mapper.Map<List<RoleAllVO>>(roles);
    }

    public Task<bool> UpdateStatus(UpdateRoleStatusDTO dto) {
        return roleRepository.UpdateStatus(dto.Id, dto.Status);
    }

    public async Task<RoleByIdVO?> GetById(long id) {
        var role = await roleRepository.GetById(id);
        var menus = await roleMenuRepository.GetByRoleId(id);
        if (role == null) return null;
        var roleByIdVo = _mapper.Map<RoleByIdVO>(role);
        roleByIdVo.MenuIds = menus.Select(m => m.MenuId).ToList();
        return roleByIdVo;
    }

    public async Task<BoolResult> UpdateOrInsert(RoleDTO roleDto) {
        // 1) DTO → 实体，并规整 RoleKey
        var role = _mapper.Map<Role>(roleDto);
        role.RoleKey = role.RoleKey?.Trim();

        return await roleRepository.UpdateOrInsert(role, roleDto.MenuIds);
    }

    public async Task<bool> Delete(RoleDeleteDTO roleDeleteDto) {
        return await roleRepository.DeleteByIds(roleDeleteDto.Ids);
    }
}