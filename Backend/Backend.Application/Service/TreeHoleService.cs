using AutoMapper;
using Backend.Application.Interface;
using Backend.Contracts.DTO;
using Backend.Contracts.IService;
using Backend.Contracts.VO;
using Backend.Domain.Entity;
using Backend.Domain.IRepository;

namespace Backend.Application.Service;

public class TreeHoleService(IMapper mapper, IBaseRepositories<TreeHole> baseRepositories, ITreeHoleRepository treeHoleRepository, ICurrentUser currentUser)
    : BaseServices<TreeHole>(mapper, baseRepositories), ITreeHoleService {
    private readonly IMapper _mapper = mapper;

    public async Task<bool> Add(AddTreeHoleDto dto) {
        if (currentUser.UserId == null) {
            return false;
        }

        var id = await treeHoleRepository.Add(new TreeHole {
                                                               UserId = currentUser.UserId.Value,
                                                               Content = dto.Content
                                                           });
        return id > 0;
    }

    public async Task<List<TreeHoleVO>> GetFrontList() {
        var treeHoles = await treeHoleRepository.GetFrontlist();
        var user = await Db.Queryable<User>().Where(u => u.Id == currentUser.UserId).FirstAsync();
        return treeHoles.Select(th => {
                                    var treeHoleVo = _mapper.Map<TreeHoleVO>(th);
                                    treeHoleVo.Avatar = user.Avatar;
                                    treeHoleVo.Nickname = user.Nickname;
                                    return treeHoleVo;
                                })
                        .ToList();
    }

    public async Task<List<TreeHoleListVO>> GetBackList(SearchTreeHoleDTO? dto) {
        var treeHoles = await treeHoleRepository.GetBackList(dto?.UserName, dto?.IsCheck, dto?.StartTime, dto?.EndTime);
        var userDic = await treeHoleRepository.GetEntityDic<User>(treeHoles.Select(th => th.UserId).ToList());
        return treeHoles.Select(th => {
                                    var treeHoleVo = _mapper.Map<TreeHoleListVO>(th);
                                    if (userDic.TryGetValue(th.UserId, out var user)) {
                                        treeHoleVo.UserName = user.Username;
                                    }

                                    return treeHoleVo;
                                })
                        .ToList();
    }

    public async Task<bool> SetCheck(TreeHoleIsCheckDTO dto) {
        return await treeHoleRepository.SetCheck(dto.Id, dto.IsCheck);
    }
}