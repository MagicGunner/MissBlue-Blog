using Backend.Contracts.VO;

namespace Backend.Contracts.IService;

public interface ILikeService {
    Task<List<LikeVo>> IsLike(int type, long? typeId);

    Task<bool> SetLiked(int   type, long typeId);
    Task<bool> UnSetLiked(int type, long typeId);
}