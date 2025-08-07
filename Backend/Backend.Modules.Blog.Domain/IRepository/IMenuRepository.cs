using Backend.Modules.Blog.Domain.Entities;

namespace Backend.Modules.Blog.Domain.IRepository;

public interface IMenuRepository {
    Task<List<Menu>> GetMenuList(long? userId, int typeId, string? userName, int? status);
    Task<Menu?>      GetById(long      id);
}