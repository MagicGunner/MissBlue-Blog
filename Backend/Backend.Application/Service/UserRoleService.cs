using AutoMapper;
using Backend.Common.Results;
using Backend.Contracts.DTO;
using Backend.Contracts.IService;
using Backend.Domain.Entity;
using Backend.Domain.IRepository;

namespace Backend.Application.Service;

public class UserRoleService(IMapper mapper, IBaseRepositories<UserRole> baseRepositories) : BaseServices<UserRole>(mapper, baseRepositories), IUserRoleService {
    private IMapper _mapper = mapper;

    public async Task<(bool isSuccess, string? msg)> Add(UserRoleDTO userRoleDto) {
        var userIds = userRoleDto.UserIds;
        var roleId = userRoleDto.RoleId;
        // 事务开始
        var transResult = await Db.Ado.UseTranAsync<(bool, string)>(async () => {
                                                                        // 1. 查询已有的用户角色
                                                                        var existUserRoles = await Db.Queryable<UserRole>()
                                                                                                     .Where(userRole => userRole.RoleId == roleId && userIds.Contains(userRole.UserId))
                                                                                                     .ToListAsync();

                                                                        var existUserIds = existUserRoles.Select(ur => ur.UserId).ToList();

                                                                        // 2. 过滤出未分配的用户
                                                                        var notExistUserIds = userIds.Where(id => !existUserIds.Contains(id)).ToList();

                                                                        if (notExistUserIds.Count == 0) {
                                                                            return (true, "全部用户已经拥有该角色，无需再次分配");
                                                                        }

                                                                        // 3. 构建新的UserRole对象
                                                                        var newUserRoles = notExistUserIds.Select(id => new UserRole {
                                                                                                                            UserId = id,
                                                                                                                            RoleId = roleId
                                                                                                                        })
                                                                                                          .ToList();

                                                                        // 4. 批量插入
                                                                        var result = await Db.Insertable(newUserRoles).ExecuteCommandAsync();

                                                                        return result > 0
                                                                                   ? (false, "分配成功")
                                                                                   : (true, "分配失败");
                                                                    });
        return transResult.IsSuccess ? transResult.Data : (false, "数据事务失败:" + transResult.ErrorException.Message);
    }
}