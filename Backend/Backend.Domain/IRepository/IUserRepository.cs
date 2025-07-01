using Backend.Domain.Entity;

namespace Backend.Domain.IRepository;

public interface IUserRepository : IBaseRepositories<User> {
    Task<bool> ValidateUser(string userName, string password);

    Task<List<Permission>> GetUserPermissions(string userName);

    Task<User?> GetUserByName(string userName);
}