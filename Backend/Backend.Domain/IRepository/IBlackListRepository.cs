using Backend.Domain.Entity;

namespace Backend.Domain.IRepository;

public interface IBlackListRepository : IBaseRepositories<BlackList> {
    Task<long?> GetIdByIp(string ip);
}