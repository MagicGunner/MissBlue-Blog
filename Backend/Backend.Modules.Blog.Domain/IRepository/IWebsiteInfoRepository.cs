using Backend.Domain;
using Backend.Domain.Entity;
using Backend.Domain.IRepository;
using Backend.Modules.Blog.Domain.Entities;

namespace Backend.Modules.Blog.Domain.IRepository;

public interface IWebsiteInfoRepository : IBaseRepositories<WebsiteInfo> {
}