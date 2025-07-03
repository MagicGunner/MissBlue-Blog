using AutoMapper;
using Backend.Application.Service;
using Backend.Common.Results;
using Backend.Contracts;
using Backend.Contracts.IService;
using Backend.Domain;
using Backend.Domain.IRepository;
using Backend.Modules.Blog.Contracts.IService;
using Backend.Modules.Blog.Contracts.VO;
using Backend.Modules.Blog.Domain.Entities;
using SqlSugar;

namespace Backend.Modules.Blog.Application.Service;

public class LeaveWordService(IMapper mapper, IBaseRepositories<LeaveWord> baseRepositories)
    : BaseServices<LeaveWord>(mapper, baseRepositories), ILeaveWordService {
    public Task<long> AddLeaveWordAsync(string content) {
        throw new NotImplementedException();
    }
}