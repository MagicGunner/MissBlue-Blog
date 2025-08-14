using AutoMapper;
using Backend.Application.Interface;
using Backend.Common.Const;
using Backend.Common.Enums;
using Backend.Common.Redis;
using Backend.Common.Utils;
using Backend.Contracts.DTO;
using Backend.Contracts.IService;
using Backend.Contracts.VO;
using Backend.Domain.Entity;
using Backend.Domain.IRepository;
using Microsoft.AspNetCore.Http;

namespace Backend.Application.Service;

public class LinkService(
    IMapper                 mapper,
    IBaseRepositories<Link> baseRepositories,
    ILinkRepository         linkRepository,
    ICurrentUser            currentUser,
    IPublicService          publicService,
    IRedisBasketRepository  redisBasketRepository
)
    : BaseServices<Link>(mapper, baseRepositories), ILinkService {
    private readonly IMapper                 _mapper           = mapper;
    private readonly IBaseRepositories<Link> _baseRepositories = baseRepositories;

    public async Task<(bool isSuccess, string? msg)> ApplyLink(LinkDTO linkDto) {
        var link = _mapper.Map<Link>(linkDto);
        if (currentUser.UserId == null) return (false, "用户未登录");
        link.UserId = currentUser.UserId.Value;
        var result = await linkRepository.Add(link);
        if (result <= 0) return (false, "申请链接失败，请稍后再试");
        // todo 向站长邮箱发送申请链接通知
        return (true, "申请链接成功，请耐心等待审核");
    }

    public async Task<List<LinkVO>> GetLinkList() {
        var links = await linkRepository.GetLinkList();
        var userDic = await _baseRepositories.GetEntityDic<User>(links.Select(l => l.UserId).ToList());
        return links.Select(l => {
                                var vo = _mapper.Map<LinkVO>(l);
                                if (userDic.TryGetValue(l.UserId, out var user)) vo.Avatar = user.Avatar;
                                return vo;
                            })
                    .ToList();
    }

    public async Task<List<LinkListVO>> GetBackLinkList(SearchLinkDTO? searchDto) {
        var links = searchDto == null
                        ? await linkRepository.Query()
                        : await linkRepository.GetBackLinkList(searchDto.UserName, searchDto.Name, searchDto.IsCheck, searchDto.StartTime, searchDto.EndTime);
        var userDic = await _baseRepositories.GetEntityDic<User>(links.Select(l => l.UserId).ToList());
        return links.Select(l => {
                                var vo = _mapper.Map<LinkListVO>(l);
                                if (userDic.TryGetValue(l.UserId, out var user)) vo.UserName = user.Username;
                                return vo;
                            })
                    .ToList();
    }

    public async Task<bool> SetChecked(LinkIsCheckDTO isCheckDto) {
        return await linkRepository.SetIsChecked(isCheckDto.Id, isCheckDto.IsCheck);

        // todo 如果审核通过，发送邮件通知申请人

        // if (isCheckDto.IsCheck == SQLConst.STATUS_PUBLIC && _settings.PassNotice) {
        //     var email = await Db.Queryable<Link>().Where(l => l.Id == isCheckDto.Id).Select(l => l.Email).FirstAsync();
        //     publicService.SendEmail(MailboxAlertsEnum.FRIEND_LINK_APPLICATION_PASS.CodeStr, email, null);
        //     return true;
        // }
    }

    public async Task<bool> Delete(List<long> ids) {
        return await DeleteByIds(ids);
    }

    public async Task EmailApply(string verifyCode, HttpResponse response) {
        var redisKey = RedisConst.EMAIL_VERIFICATION_LINK + verifyCode;

        // Redis 验证
        if (!await redisBasketRepository.Exist(redisKey)) {
            await WebUtil.RenderString(response, "操作失败，请重试");
            return;
        }

        var value = await redisBasketRepository.GetValue(redisKey); // 示例："123 user@example.com"
        if (string.IsNullOrWhiteSpace(value)) {
            await WebUtil.RenderString(response, "操作失败，请重试");
            return;
        }

        var parts = value.Split(' ');
        if (parts.Length != 2 || !long.TryParse(parts[0], out var linkId)) {
            await WebUtil.RenderString(response, "数据格式错误");
            return;
        }

        var email = parts[1];

        // 修改审核状态
        var result = await Db.Updateable<Link>()
                             .SetColumns(l => l.IsCheck == SQLConst.IS_CHECK_YES)
                             .Where(l => l.Id == linkId)
                             .ExecuteCommandAsync();

        if (result <= 0) {
            await WebUtil.RenderString(response, "操作失败，请重试");
            return;
        }

        // 删除 Redis Key
        await redisBasketRepository.Remove(redisKey);

        // todo 邮件通知

        // if (_siteSettings.PassNotice) {
        //     publicService.SendEmail(MailboxAlertsEnum.FRIEND_LINK_APPLICATION_PASS.CodeStr,
        //                             email,
        //                             null);
        //
        //     await WebUtil.RenderString(response, "操作成功，已发送通知邮件");
        //     return;
        // }

        await WebUtil.RenderString(response, "操作成功");
    }
}