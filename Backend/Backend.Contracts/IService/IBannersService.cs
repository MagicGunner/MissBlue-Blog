using Backend.Contracts.DTO;
using Backend.Contracts.VO;
using Microsoft.AspNetCore.Http;

namespace Backend.Contracts.IService;

public interface IBannersService {
    Task<List<string>> GetBanners();

    Task<List<BannersVO>> BackGetBanners();

    Task<bool> RemoveBannerById(long id);

    Task<(BannersVO? bannersVo, string? msg)> UploadBannerImage(IFormFile bannerImage);

    Task<(bool success, string msg)> UpdateSortOrder(List<BannersDTO> bannersDtos);
}