using AutoMapper;
using Backend.Application.Interface;
using Backend.Application.Service;
using Backend.Domain.IRepository;
using Backend.Modules.Blog.Contracts.IService;
using Backend.Modules.Blog.Contracts.VO;
using Backend.Modules.Blog.Domain.Entities;
using Backend.Modules.Blog.Domain.IRepository;

namespace Backend.Modules.Blog.Application.Service;

public class LikeService(IMapper mapper, IBaseRepositories<Like> baseRepositories, ILikeRepository likeRepository, ICurrentUser currentUser)
    : BaseServices<Like>(mapper, baseRepositories), ILikeService {
    private readonly IMapper _mapper = mapper;

    public async Task<List<LikeVo>> IsLike(int type, long? typeId) {
        if (currentUser.UserId == null) {
            return [];
        }

        var userId = currentUser.UserId.Value;
        return (await likeRepository.IsLike(userId, type, typeId)).Select(like => _mapper.Map<LikeVo>(like)).ToList();
    }
}