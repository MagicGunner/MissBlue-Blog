using Backend.Domain.IRepository;
using Backend.Modules.Blog.Domain.Entities;

namespace Backend.Modules.Blog.Domain.IRepository;

public interface ILeaveWordRepository : IBaseRepositories<LeaveWord> {
    /// <summary>
    /// 
    /// </summary>
    /// <param name="userName"></param>
    /// <param name="isCheck">默认查询当前用户名下所有已经通过审核的留言</param>
    /// <param name="startTime"></param>
    /// <param name="endTime"></param>
    /// <returns></returns>
    Task<List<LeaveWord>> GetBackList(string userName, int isCheck = 1, string? startTime = null, string? endTime = null);

    Task<List<LeaveWord>> GetList(string? id);

    Task<Dictionary<long, string>> GetContentDic(List<long> userIds);
}