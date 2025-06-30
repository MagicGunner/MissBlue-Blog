using Backend.Contracts.DTO;
using Backend.Contracts.VO;
using Microsoft.AspNetCore.Http;

namespace Backend.Contracts.IService;

public interface IUserService {
    Task<UserAccountVO>    GetAccountByIdAsync();
    Task<List<UserListVO>> ListAllAsync();
    bool                   ValidateUser(string       userName, string password);
    List<string>           GetUserPermissions(string userName);
    List<PermissionVO>     GetAllPermissions();
    Task<bool>             RegisterAsync(UserRegisterDTO userRegisterDto, HttpContext context);
}