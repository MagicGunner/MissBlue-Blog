using AutoMapper;
using Backend.Contracts.DTO;
using Backend.Contracts.VO;
using Backend.Domain.Entity;

namespace Backend.Extensions.MapperProfiles;

public class BlogMapperProfile : Profile {
    public BlogMapperProfile() {
        CreateMap<Category, CategoryVO>();
        CreateMap<CategoryDto, Category>();

        CreateMap<UserCommentDTO, Comment>();
        CreateMap<Comment, CommentListVO>();

        CreateMap<LeaveWord, LeaveWordVO>();

        CreateMap<Tag, TagVO>();
        CreateMap<TagDTO, Tag>();

        CreateMap<User, UserListVO>();
        CreateMap<User, UserAccountVO>();
        CreateMap<User, UserDetailsVO>();
        CreateMap<UserUpdateDTO, User>();
        CreateMap<User, RoleUserVO>();

        CreateMap<Role, RoleVO>();
        CreateMap<Role, RoleAllVO>();
        CreateMap<Role, RoleByIdVO>();
        CreateMap<RoleDTO, Role>();
        CreateMap<UpdateRoleStatusDTO, Role>();

        CreateMap<Comment, ArticleCommentVO>();

        CreateMap<WebsiteInfo, WebsiteInfoVO>();
        CreateMap<WebsiteInfoDTO, WebsiteInfo>();

        CreateMap<Permission, PermissionVO>();
        CreateMap<Permission, PermissionMenuVO>();
        CreateMap<PermissionDTO, Permission>()
           .ForMember(dest => dest.MenuId, opt => opt.MapFrom(src => src.PermissionMenuId))
           .ReverseMap()
           .ForMember(dest => dest.PermissionMenuId, opt => opt.MapFrom(src => src.MenuId));

        CreateMap<Article, ArticleDetailVO>();
        CreateMap<Article, ArticleVO>()
           .ForMember(dest => dest.Tags, opt => opt.Ignore()) // 数据库查询
           .ForMember(dest => dest.CategoryName, opt => opt.Ignore());
        CreateMap<Article, RecommendArticleVO>();
        CreateMap<Article, RandomArticleVO>();
        CreateMap<Article, InitSearchTitleVO>();
        CreateMap<Article, TimeLineVO>();
        CreateMap<Article, HotArticleVO>();
        CreateMap<Article, CategoryArticleVO>();
        CreateMap<Article, ArticleListVO>();
        CreateMap<ArticleDto, Article>();
        CreateMap<Article, ArticleDto>();


        CreateMap<Menu, MenuVO>()
           .ForMember(dest => dest.Affix, opt => opt.Ignore())
           .ForMember(dest => dest.HideInMenu, opt => opt.Ignore())
           .ForMember(dest => dest.KeepAlive, opt => opt.Ignore())
           .ForMember(dest => dest.IsDisable, opt => opt.Ignore());
        CreateMap<Menu, MenuByIdVO>();
        CreateMap<MenuDTO, Menu>();

        CreateMap<LeaveWord, LeaveWordListVO>();
        CreateMap<LeaveWord, LeaveWordVO>();

        CreateMap<Like, LikeVo>();

        CreateMap<Favorite, FavoriteListVO>();

        CreateMap<Banners, BannersVO>();
        CreateMap<BannersDTO, Banners>();

        CreateMap<LinkDTO, Link>();
        CreateMap<Link, LinkVO>();

        CreateMap<TreeHole, TreeHoleVO>();
        CreateMap<TreeHole, TreeHoleListVO>();
    }
}