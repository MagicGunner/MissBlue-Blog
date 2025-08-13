using Backend.Infrastructure.Repository;
using Backend.Infrastructure.UnitOfWorks;
using Backend.Modules.Blog.Domain.Entities;
using Backend.Modules.Blog.Domain.IRepository;

namespace Backend.Modules.Blog.Infrastructure.Repository;

public class PhotoRepository(IUnitOfWorkManage unitOfWorkManage) : BaseRepositories<Photo>(unitOfWorkManage), IPhotoRepository {
}