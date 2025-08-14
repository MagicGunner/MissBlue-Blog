using Backend.Contracts.VO;

namespace Backend.Contracts.IService;

public interface IWebsiteInfoService {
    Task<WebsiteInfoVO> GetWebsiteInfo();
}