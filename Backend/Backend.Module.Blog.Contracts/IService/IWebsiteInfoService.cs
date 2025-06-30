using Backend.Modules.Blog.Contracts.VO;

namespace Backend.Modules.Blog.Contracts.IService;

public interface IWebsiteInfoService {
    Task<WebsiteInfoVO> GetWebsiteInfo();
}