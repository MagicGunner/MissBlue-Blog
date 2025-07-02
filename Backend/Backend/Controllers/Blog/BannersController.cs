using Backend.Modules.Blog.Contracts.IService;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers.Blog;

[ApiController]
[Route("api/banners")]
public class BannersController(IBannersService bannersService) : ControllerBase {
    [HttpGet("list")]
    public async Task<List<string>> GetBanners() => await bannersService.GetBanners();
}