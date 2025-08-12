using Backend.Common.Record;
using Backend.Domain.Entity;

namespace Backend.Domain.IRepository;

public interface IPermissionRepository : IBaseRepositories<Permission> {
    Task<List<Permission>> GetAll();
    Task<List<Permission>> Get(string?               permissionDesc, string? permissionKey, long? permissionMenuId);
    Task<BoolResult>       UpdateOrInsert(Permission permission);
    Task<BoolResult>       Delete(long               id);
}