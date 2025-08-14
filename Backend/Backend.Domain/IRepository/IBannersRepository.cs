using Backend.Domain.Entity;

namespace Backend.Domain.IRepository;

public interface IBannersRepository : IBaseRepositories<Banners> {
    Task<List<string>> GetBanners();

    Task<List<Banners>> BackGetBanners();

    Task<Banners?> RemoveBannerById(long id);

    Task<(bool success, string msg)> UpdateSortOrder(List<Banners> banners);
}