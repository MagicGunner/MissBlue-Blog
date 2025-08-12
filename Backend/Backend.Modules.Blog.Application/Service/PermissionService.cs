using AutoMapper;
using Backend.Application.Service;
using Backend.Common.Record;
using Backend.Contracts.DTO;
using Backend.Contracts.VO;
using Backend.Domain.Entity;
using Backend.Domain.IRepository;
using Backend.Modules.Blog.Contracts.IService;
using Backend.Modules.Blog.Domain.Entities;

namespace Backend.Modules.Blog.Application.Service;

public class PermissionService(IMapper mapper, IBaseRepositories<Permission> baseRepositories, IPermissionRepository permissionRepository)
    : BaseServices<Permission>(mapper, baseRepositories), IPermissionService {
    private readonly IMapper _mapper = mapper;

    public async Task<List<PermissionVO>> Get(string? permissionDesc, string? permissionKey, long? permissionMenuId) {
        var permissions = await permissionRepository.Get(permissionDesc, permissionKey, permissionMenuId);
        var menusDic = await permissionRepository.GetEntityDic<Menu>(permissions.Select(p => p.MenuId).ToList());
        return permissions.Select(p => {
                                      var vo = _mapper.Map<PermissionVO>(p);
                                      if (menusDic.TryGetValue(p.MenuId, out var menu)) {
                                          vo.MenuName = menu.Name;
                                      }

                                      return vo;
                                  })
                          .ToList();
    }

    public async Task<List<PermissionMenuVO>> GetPermissionMenuList() {
        var permissions = await permissionRepository.GetAll();
        var menusDic = await permissionRepository.GetEntityDic<Menu>(permissions.Select(p => p.MenuId).ToList());
        return permissions.Select(p => {
                                      var vo = _mapper.Map<PermissionMenuVO>(p);
                                      if (menusDic.TryGetValue(p.MenuId, out var menu)) {
                                          vo.MenuName = menu.Name;
                                      }

                                      return vo;
                                  })
                          .DistinctBy(pmv => new {
                                                     pmv.MenuId,
                                                     pmv.MenuName
                                                 })
                          .ToList();
    }

    public async Task<BoolResult> UpdateOrInsert(PermissionDTO permissionDto) {
        var permission = _mapper.Map<Permission>(permissionDto);
        return await permissionRepository.UpdateOrInsert(permission);
    }

    public async Task<PermissionDTO> GetById(long id) {
        var permission = await permissionRepository.GetById(id);
        return _mapper.Map<PermissionDTO>(permission);
    }

    public async Task<BoolResult> Delete(long id) {
        return await permissionRepository.Delete(id);
    }
}