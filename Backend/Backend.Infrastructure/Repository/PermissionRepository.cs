using Backend.Common.Record;
using Backend.Domain.Entity;
using Backend.Domain.IRepository;
using Backend.Infrastructure.UnitOfWorks;

namespace Backend.Infrastructure.Repository;

public class PermissionRepository(IUnitOfWorkManage unitOfWorkManage) : BaseRepositories<Permission>(unitOfWorkManage), IPermissionRepository {
    public async Task<List<Permission>> GetAll() {
        return await Query();
    }

    public async Task<List<Permission>> Get(string? permissionDesc, string? permissionKey, long? permissionMenuId) {
        // 1) 条件查询 Permission
        return await Db.Queryable<Permission>()
                       .WhereIF(!string.IsNullOrWhiteSpace(permissionDesc), p => p.PermissionDesc.Contains(permissionDesc!))
                       .WhereIF(!string.IsNullOrWhiteSpace(permissionKey), p => p.PermissionKey.Contains(permissionKey!))
                       .WhereIF(permissionMenuId.HasValue, p => p.MenuId == permissionMenuId!.Value)
                       .ToListAsync();
    }

    public async Task<BoolResult> UpdateOrInsert(Permission permission) {
        var tran = await Db.Ado.UseTranAsync(async Task<BoolResult> () => {
                                                 // 1) 唯一性校验（按 PermissionKey 去重，忽略首尾空格）
                                                 var key = permission.PermissionKey.Trim();
                                                 var exist = await Db.Queryable<Permission>()
                                                                     .Where(p => p.PermissionKey == key)
                                                                     .FirstAsync();

                                                 if (exist != null && exist.Id != permission.Id) {
                                                     // 和 Java 一样：直接返回失败（在事务体里返回结果，不抛异常也可）
                                                     return new BoolResult(false, "权限字符不可重复");
                                                 }

                                                 // 3) 保存或更新（等价 saveOrUpdate）
                                                 int rows;
                                                 if (permission.Id > 0) {
                                                     // 如需让 null 也写回数据库，可加 IgnoreColumns(false)
                                                     rows = await Db.Updateable(permission)
                                                                    .WhereColumns(p => p.Id)
                                                                    .ExecuteCommandAsync();
                                                 } else {
                                                     rows = await Db.Insertable(permission).ExecuteCommandAsync();
                                                 }

                                                 return rows > 0 ? new BoolResult(true, null) : new BoolResult(false, "保存失败");
                                             });
        return tran.Data;
    }

    public async Task<BoolResult> Delete(long id) {
        return (await Db.Ado.UseTranAsync(async Task<BoolResult> () => {
                                              //  删除权限
                                              var rows = await Db.Deleteable<Permission>()
                                                                 .Where(p => p.Id == id)
                                                                 .ExecuteCommandAsync();
                                              if (rows <= 0) return new BoolResult(false, "删除失败");
                                              //  删除相关的权限菜单（如果有的话）
                                              await Db.Deleteable<RolePermission>().Where(rp => rp.PermissionId == id).ExecuteCommandAsync();
                                              return new BoolResult(true, null);
                                          })).Data;
    }
}