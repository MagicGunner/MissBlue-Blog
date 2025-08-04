using Backend.Modules.Blog.Contracts.DTO;
using Backend.Modules.Blog.Contracts.VO;
using Microsoft.AspNetCore.Http;

namespace Backend.Modules.Blog.Contracts.IService;

public interface ILinkService {
    Task<(bool isSuccess, string? msg)> ApplyLink(LinkDTO linkDto);
    Task<List<LinkVO>>                  GetLinkList();
    Task<List<LinkListVO>>              GetBackLinkList(SearchLinkDTO? searchDto);
    Task<bool>                          SetChecked(LinkIsCheckDTO      isCheckDto);
    Task<bool>                          Delete(List<long>              ids);
    Task                                EmailApply(string              verifyCode, HttpResponse response);
}