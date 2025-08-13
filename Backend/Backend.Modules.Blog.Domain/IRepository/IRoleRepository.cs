using Backend.Domain.Entity;
using Backend.Domain.IRepository;

namespace Backend.Modules.Blog.Domain.IRepository;

public interface IRoleRepository : IBaseRepositories<Role> {
    Task<List<Role>> SelectAll();

    Task<List<Role>> Get(string? roleName, string? roleKey, int? status, DateTime? startTime, DateTime? endTime);
}