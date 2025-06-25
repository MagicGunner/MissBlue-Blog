using AutoMapper;
using Backend.Modules.Blog.Contracts.VO;
using Backend.Modules.Blog.Domain.Entities;

namespace Backend.Extensions.MapperProfiles;

public class LeaveWordProfile : Profile {
    public LeaveWordProfile() {
        CreateMap<LeaveWord, LeaveWordVO>();
    }
}