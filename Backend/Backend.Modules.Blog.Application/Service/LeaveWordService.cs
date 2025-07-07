using AutoMapper;
using Backend.Application.Service;
using Backend.Common.Results;
using Backend.Contracts;
using Backend.Contracts.IService;
using Backend.Domain;
using Backend.Domain.Entity;
using Backend.Domain.IRepository;
using Backend.Modules.Blog.Contracts.DTO;
using Backend.Modules.Blog.Contracts.IService;
using Backend.Modules.Blog.Contracts.VO;
using Backend.Modules.Blog.Domain.Entities;
using Backend.Modules.Blog.Domain.Enums;
using Backend.Modules.Blog.Domain.IRepository;
using SqlSugar;

namespace Backend.Modules.Blog.Application.Service;

public class LeaveWordService(IMapper                      mapper,
                              IBaseRepositories<LeaveWord> baseRepositories,
                              ILeaveWordRepository         leaveWordRepository,
                              IUserRepository              userRepository,
                              ICommentRepository           commentRepository,
                              IFavoriteRepository          favoriteRepository,
                              ILikeRepository              likeRepository)
    : BaseServices<LeaveWord>(mapper, baseRepositories), ILeaveWordService {
    private readonly IMapper _mapper = mapper;

    public Task<long> AddLeaveWordAsync(string content) {
        throw new NotImplementedException();
    }

    public async Task<List<LeaveWordListVO>> GetBackList(SearchLeaveWordDTO? searchLeaveWordDto = null) {
        var leaveWords = searchLeaveWordDto == null
                             ? await leaveWordRepository.Query()
                             : await leaveWordRepository.GetBackList(searchLeaveWordDto.UserName, searchLeaveWordDto.isCheck, searchLeaveWordDto.startTime, searchLeaveWordDto.endTime);

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
        var favoriteCountDic = await favoriteRepository.GetCountDic(CommentType.LeaveWord, typeIds);
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
}