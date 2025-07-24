using Backend.Domain.IRepository;
using Backend.Modules.Blog.Domain.Entities;

namespace Backend.Modules.Blog.Domain.IRepository;

public interface IBlackListRepository : IBaseRepositories<BlackList> {
    Task<long?> GetIdByIp(string ip);
}