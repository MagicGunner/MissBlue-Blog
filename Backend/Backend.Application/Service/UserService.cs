using AutoMapper;
using Backend.Common.Results;
using Backend.Contracts.DTO;
using Backend.Contracts.IService;
using Backend.Contracts.VO;
using Backend.Domain;
using Backend.Domain.Entity;
using Microsoft.AspNetCore.Http;
using SqlSugar;

namespace Backend.Application.Service;

public class UserService(IMapper                 mapper,
                         IBaseRepositories<User> baseRepositories,
                         IBaseServices<User>     baseServices) : IUserService {
    private ISqlSugarClient Db => baseRepositories.Db;

    private static readonly Dictionary<string, string> UserDb = new() {
                                                                          {
                                                                              "admin", "123456"
                                                                          }, {
                                                                              "editor", "123456"
                                                                          }
                                                                      };

    private static readonly Dictionary<string, List<string>> RolePermissions = new() {
                                                                                         {
                                                                                             "Admin", new List<string> {
                                                                                                                           "blog:tag:add",
                                                                                                                           "blog:tag:delete"
                                                                                                                       }
                                                                                         }, {
                                                                                             "Editor", new List<string> {
                                                                                                                            "blog:article:publish"
                                                                                                                        }
                                                                                         }
                                                                                     };

    private static readonly Dictionary<string, string> UserRoles = new() {
                                                                             {
                                                                                 "admin", "Admin"
                                                                             }, {
                                                                                 "editor", "Editor"
                                                                             }
                                                                         };
    
    public Task<UserAccountVO> GetAccountByIdAsync() {
        throw new NotImplementedException();
    }

    public async Task<List<UserListVO>> ListAllAsync() => await baseServices.Query<UserListVO>();

    public bool ValidateUser(string userName, string password) {
        return UserDb.TryGetValue(userName, out var pwd) && pwd == password;
    }

    public List<string> GetUserPermissions(string userName) {
        if (UserRoles.TryGetValue(userName, out var role)) {
            return RolePermissions.TryGetValue(role, out var permissions) ? permissions : [];
        }

        return [];
    }

    public List<PermissionVO> GetAllPermissions() {
        return [
                   mapper.Map<PermissionVO>(new Permission {
                                                               PermissionKey = "blog:tag:add",
                                                               PermissionDesc = "新增标签"
                                                           })
               ];
    }

    public async Task<bool> RegisterAsync(UserRegisterDTO userRegisterDto, HttpContext context) {
        // 1. 判断用户名或邮箱是否存在
        var exists = await baseServices.Query<UserListVO>(i => i.Username == userRegisterDto.Username || i.Email == userRegisterDto.Email);
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
                                LoginTime = DateTime.Now,
                                IsDeleted = 0
                            };

        // 5. 保存用户
        var id = await baseServices.Add(user);
        return id > 0;
    }
}