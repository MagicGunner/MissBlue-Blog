using Backend.Common.Results;

namespace Backend.Modules.Blog.Contracts.IService;

public interface ILeaveWordService {
    Task<ResponseResult<object>> AddLeaveWordAsync(string content);
}