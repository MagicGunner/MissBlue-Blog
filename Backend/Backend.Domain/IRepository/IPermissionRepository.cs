using Backend.Domain.Entity;

namespace Backend.Domain.IRepository;

public interface IPermissionRepository {
    Task<List<Permission>> GetAllPermissions();
}