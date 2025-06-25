using AutoMapper;
using Backend.Modules.Blog.Contracts.DTO;
using Backend.Modules.Blog.Contracts.VO;
using Backend.Modules.Blog.Domain.Entities;

namespace Backend.Extensions.MapperProfiles;

public class TagProfile : Profile {
    public TagProfile() {
        CreateMap<Tag, TagVO>();
        CreateMap<TagDTO, Tag>();
    }
}