using Backend.Domain.Entity;
using Backend.Domain.IRepository;
using Backend.Infrastructure.UnitOfWorks;

namespace Backend.Infrastructure.Repository;

public class WebsiteInfoRepository(IUnitOfWorkManage unitOfWorkManage) : BaseRepositories<WebsiteInfo>(unitOfWorkManage), IWebsiteInfoRepository {
}