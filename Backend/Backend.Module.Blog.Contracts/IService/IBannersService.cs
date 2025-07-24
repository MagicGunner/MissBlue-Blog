using Backend.Modules.Blog.Contracts.DTO;
using Backend.Modules.Blog.Contracts.VO;
using Microsoft.AspNetCore.Http;

namespace Backend.Modules.Blog.Contracts.IService;

public interface IBannersService {
    Task<List<string>> GetBanners();

    Task<List<BannersVO>> BackGetBanners();

    Task<bool> RemoveBannerById(long id);

    Task<(BannersVO? bannersVo, string? msg)> UploadBannerImage(IFormFile bannerImage);

    Task<(bool success, string msg)> UpdateSortOrder(List<BannersDTO> bannersDtos);
}