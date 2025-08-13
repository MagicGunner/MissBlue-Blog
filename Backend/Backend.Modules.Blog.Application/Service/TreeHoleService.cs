using AutoMapper;
using Backend.Application.Interface;
using Backend.Application.Service;
using Backend.Domain.Entity;
using Backend.Domain.IRepository;
using Backend.Modules.Blog.Contracts.DTO;
using Backend.Modules.Blog.Contracts.IService;
using Backend.Modules.Blog.Contracts.VO;
using Backend.Modules.Blog.Domain.Entities;
using Backend.Modules.Blog.Domain.IRepository;

namespace Backend.Modules.Blog.Application.Service;

public class TreeHoleService(IMapper mapper, IBaseRepositories<TreeHole> baseRepositories, ITreeHoleRepository treeHoleRepository, ICurrentUser currentUser)
    : BaseServices<TreeHole>(mapper, baseRepositories), ITreeHoleService {
    public async Task<bool> Add(string content) {
        if (currentUser.UserId == null) {
            return false;
        }

        var id = await treeHoleRepository.Add(new TreeHole {
                                                               UserId = currentUser.UserId.Value,
                                                               Content = content
                                                           });
        return id > 0;
    }

    public async Task<List<TreeHoleVO>> GetFrontList() {
        var treeHoles = await treeHoleRepository.GetFrontlist();
        var user = await Db.Queryable<User>().Where(u => u.Id == currentUser.UserId).FirstAsync();
        return treeHoles.Select(th => {
                                    var treeHoleVo = mapper.Map<TreeHoleVO>(th);
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
                                    var treeHoleVo = mapper.Map<TreeHoleListVO>(th);
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