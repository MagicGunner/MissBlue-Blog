using AutoMapper;
using Backend.Common.Results;
using Backend.Contracts;
using Backend.Domain;
using Backend.Modules.Blog.Contracts.IService;
using Backend.Modules.Blog.Contracts.VO;
using Backend.Modules.Blog.Domain.Entities;
using SqlSugar;

namespace Backend.Modules.Blog.Application.Service;

public class LeaveWordService(IMapper mapper, IBaseRepositories<Category> baseRepositories, IBaseServices<LeaveWord> baseServices) : ILeaveWordService {
    public Task<long> AddLeaveWordAsync(string content) {
        throw new NotImplementedException();
    }
}