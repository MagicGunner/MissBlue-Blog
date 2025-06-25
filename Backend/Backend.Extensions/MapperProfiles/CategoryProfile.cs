using AutoMapper;
using Backend.Modules.Blog.Contracts.DTO;
using Backend.Modules.Blog.Contracts.VO;
using Backend.Modules.Blog.Domain.Entities;

namespace Backend.Extensions.MapperProfiles;

public class CategoryProfile : Profile {
    public CategoryProfile() {
        CreateMap<Category, CategoryVO>();
        CreateMap<CategoryDto, Category>();
    }
}