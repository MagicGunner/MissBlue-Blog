using Backend.Contracts.DTO;

namespace Backend.Contracts.IService;

public interface IBlackListService {
    Task<bool> AddBlackList(AddBlackListDTO addBlackListDto);
}