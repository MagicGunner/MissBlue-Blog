using Backend.Modules.Blog.Contracts.DTO;

namespace Backend.Modules.Blog.Contracts.IService;

public interface IBlackListService {
    Task<bool> AddBlackList(AddBlackListDTO addBlackListDto);
}