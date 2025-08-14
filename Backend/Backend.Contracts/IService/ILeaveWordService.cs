using Backend.Contracts.DTO;
using Backend.Contracts.VO;

namespace Backend.Contracts.IService;

public interface ILeaveWordService {
    /// <summary>
    /// 添加用户留言
    /// </summary>
    /// <param name="content">留言内容</param>
    /// <returns></returns>
    Task<(bool isSuccess, string? msg)> AddLeaveWord(string content);

    Task<List<LeaveWordListVO>> GetBackList(SearchLeaveWordDTO? searchLeaveWordDto = null);
    Task<List<LeaveWordVO>>     GetList(string?                 id);
    Task<bool>                  SetIsCheck(LeaveWordIsCheckDTO  leaveWordIsCheckDto);
    Task<bool>                  Delete(List<long>               leaveWordIds);
}