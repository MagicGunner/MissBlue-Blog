using Backend.Domain.Entity;

namespace Backend.Domain.IRepository;

public interface ITreeHoleRepository : IBaseRepositories<TreeHole> {
    Task<List<TreeHole>> GetFrontlist();
    Task<List<TreeHole>> GetBackList(string? userName, int? isCheck, string? startTime, string? endTime);
    Task<bool>           SetCheck(long       id,       int  isCheck);
}