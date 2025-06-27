using AutoMapper;
using Backend.Common.Results;
using Backend.Contracts;
using Backend.Domain;
using Backend.Modules.Blog.Contracts.IService;
using Backend.Modules.Blog.Contracts.VO;
using Backend.Modules.Blog.Domain.Entities;
using SqlSugar;

namespace Backend.Modules.Blog.Application.Service;

public class UserService(IMapper                 mapper,
                         IBaseRepositories<User> baseRepositories,
                         IBaseServices<User>     baseServices) : IUserService {
    public Task<UserAccountVO> GetAccountByIdAsync() {
        throw new NotImplementedException();
    }

    public async Task<List<UserListVO>> ListAllAsync() => await baseServices.Query<UserListVO>();
}