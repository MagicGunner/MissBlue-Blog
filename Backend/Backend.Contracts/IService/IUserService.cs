using Backend.Common.Record;
using Backend.Contracts.DTO;
using Backend.Contracts.VO;
using Microsoft.AspNetCore.Http;

namespace Backend.Contracts.IService;

public interface IUserService {
    Task<UserAccountVO?>     GetAccountById();
    Task<List<UserListVO>>   ListAllAsync();
    Task<long>               ValidateUser(string                 userName, string password);
    Task<List<PermissionVO>> GetUserPermissions(string           userName);
    Task<bool>               Register(UserRegisterDTO            userRegisterDto, HttpContext context);
    Task<bool>               Update(UserUpdateDTO                userUpdateDto);
    Task<BoolResult>         UpdateEmailAndVerify(UpdateEmailDTO dto);
    Task<BoolResult>         ThirdUpdateEmail(UpdateEmailDTO     dto);
    Task<bool>               ResetConfirm(UserResetConfirmDTO    dto);
    Task<bool>               ResetPassword(UserResetPasswordDTO  dto);
    Task<List<UserListVO>>   GetOrSearch(UserSearchDTO?          dto);
    Task<bool>               UpdateStatus(UpdateRoleStatusDTO    dto);
    Task<UserDetailsVO>      GetUserDetails(long                 id);
    Task<bool>               Delete(UserDeleteDTO                dto);
}