using Backend.Domain.Entity;

namespace Backend.Domain.IRepository;

public interface ILeaveWordRepository : IBaseRepositories<LeaveWord> {
    Task<List<LeaveWord>> GetList(string? id);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="userName"></param>
    /// <param name="isCheck">默认查询当前用户名下所有已经通过审核的留言</param>
    /// <param name="startTime"></param>
    /// <param name="endTime"></param>
    /// <returns></returns>
    Task<List<LeaveWord>> GetBackList(string? userName, int? isCheck, string? startTime, string? endTime);

    Task<Dictionary<long, string>> GetContentDic(List<long> userIds);
    Task<bool>                     SetIsChecked(long        leaveWordId, int isChecked);
}