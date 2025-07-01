using AutoMapper;
using Backend.Contracts;
using Backend.Contracts.DTO;
using Backend.Contracts.IService;
using Backend.Contracts.VO;
using Backend.Domain;
using Backend.Domain.Entity;
using Backend.Domain.IRepository;
using Backend.Modules.Blog.Contracts.DTO;
using Backend.Modules.Blog.Contracts.IService;
using Backend.Modules.Blog.Contracts.VO;
using Backend.Modules.Blog.Domain.Entities;

namespace Backend.Modules.Blog.Application.Service;

public class RoleService(IMapper                 mapper,
                         IBaseRepositories<Role> baseRepositories,
                         IBaseServices<Role>     baseServices) : IRoleService {
    public async Task<long> AddAsync(RoleDTO roleDto) => await baseServices.Add(mapper.Map<Role>(roleDto));

    public async Task<bool> DeleteByIdsAsync(List<long> ids) => await baseServices.DeleteByIds(ids);

    public async Task<bool> UpdateStatusAsync(UpdateRoleStatusDTO roleStatusDto) => await baseServices.Update(i => i.Id == roleStatusDto.Id, i => new Role { Status = roleStatusDto.Status });

    public Task<bool> UpdateAsync(RoleDTO roleDto) => throw new NotImplementedException();

    public async Task<RoleByIdVO?> GetByIdAsync(long id) => (await baseServices.Query<RoleByIdVO>(role => role.Id == id)).FirstOrDefault();

    public async Task<List<RoleAllVO>> SearchAsync(RoleSearchDTO roleSearchDto) =>
        await baseServices.QueryWithMulti<RoleAllVO>(query =>
                                                         query.WhereIF(!string.IsNullOrEmpty(roleSearchDto.RoleName), x => x.RoleName.Contains(roleSearchDto.RoleName))
                                                              .WhereIF(!string.IsNullOrEmpty(roleSearchDto.RoleKey), x => x.RoleKey.Contains(roleSearchDto.RoleKey))
                                                              .WhereIF(roleSearchDto.Status != null, x => x.Status == roleSearchDto.Status)
                                                              .WhereIF(roleSearchDto is { CreateTimeStart: not null, CreateTimeEnd: not null },
                                                                       x => x.CreateTime >= roleSearchDto.CreateTimeStart && x.CreateTime <= roleSearchDto.CreateTimeEnd)
                                                              .OrderBy(x => x.OrderNum)
                                                    );

    public async Task<List<RoleAllVO>> ListAllAsync() => await baseServices.Query<RoleAllVO>();
}