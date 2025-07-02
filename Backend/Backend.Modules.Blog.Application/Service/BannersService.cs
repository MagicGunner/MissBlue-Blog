using AutoMapper;
using Backend.Application.Service;
using Backend.Domain.IRepository;
using Backend.Modules.Blog.Contracts.IService;
using Backend.Modules.Blog.Domain.Entities;
using Backend.Modules.Blog.Domain.IRepository;

namespace Backend.Modules.Blog.Application.Service;

public class BannersService(IMapper mapper, IBaseRepositories<Banners> baseRepositories, IBannersRepository bannersRepository) : BaseServices<Banners>(mapper, baseRepositories), IBannersService {
    public async Task<List<string>> GetBanners() => (await bannersRepository.Query()).Select(i => i.Path).ToList();
}