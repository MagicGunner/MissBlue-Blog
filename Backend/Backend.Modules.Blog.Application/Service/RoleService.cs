using AutoMapper;
using Backend.Application.Service;
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
using Backend.Modules.Blog.Domain.IRepository;

namespace Backend.Modules.Blog.Application.Service;

public class RoleService(IMapper                 mapper,
                         IBaseRepositories<Role> baseRepositories,
                         IRoleRepository         roleRepository) : BaseServices<Role>(mapper, baseRepositories), IRoleService {
    private readonly IMapper _mapper = mapper;

    public async Task<List<RoleVO>> SelectAll() {
        var roles = await roleRepository.SelectAll();
        var roleVOs = _mapper.Map<List<RoleVO>>(roles);
        return roleVOs;
    }
}