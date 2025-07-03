using AutoMapper;
using Backend.Application.Interface;
using Backend.Application.Service;
using Backend.Common.Results;
using Backend.Domain.IRepository;
using Backend.Modules.Blog.Contracts.IService;
using Backend.Modules.Blog.Domain.Entities;

namespace Backend.Modules.Blog.Application.Service;

public class TreeHoleService(IMapper mapper, IBaseRepositories<TreeHole> baseRepositories, ICurrentUser currentUser) : BaseServices<TreeHole>(mapper, baseRepositories), ITreeHoleService {
    public Task<bool> AddTreeHole(string content) {
        throw new NotImplementedException();
    }
}