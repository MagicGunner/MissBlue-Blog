using Backend.Contracts.DTO;
using Backend.Contracts.VO;

namespace Backend.Contracts.IService;

public interface ITreeHoleService {
    Task<bool>                 Add(AddTreeHoleDto dto);
    Task<List<TreeHoleVO>>     GetFrontList();
    Task<List<TreeHoleListVO>> GetBackList(SearchTreeHoleDTO? dto);

    Task<bool> SetCheck(TreeHoleIsCheckDTO dto);
    Task<bool> DeleteByIds(List<long>      ids);
}