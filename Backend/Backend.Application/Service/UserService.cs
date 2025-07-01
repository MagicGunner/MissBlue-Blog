using AutoMapper;
using Backend.Common.Results;
using Backend.Common.Utils;
using Backend.Contracts.DTO;
using Backend.Contracts.IService;
using Backend.Contracts.VO;
using Backend.Domain;
using Backend.Domain.Entity;
using Backend.Domain.IRepository;
using Microsoft.AspNetCore.Http;
using SqlSugar;

namespace Backend.Application.Service;

public class UserService(IMapper                 mapper,
                         IBaseRepositories<User> baseRepositories,
                         IUserRepository         userRepository) : BaseServices<User>(mapper, baseRepositories), IUserService {
    private readonly IMapper _mapper = mapper;

    public Task<UserAccountVO> GetAccountByIdAsync() {
        throw new NotImplementedException();
    }

    public async Task<List<UserListVO>> ListAllAsync() => await Query<UserListVO>();

    public async Task<bool> ValidateUser(string userName, string password) => await userRepository.ValidateUser(userName, password);

    public async Task<List<PermissionVO>> GetUserPermissions(string userName) {
        var permissions = await userRepository.GetUserPermissions(userName);
        return permissions.Select(i => _mapper.Map<PermissionVO>(i)).ToList();
    }

    public async Task<bool> RegisterAsync(UserRegisterDTO userRegisterDto, HttpContext context) {
        // 1. 判断用户名或邮箱是否存在
        var exists = await Query<UserListVO>(i => i.Username == userRegisterDto.Username || i.Email == userRegisterDto.Email);
        if (exists.Count > 0) {
            return false;
        }

        // 2. 密码加密
        var encryptedPassword = BCrypt.Net.BCrypt.HashPassword(userRegisterDto.Password);

        // 3. 获取IP地址
        var ip = context.Connection.RemoteIpAddress?.ToString() ?? "unknown";

        // 4. 构建用户对象
        var user = new User {
                                Username = userRegisterDto.Username,
                                Nickname = userRegisterDto.Username,
                                Password = encryptedPassword,
                                Email = userRegisterDto.Email,
                                Avatar = "https://default.avatar.png",
                                Gender = 0,
                                Intro = "这个人很懒，什么都没有写",
                                RegisterType = 0,
                                RegisterIp = ip,
                                RegisterAddress = IpUtils.GetIpAddr(context) ?? "unknown",
                                LoginTime = DateTime.Now,
                                IsDeleted = 0
                            };

        // 5. 保存用户
        var id = await Add(user);
        return id > 0;
    }
}