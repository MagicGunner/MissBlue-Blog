using Backend.Domain.Entity;

namespace Backend.Modules.Blog.Domain.IRepository;

public interface IRoleRepository {
    Task<List<Role>> SelectAll();
}