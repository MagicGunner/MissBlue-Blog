using AutoMapper;
using Backend.Common.Results;
using Backend.Contracts;
using Backend.Domain;
using Backend.Modules.Blog.Contracts.DTO;
using Backend.Modules.Blog.Contracts.IService;
using Backend.Modules.Blog.Contracts.VO;
using Backend.Modules.Blog.Domain.Entities;

namespace Backend.Modules.Blog.Application.Service;

public class RoleService(IMapper                 mapper,
                         IBaseRepositories<Role> baseRepositories,
                         IBaseServices<Role>     baseServices) : IRoleService {
    public async Task<long> AddAsync(RoleDTO roleDto) => await baseServices.AddAsync(mapper.Map<Role>(roleDto));

    public async Task<bool> DeleteByIdsAsync(List<long> ids) => await baseServices.DeleteByIdsAsync(ids);

    public async Task<bool> UpdateStatusAsync(UpdateRoleStatusDTO roleStatusDto) => await baseServices.UpdateAsync(i => i.Id == roleStatusDto.Id, i => new Role { Status = roleStatusDto.Status });

    public Task<bool> UpdateAsync(RoleDTO roleDto) => throw new NotImplementedException();

    public async Task<RoleByIdVO?> GetByIdAsync(long id) => (await baseServices.QueryAsync<RoleByIdVO>(role => role.Id == id)).FirstOrDefault();

    public async Task<List<RoleAllVO>> SearchAsync(RoleSearchDTO roleSearchDto) => await baseServices.QueryAsync<RoleAllVO>(role => role.RoleName == roleSearchDto.RoleName);

    public async Task<List<RoleAllVO>> ListAllAsync() => await baseServices.QueryAsync<RoleAllVO>();
}