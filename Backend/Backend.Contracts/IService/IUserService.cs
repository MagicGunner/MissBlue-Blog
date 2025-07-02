using Backend.Contracts.DTO;
using Backend.Contracts.VO;
using Microsoft.AspNetCore.Http;

namespace Backend.Contracts.IService;

public interface IUserService {
    Task<UserAccountVO?>     GetAccountById();
    Task<List<UserListVO>>   ListAllAsync();
    Task<long>               ValidateUser(string           userName, string password);
    Task<List<PermissionVO>> GetUserPermissions(string     userName);
    Task<bool>               RegisterAsync(UserRegisterDTO userRegisterDto, HttpContext context);
    Task<bool>               UpdateUser(UserUpdateDTO      userUpdateDto);
}