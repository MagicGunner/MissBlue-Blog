using AutoMapper;
using Backend.Application.Interface;
using Backend.Contracts.DTO;
using Backend.Contracts.IService;
using Backend.Contracts.VO;
using Backend.Domain.Entity;
using Backend.Domain.Enums;
using Backend.Domain.IRepository;

namespace Backend.Application.Service;

public class FavoriteService(
    IMapper                     mapper,
    IBaseRepositories<Favorite> baseRepositories,
    IFavoriteRepository         favoriteRepository,
    IUserRepository             userRepository,
    IArticleRepository          articleRepository,
    ILeaveWordRepository        leaveWordRepository,
    ICurrentUser                currentUser
)
    : BaseServices<Favorite>(mapper, baseRepositories), IFavoriteService {
    private readonly IMapper _mapper = mapper;
    private          long?   UserId => currentUser.UserId;

    public async Task<bool> SetFavorited(int type, long typeId) {
        if (UserId == null) return false;
        return await favoriteRepository.SetFavorited(UserId.Value, type, typeId);
    }

    public async Task<bool> UnSetFavorited(int type, long typeId) {
        if (UserId == null) return false;
        return await favoriteRepository.UnSetFavorited(UserId.Value, type, typeId);
    }

    public async Task<bool> IsFavorited(int type, long typeId) {
        if (UserId == null) return false;
        return await favoriteRepository.IsFavorited(UserId.Value, type, typeId);
    }

    public async Task<List<FavoriteListVO>> GetBackList(SearchFavoriteDTO? dto) {
        var userIds = await userRepository.GetIds(dto?.UserName);
        var favorites = dto == null
                            ? await baseRepositories.Query()
                            : await favoriteRepository.GetBackList(userIds, dto.Type, dto.IsCheck, dto.StartTime, dto.EndTime);
        var userDic = await userRepository.GetUserDic(userIds);
        var articleContentDic = await articleRepository.GetContentDic(userIds);
        var leaveWordContentDic = await leaveWordRepository.GetContentDic(userIds);
        return favorites.Select(favorite => {
                                    var vo = _mapper.Map<FavoriteListVO>(favorite);
                                    if (userDic.TryGetValue(favorite.UserId, out var user)) vo.UserName = user.Username;
                                    vo.Content = favorite.Type switch {
                                                     (int)FavoriteType.Article when articleContentDic.TryGetValue(favorite.TypeId, out var articleContent)       => articleContent,
                                                     (int)FavoriteType.LeaveWord when leaveWordContentDic.TryGetValue(favorite.TypeId, out var leaveWordContent) => leaveWordContent,
                                                     _                                                                                                           => vo.Content
                                                 };
                                    return vo;
                                })
                        .ToList();
    }

    public async Task<bool> SetChecked(FavoriteIsCheckDTO favoriteIsCheckDto) => await favoriteRepository.SetChecked(favoriteIsCheckDto.Id, favoriteIsCheckDto.IsCheck);

    public async Task<bool> Delete(List<long> ids) => await favoriteRepository.Delete(ids);
}