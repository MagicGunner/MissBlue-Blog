using AutoMapper;
using Backend.Application.Interface;
using Backend.Common.Enums;
using Backend.Common.Extensions;
using Backend.Common.Static;
using Backend.Contracts.DTO;
using Backend.Contracts.IService;
using Backend.Contracts.VO;
using Backend.Domain.Entity;
using Backend.Domain.IRepository;
using Microsoft.AspNetCore.Http;

namespace Backend.Application.Service;

public class BannersService(IMapper mapper, IBaseRepositories<Banners> baseRepositories, IBannersRepository bannersRepository, IMinIOService minIoService, ICurrentUser currentUser)
    : BaseServices<Banners>(mapper, baseRepositories), IBannersService {
    private readonly IMapper            _mapper = mapper;
    public async     Task<List<string>> GetBanners() => await bannersRepository.GetBanners();

    public async Task<List<BannersVO>> BackGetBanners() => (await bannersRepository.BackGetBanners()).Select(_mapper.Map<BannersVO>).ToList();

    public async Task<bool> RemoveBannerById(long id) {
        var banners = await bannersRepository.RemoveBannerById(id);
        if (banners == null) return false;
        var dir = UploadRules.Get(UploadEnum.UiBanners);
        var fileName = minIoService.GetFileName(banners.Path);
        var fullPath = $"{dir}{fileName}";
        var exists = await minIoService.FileExistsAsync(fullPath);
        if (exists) await minIoService.DeleteAsync(fullPath);
        return true;
    }

    public async Task<(BannersVO? bannersVo, string? msg)> UploadBannerImage(IFormFile bannerImage) {
        try {
            // 判断数量是否达到上限（假设常量已定义）
            var currentCount = await Db.Queryable<Banners>().CountAsync();
            if (currentCount >= SQLConst.BANNER_MAX_COUNT) return (null, RespDesc.BannerMaxCountMsg.GetDescription());

            // 上传图片到 MinIO
            var url = await minIoService.UploadAsync(UploadEnum.UiBanners, bannerImage);

            // 构造 Banner 实体
            var banner = new Banners {
                                         Path = url,
                                         Size = bannerImage.Length,
                                         Type = bannerImage.ContentType,
                                         UserId = currentUser.UserId ?? 0, // 从上下文中拿用户ID
                                         SortOrder = (int)(currentCount + 1),
                                         CreateTime = DateTime.Now
                                     };

            // 插入数据库
            await Db.Insertable(banner).ExecuteCommandAsync();

            return (_mapper.Map<BannersVO>(banner), null);
        } catch (FileUploadException ex) {
            return (null, ex.Message);
        } catch (Exception ex) {
            return (null, "上传失败，请稍后重试");
        }
    }

    public async Task<(bool success, string msg)> UpdateSortOrder(List<BannersDTO> bannersDtos) => await bannersRepository.UpdateSortOrder(bannersDtos.Select(b => _mapper.Map<Banners>(b)).ToList());
}