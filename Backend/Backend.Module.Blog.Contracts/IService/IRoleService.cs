using Backend.Common.Results;
using Backend.Contracts.DTO;
using Backend.Contracts.VO;
using Backend.Modules.Blog.Contracts.DTO;
using Backend.Modules.Blog.Contracts.VO;

namespace Backend.Modules.Blog.Contracts.IService;

public interface IRoleService {
    Task<List<RoleVO>> SelectAll();
}