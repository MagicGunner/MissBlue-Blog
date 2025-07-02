namespace Backend.Modules.Blog.Contracts.IService;

public interface IBannersService {
    Task<List<string>> GetBanners();
}