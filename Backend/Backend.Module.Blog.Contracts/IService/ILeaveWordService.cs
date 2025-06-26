using Backend.Common.Results;

namespace Backend.Modules.Blog.Contracts.IService;

public interface ILeaveWordService {
    Task<long> AddLeaveWordAsync(string content);
}