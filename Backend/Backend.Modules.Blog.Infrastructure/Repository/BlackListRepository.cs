using Backend.Infrastructure.Repository;
using Backend.Infrastructure.UnitOfWorks;
using Backend.Modules.Blog.Domain.Entities;
using Backend.Modules.Blog.Domain.IRepository;

namespace Backend.Modules.Blog.Infrastructure.Repository;

public class BlackListRepository(IUnitOfWorkManage unitOfWorkManage) : BaseRepositories<BlackList>(unitOfWorkManage), IBlackListRepository {
    public async Task<long?> GetIdByIp(string ip) {
        throw new NotImplementedException();
    }
}