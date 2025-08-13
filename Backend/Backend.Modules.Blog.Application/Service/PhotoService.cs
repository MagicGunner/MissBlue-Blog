using AutoMapper;
using Backend.Application.Service;
using Backend.Domain.IRepository;
using Backend.Modules.Blog.Contracts.IService;
using Backend.Modules.Blog.Domain.Entities;

namespace Backend.Modules.Blog.Application.Service;

public class PhotoService(IMapper mapper, IBaseRepositories<Photo> baseRepositories) : BaseServices<Photo>(mapper, baseRepositories), IPhotoService {
}