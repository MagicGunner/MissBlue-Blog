using Backend.Domain.Entity;
using Backend.Infrastructure;
using Backend.Infrastructure.Repository;
using Backend.Infrastructure.UnitOfWorks;
using Backend.Modules.Blog.Domain.Entities;
using Backend.Modules.Blog.Domain.IRepository;

namespace Backend.Modules.Blog.Infrastructure.Repository;

public class WebsiteInfoRepository(IUnitOfWorkManage unitOfWorkManage) : BaseRepositories<WebsiteInfo>(unitOfWorkManage), IWebsiteInfoRepository {
}