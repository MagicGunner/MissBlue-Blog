using AutoMapper;
using Backend.Application.Interface;
using Backend.Common.Const;
using Backend.Common.Record;
using Backend.Common.Redis;
using Backend.Common.Utils;
using Backend.Contracts.DTO;
using Backend.Contracts.IService;
using Backend.Contracts.VO;
using Backend.Domain.Entity;
using Backend.Domain.IRepository;
using Microsoft.AspNetCore.Http;

namespace Backend.Application.Service;

public class UserService(IMapper                 mapper,
                         IBaseRepositories<User> baseRepositories,
                         IUserRepository         userRepository,
                         IUserRoleRepository     userRoleRepository,
                         IRedisBasketRepository  redisBasketRepository,
                         ICurrentUser            currentUser) : BaseServices<User>(mapper, baseRepositories), IUserService {
    private readonly IMapper _mapper = mapper;

    public async Task<UserAccountVO?> GetAccountById() {
        var userId = currentUser.UserId;
        if (userId == null) {
            return null;
        }

        var user = await userRepository.GetById(userId.Value);
        return _mapper.Map<UserAccountVO>(user);
    }

    public async Task<List<UserListVO>> ListAllAsync() => await Query<UserListVO>();

    public async Task<long> ValidateUser(string userName, string password) => await userRepository.ValidateUser(userName, password);

    public async Task<List<PermissionVO>> GetUserPermissions(string userName) {
        var permissions = await userRepository.GetUserPermissions(userName);
        return permissions.Select(i => _mapper.Map<PermissionVO>(i)).ToList();
    }

    public async Task<bool> Register(UserRegisterDTO userRegisterDto, HttpContext context) {
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

    public async Task<bool> Update(UserUpdateDTO userUpdateDto) {
        var userId = currentUser.UserId;
        if (userId is null or <= 0) {
            return false;
        }

        var user = _mapper.Map<User>(userUpdateDto);
        user.Id = userId.Value;
        // 数据库更新
        return await Update(user);
    }

    public async Task<BoolResult> UpdateEmailAndVerify(UpdateEmailDTO dto) {
        if (currentUser.UserId == null) {
            return new BoolResult(false, "用户未登录");
        }

        var user = await userRepository.GetById(currentUser.UserId.Value);
        if (user == null) {
            return new BoolResult(false, "用户不存在");
        }

        // 邮箱是否改变
        if (user.Email == dto.Email) return new BoolResult(false, "新邮箱与当前邮箱相同");

        // 判断邮箱是否已被注册
        if (await userRepository.UserExists(user.Username, dto.Email)) return new BoolResult(false, "该邮箱已被注册");

        if (!await VerifyEmailCode(dto.Email, dto.Code, RedisConst.RESET_EMAIL)) return new BoolResult(false, "验证码错误或已过期");
        // 验证码正确，更新邮箱
        user.Email = dto.Email;
        var isUpdated = await userRepository.Update(user);
        if (!isUpdated) return new BoolResult(false, "邮箱更新失败");
        // 删除验证码缓存
        await redisBasketRepository.Remove($"{RedisConst.VERIFY_CODE}{RedisConst.RESET_EMAIL}{RedisConst.SEPARATOR}{dto.Email}");
        return new BoolResult(true, "邮箱更新成功");
    }

    public async Task<BoolResult> ThirdUpdateEmail(UpdateEmailDTO dto) {
        if (currentUser.UserId == null) {
            return new BoolResult(false, "用户未登录");
        }

        var user = await userRepository.GetById(currentUser.UserId.Value);
        if (user == null) {
            return new BoolResult(false, "用户不存在");
        }

        // 邮箱是否改变
        if (user.Email != null && user.Email == dto.Email) return new BoolResult(false, "新邮箱与当前邮箱相同");
        // 判断邮箱是否已被注册
        if (await userRepository.UserExists(null, dto.Email)) return new BoolResult(false, "该邮箱已被注册");
        if (!await VerifyEmailCode(dto.Email, dto.Code, RedisConst.RESET_EMAIL)) return new BoolResult(false, "验证码错误或已过期");
        // 验证码正确，更新邮箱
        user.Email = dto.Email;
        var isUpdated = await userRepository.Update(user);
        if (!isUpdated) return new BoolResult(false, "邮箱更新失败");
        // 删除验证码缓存
        await redisBasketRepository.Remove($"{RedisConst.VERIFY_CODE}{RedisConst.RESET_EMAIL}{RedisConst.SEPARATOR}{dto.Email}");
        return new BoolResult(true, "邮箱更新成功");
    }

    public async Task<bool> ResetConfirm(UserResetConfirmDTO dto) {
        return await VerifyEmailCode(dto.Email, dto.Code, RedisConst.RESET);
    }

    public async Task<bool> ResetPassword(UserResetPasswordDTO dto) {
        if (!await VerifyEmailCode(dto.Email, dto.Code, RedisConst.RESET)) return false;
        var password = BCrypt.Net.BCrypt.HashPassword(dto.Password);
        if (!await userRepository.ResetPassWord(password, dto.Email)) return false;
        // 删除验证码缓存
        await redisBasketRepository.Remove($"{RedisConst.VERIFY_CODE}{RedisConst.RESET}{RedisConst.SEPARATOR}{dto.Email}");
        return true;
    }

    public async Task<List<UserListVO>> GetOrSearch(UserSearchDTO? dto) {
        var users = await userRepository.GetOrSearch(dto?.UserName, dto?.Email, dto?.IsDisable, dto?.CreateTimeStart, dto?.CreateTimeEnd);
        return users.Select(u => _mapper.Map<UserListVO>(u)).ToList();
    }

    public async Task<bool> UpdateStatus(UpdateRoleStatusDTO dto) {
        return await userRepository.UpdateStatus(dto.Id, dto.Status);
    }

    public async Task<UserDetailsVO> GetUserDetails(long id) {
        var user = await userRepository.GetById(id);
        if (user == null) {
            throw new Exception("用户不存在");
        }

        var userDetails = _mapper.Map<UserDetailsVO>(user);
        // 获取用户角色
        var userRoles = await userRoleRepository.GetUserId(id);
        var roleDic = await userRoleRepository.GetEntityDic<Role>(userRoles.Select(ur => ur.RoleId).ToList());
        userDetails.Roles = userRoles.Select(ur => roleDic.TryGetValue(ur.RoleId, out var role) ? role.RoleName : null)
                                     .Where(name => !string.IsNullOrEmpty(name))
                                     .ToList()!;
        return userDetails;
    }

    public async Task<bool> Delete(UserDeleteDTO dto) {
        return await userRepository.DeleteByIds(dto.Ids);
    }

    /// <summary>
    /// 判断验证码是否正确
    /// </summary>
    /// <param name="email"></param>
    /// <param name="code"></param>
    /// <param name="type"></param>
    /// <returns></returns>
    private async Task<bool> VerifyEmailCode(string email, string code, string type) {
        var redisCode = await redisBasketRepository.GetValue($"{RedisConst.VERIFY_CODE}{type}{RedisConst.SEPARATOR}{email}");
        return redisCode == code;
    }
}