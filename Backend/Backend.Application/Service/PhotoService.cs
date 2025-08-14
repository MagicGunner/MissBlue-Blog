using AutoMapper;
using Backend.Contracts.IService;
using Backend.Domain.Entity;
using Backend.Domain.IRepository;

namespace Backend.Application.Service;

public class PhotoService(IMapper mapper, IBaseRepositories<Photo> baseRepositories) : BaseServices<Photo>(mapper, baseRepositories), IPhotoService {
}