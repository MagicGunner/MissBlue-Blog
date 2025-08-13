using Backend.Common.Record;
using Backend.Domain.Entity;
using Backend.Infrastructure.Repository;
using Backend.Infrastructure.UnitOfWorks;
using Backend.Modules.Blog.Domain.IRepository;

namespace Backend.Modules.Blog.Infrastructure.Repository;

public class RoleRepository(IUnitOfWorkManage unitOfWorkManage) : BaseRepositories<Role>(unitOfWorkManage), IRoleRepository {
    public async Task<List<Role>> SelectAll() {
        return await Db.Queryable<Role>().Where(r => r.Status == 0).ToListAsync();
    }

    public async Task<List<Role>> Get(string? roleName, string? roleKey, int? status, DateTime? startTime, DateTime? endTime) {
        var query = Db.Queryable<Role>();
        if (!string.IsNullOrEmpty(roleName)) query = query.Where(r => r.RoleName == roleName);
        if (!string.IsNullOrEmpty(roleKey)) query = query.Where(r => r.RoleKey == roleKey);
        if (status.HasValue) query = query.Where(r => r.Status == status.Value);
        if (startTime.HasValue) query = query.Where(r => r.CreateTime >= startTime.Value);
        if (endTime.HasValue) query = query.Where(r => r.CreateTime <= endTime.Value);
        return await query.ToListAsync();
    }

    public async Task<bool> UpdateStatus(long id, int status) {
        var affectedRows = await Db.Updateable<Role>()
                                   .SetColumns(r => r.Status == status)
                                   .Where(r => r.Id == id)
                                   .ExecuteCommandAsync();
        return affectedRows > 0;
    }

    public async Task<BoolResult> UpdateOrInsert(Role role, List<long>? menuIds) {
        // 唯一性校验
        var exist = await Db.Queryable<Role>().Where(r => r.RoleKey == role.RoleKey).FirstAsync();
        if (exist != null && exist.Id != role.Id) {
            return new BoolResult(false, "角色标识已存在");
        }

        var trans = await Db.Ado.UseTranAsync(async Task<BoolResult> () => {
                                                  if (role.Id > 0) { // 更新
                                                      role.UpdateTime = DateTime.Now;
                                                      var affectedRows = await Db.Updateable(role)
                                                                                 .WhereColumns(r => r.Id)
                                                                                 .ExecuteCommandAsync();
                                                      if (affectedRows <= 0) return new BoolResult(false, "更新失败");
                                                  } else { // 插入
                                                      role.CreateTime = DateTime.Now;
                                                      role.UpdateTime = DateTime.Now;
                                                      var insertedId = await Add(role);
                                                      if (insertedId <= 0) return new BoolResult(false, "插入失败");
                                                  }

                                                  // 维护 Role-Menu 关联：先删后插
                                                  await Db.Deleteable<RoleMenu>().Where(rm => rm.RoleId == role.Id).ExecuteCommandAsync();
                                                  if (menuIds is { Count: > 0 }) {
                                                      var roleMenus = menuIds.Select(menuId => new RoleMenu {
                                                                                                                RoleId = role.Id,
                                                                                                                MenuId = menuId
                                                                                                            })
                                                                             .ToList();
                                                      var insertedCount = await Db.Insertable(roleMenus).ExecuteCommandAsync();
                                                      if (insertedCount <= 0) return new BoolResult(false, "角色菜单关联插入失败");
                                                  }

                                                  return new BoolResult(true, "操作成功");
                                              });
        return trans.IsSuccess ? trans.Data : new BoolResult(false, $"事务执行失败：{trans.ErrorMessage}");
    }

    public override async Task<bool> DeleteByIds(List<long> ids) {
        var trans = await Db.Ado.UseTranAsync(async () => {
                                                  ;
                                                  // 先删除 Role-Menu 关联
                                                  var deletedCount = await Db.Deleteable<RoleMenu>().In(rm => rm.RoleId, ids).ExecuteCommandAsync();
                                                  if (deletedCount < 0) throw new Exception("删除角色菜单关联失败");

                                                  // 再删除角色
                                                  var affectedRows = await Db.Deleteable<Role>().In(r => r.Id, ids).ExecuteCommandAsync();
                                                  if (affectedRows <= 0) throw new Exception("删除角色失败");
                                              });
        return trans.IsSuccess;
    }
}