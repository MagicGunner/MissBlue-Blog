using Backend.Domain.Entity;
using Backend.Domain.IRepository;
using Backend.Infrastructure.UnitOfWorks;

namespace Backend.Infrastructure.Repository;

public class PhotoRepository(IUnitOfWorkManage unitOfWorkManage) : BaseRepositories<Photo>(unitOfWorkManage), IPhotoRepository {
}