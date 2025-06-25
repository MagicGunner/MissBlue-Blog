using Backend.Common.Results;
using Backend.Modules.Blog.Contracts.VO;

namespace Backend.Modules.Blog.Contracts.IService;

public interface IUserService {
    Task<ResponseResult<UserAccountVO>> GetAccountByIdAsync();
}