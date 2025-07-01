using Backend.Contracts.DTO;
using Backend.Contracts.VO;
using Microsoft.AspNetCore.Http;

namespace Backend.Contracts.IService;

public interface IUserService {
    Task<UserAccountVO>      GetAccountByIdAsync();
    Task<List<UserListVO>>   ListAllAsync();
    Task<bool>               ValidateUser(string           userName, string password);
    Task<List<PermissionVO>> GetUserPermissions(string     userName);
    Task<bool>               RegisterAsync(UserRegisterDTO userRegisterDto, HttpContext context);
}