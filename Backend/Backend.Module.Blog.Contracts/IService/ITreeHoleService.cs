using Backend.Modules.Blog.Contracts.DTO;
using Backend.Modules.Blog.Contracts.VO;

namespace Backend.Modules.Blog.Contracts.IService;

public interface ITreeHoleService {
    Task<bool>                 Add(string content);
    Task<List<TreeHoleVO>>     GetFrontList();
    Task<List<TreeHoleListVO>> GetBackList(SearchTreeHoleDTO? dto);

    Task<bool> SetCheck(TreeHoleIsCheckDTO dto);
    Task<bool> DeleteByIds(List<long>      ids);
}