using Backend.Common.Results;
using Backend.Modules.Blog.Contracts.DTO;
using Backend.Modules.Blog.Contracts.VO;

namespace Backend.Modules.Blog.Contracts.IService;

public interface ILeaveWordService {
    /// <summary>
    /// 添加用户留言
    /// </summary>
    /// <param name="content">留言内容</param>
    /// <returns></returns>
    Task<bool> AddLeaveWord(string content);

    Task<List<LeaveWordListVO>> GetBackList(SearchLeaveWordDTO? searchLeaveWordDto = null);
    Task<List<LeaveWordVO>>     GetList(string?                 id);
    Task<bool>                  SetIsCheck(LeaveWordIsCheckDTO  leaveWordIsCheckDto);
    Task<bool>                  Delete(List<long>               leaveWordIds);
}