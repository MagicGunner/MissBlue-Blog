using Backend.Common.Results;
using Backend.Modules.Blog.Contracts.DTO;
using Backend.Modules.Blog.Contracts.VO;

namespace Backend.Modules.Blog.Contracts.IService;

public interface ILeaveWordService {
    Task<long>                  AddLeaveWordAsync(string        content);
    Task<List<LeaveWordListVO>> GetBackList(SearchLeaveWordDTO? searchLeaveWordDto = null);
    Task<List<LeaveWordVO>>     GetList(string?                 id);
}