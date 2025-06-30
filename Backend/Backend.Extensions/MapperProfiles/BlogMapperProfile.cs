using AutoMapper;
using Backend.Modules.Blog.Contracts.DTO;
using Backend.Modules.Blog.Contracts.VO;
using Backend.Modules.Blog.Domain.Entities;

namespace Backend.Extensions.MapperProfiles;

public class BlogMapperProfile : Profile {
    public BlogMapperProfile() {
        CreateMap<Category, CategoryVO>();
        CreateMap<CategoryDto, Category>();

        CreateMap<LeaveWord, LeaveWordVO>();

        CreateMap<Tag, TagVO>();
        CreateMap<TagDTO, Tag>();

        CreateMap<User, UserListVO>();
        CreateMap<User, UserAccountVO>();
        CreateMap<User, UserDetailsVO>();

        CreateMap<Role, RoleVO>();
        CreateMap<Role, RoleAllVO>();
        CreateMap<Role, RoleByIdVO>();
        CreateMap<RoleDTO, Role>();
        CreateMap<UpdateRoleStatusDTO, Role>();

        CreateMap<Comment, ArticleCommentVO>();

        CreateMap<WebsiteInfo, WebsiteInfoVO>();
        CreateMap<WebsiteInfoDTO, WebsiteInfo>();
    }
}