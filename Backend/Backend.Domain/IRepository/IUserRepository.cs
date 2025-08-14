using Backend.Domain.Entity;

namespace Backend.Domain.IRepository;

public interface IUserRepository : IBaseRepositories<User> {
    Task<long>                     ValidateUser(string        userName, string password);
    Task<List<Permission>>         GetUserPermissions(string  userName);
    Task<User?>                    GetUserByName(string       userName);
    Task<Dictionary<long, User>>   GetUserDic(List<long>      userIds);
    Task<List<long>>               GetIds(string?             userName);
    Task<Dictionary<long, string>> GetNameDicByIds(List<long> userIds);
    Task<bool>                     UserExists(string?         userName, string  email);
    Task<bool>                     ResetPassWord(string       password, string  email);
    Task<List<User>>               GetOrSearch(string?        userName, string? email, int? isDisable, DateTime? startTime, DateTime? endTime);
    Task<bool>                     UpdateStatus(long          userId,   int     isDisable);
}