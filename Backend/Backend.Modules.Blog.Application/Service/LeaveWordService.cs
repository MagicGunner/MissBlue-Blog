using AutoMapper;
using Backend.Application.Interface;
using Backend.Application.Service;
using Backend.Common.Const;
using Backend.Common.Results;
using Backend.Domain.Entity;
using Backend.Domain.IRepository;
using Backend.Modules.Blog.Contracts.DTO;
using Backend.Modules.Blog.Contracts.IService;
using Backend.Modules.Blog.Contracts.VO;
using Backend.Modules.Blog.Domain.Entities;
using Backend.Modules.Blog.Domain.Enums;
using Backend.Modules.Blog.Domain.IRepository;
using Newtonsoft.Json;


namespace Backend.Modules.Blog.Application.Service;

public class LeaveWordService(IMapper                      mapper,
                              IBaseRepositories<LeaveWord> baseRepositories,
                              ILeaveWordRepository         leaveWordRepository,
                              IUserRepository              userRepository,
                              ICommentRepository           commentRepository,
                              IFavoriteRepository          favoriteRepository,
                              ILikeRepository              likeRepository,
                              ICurrentUser                 currentUser
)
    : BaseServices<LeaveWord>(mapper, baseRepositories), ILeaveWordService {
    private readonly IMapper _mapper = mapper;

    public async Task<(bool isSuccess, string? msg)> AddLeaveWord(string content) {
        // 1. 解析 JSON 字符串
        // 2. 校验长度
        if (string.IsNullOrWhiteSpace(content) || content.Length > FunctionConst.LEAVE_WORD_CONTENT_LENGTH) {
            return (false, "留言内容为空或过长");
        }

        // 5. 查询当前用户信息
        var userId = currentUser.UserId;
        if (userId == null) {
            return (false, "请先登录后再留言");
        }

        // 3. 构建留言实体
        var leaveWord = new LeaveWord {
                                          Content = content,
                                          UserId = userId.Value,
                                          CreateTime = DateTime.Now // 假设你有这个字段
                                      };

        // 4. 保存留言
        var result = await leaveWordRepository.Add(leaveWord);
        return result <= 0 ? (false, "留言保存失败") : (true, "操作成功");


        // // 6. 判断是否为站长本人留言 或禁用提醒
        // if (user.Email == _siteSettings.AdminEmail || !_siteSettings.MessageNewNotice)
        //     return ResponseResult.Success();
        //
        // // 7. 发送邮箱提醒
        // var mailData = new Dictionary<string, object> {
        //                                                   ["messageId"] = leaveWord.Id
        //                                               };
        //
        // await _publicService.SendEmailAsync(MailboxAlertsEnum.MESSAGE_NOTIFICATION_EMAIL.CodeStr,
        //                                     _siteSettings.AdminEmail,
        //                                     mailData);
        //
        // return ResponseResult.Success();
    }

    public async Task<List<LeaveWordListVO>> GetBackList(SearchLeaveWordDTO? searchLeaveWordDto = null) {
        var leaveWords = searchLeaveWordDto == null
                             ? await leaveWordRepository.Query()
                             : await leaveWordRepository.GetBackList(searchLeaveWordDto.UserName, searchLeaveWordDto.IsCheck, searchLeaveWordDto.StartTime, searchLeaveWordDto.EndTime);

        var userDic = await userRepository.GetUserDic(leaveWords.Select(lw => lw.UserId).ToList());
        return leaveWords.Select(lw => {
                                     var vo = _mapper.Map<LeaveWordListVO>(lw);
                                     if (userDic.TryGetValue(lw.UserId, out var user)) {
                                         vo.UserName = user.Username;
                                     }

                                     return vo;
                                 })
                         .ToList();
    }

    public async Task<List<LeaveWordVO>> GetList(string? id) {
        var leaveWords = await leaveWordRepository.GetList(id);
        var userDic = await userRepository.GetUserDic(leaveWords.Select(lw => lw.UserId).ToList());
        var typeIds = leaveWords.Select(lw => lw.Id).ToList();
        var commentCountDic = await commentRepository.GetCountDic(CommentType.LeaveWord, typeIds);
        var favoriteCountDic = await favoriteRepository.GetCountDic(FavoriteType.LeaveWord, typeIds);
        var likeCountDic = await likeRepository.GetCountDic(LikeType.LeaveWord, typeIds);
        return leaveWords.Select(lw => {
                                     var vo = _mapper.Map<LeaveWordVO>(lw);
                                     if (userDic.TryGetValue(lw.UserId, out var user)) {
                                         vo.Nickname = user.Nickname;
                                         vo.Avatar = user.Avatar;
                                         if (commentCountDic.TryGetValue(lw.Id, out var commentCount)) vo.CommentCount = commentCount;
                                         if (likeCountDic.TryGetValue(lw.Id, out var favoriteCount)) vo.FavoriteCount = favoriteCount;
                                         if (likeCountDic.TryGetValue(lw.Id, out var likeCount)) vo.LikeCount = likeCount;
                                     }

                                     return vo;
                                 })
                         .ToList();
    }

    public async Task<bool> SetIsCheck(LeaveWordIsCheckDTO leaveWordIsCheckDto) => await leaveWordRepository.SetIsChecked(leaveWordIsCheckDto.Id, leaveWordIsCheckDto.IsCheck);

    public async Task<bool> Delete(List<long> leaveWordIds) {
        if (!await DeleteByIds(leaveWordIds)) return false;
        // 删除该留言下的点赞，收藏和评论
        await likeRepository.Delete(l => l.Type == (int)LikeType.LeaveWord && leaveWordIds.Contains(l.TypeId));
        await favoriteRepository.Delete(l => l.Type == (int)FavoriteType.LeaveWord && leaveWordIds.Contains(l.TypeId));
        await commentRepository.Delete(l => l.Type == (int)CommentType.LeaveWord && leaveWordIds.Contains(l.TypeId));
        return false;
    }
}