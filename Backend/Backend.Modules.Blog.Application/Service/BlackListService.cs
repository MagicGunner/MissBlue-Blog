using AutoMapper;
using Backend.Application.Interface;
using Backend.Application.Service;
using Backend.Common.Extensions;
using Backend.Common.IP;
using Backend.Common.Utils;
using Backend.Domain.IRepository;
using Backend.Modules.Blog.Contracts.DTO;
using Backend.Modules.Blog.Contracts.IService;
using Backend.Modules.Blog.Domain.Entities;
using Backend.Modules.Blog.Domain.IRepository;

namespace Backend.Modules.Blog.Application.Service;

public class BlackListService(IMapper mapper, IBaseRepositories<BlackList> baseRepositories, IBlackListRepository blackListRepository, ICurrentUser currentUser)
    : BaseServices<BlackList>(mapper, baseRepositories), IBlackListService {
    private readonly IMapper _mapper = mapper;

    public Task<bool> AddBlackList(AddBlackListDTO addBlackListDto) {
        throw new NotImplementedException();
    }

    private async Task<bool> SaveBlackList(AddBlackListDTO addBlackListDto, int index) {
        var isUser = addBlackListDto.UserIds.Count != 0;

        var blackList = new BlackList {
                                          UserId = isUser ? addBlackListDto.UserIds[index] : null,
                                          Reason = addBlackListDto.Reason,
                                          Type = isUser ? (int)BlackListEnum.BlackListTypeUser : (int)BlackListEnum.BlackListTypeBot,
                                          ExpiresTime = addBlackListDto.ExpiresTime
                                      };
        var ipInfo = new BlackListIpInfo { CreateIp = isUser ? null : currentUser.IpAddress };
        // 若是 IP 黑名单，查是否已存在
        if (!isUser) {
            var existingId = await blackListRepository.GetIdByIp(ipInfo.CreateIp!);
            if (existingId != null) {
                blackList.Id = existingId.Value;
            }
        }

        var result = await blackListRepository.InsertOrUpdate(blackList);

        // if (result) {
        //     if (blackList.Type == BlackListConst.BlackListTypeBot) {
        //         await ipService.RefreshIpDetailAsyncByBid(blackList.Id!.Value);
        //     }
        //
        //     await UpdateBlackListCacheAsync(blackList);
        //     return true;
        // }

        return false;
    }
}