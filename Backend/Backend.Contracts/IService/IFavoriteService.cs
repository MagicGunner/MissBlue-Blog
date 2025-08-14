using Backend.Contracts.DTO;
using Backend.Contracts.VO;

namespace Backend.Contracts.IService;

public interface IFavoriteService {
    Task<bool>                 SetFavorited(int               type, long typeId);
    Task<bool>                 UnSetFavorited(int             type, long typeId);
    Task<bool>                 IsFavorited(int                type, long typeId);
    Task<List<FavoriteListVO>> GetBackList(SearchFavoriteDTO? dto);
    Task<bool>                 SetChecked(FavoriteIsCheckDTO  favoriteIsCheckDto);
    Task<bool>                 Delete(List<long>              ids);
}