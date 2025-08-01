﻿using Backend.Domain.Entity;

namespace Backend.Domain.IRepository;

public interface IUserRepository : IBaseRepositories<User> {
    Task<long>                     ValidateUser(string        userName, string password);
    Task<List<Permission>>         GetUserPermissions(string  userName);
    Task<User?>                    GetUserByName(string       userName);
    Task<Dictionary<long, User>>   GetUserDic(List<long>      userIds);
    Task<List<long>>               GetIds(string?             userName);
    Task<Dictionary<long, string>> GetNameDicByIds(List<long> userIds);
}