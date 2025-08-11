using AutoMapper;
using Backend.Application.Service;
using Backend.Domain.Entity;
using Backend.Domain.IRepository;
using Backend.Modules.Blog.Contracts.IService;

namespace Backend.Modules.Blog.Application.Service;

public class PermissionService(IMapper mapper, IBaseRepositories<Permission> baseRepositories) : BaseServices<Permission>(mapper, baseRepositories), IPermissionService {
}