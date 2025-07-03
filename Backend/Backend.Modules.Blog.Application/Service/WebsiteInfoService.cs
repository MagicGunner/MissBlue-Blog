using AutoMapper;
using Backend.Application.Service;
using Backend.Common.Utils;
using Backend.Contracts;
using Backend.Contracts.IService;
using Backend.Domain;
using Backend.Domain.Entity;
using Backend.Domain.IRepository;
using Backend.Modules.Blog.Contracts.IService;
using Backend.Modules.Blog.Contracts.VO;
using Backend.Modules.Blog.Domain.Entities;
using SqlSugar;

namespace Backend.Modules.Blog.Application.Service;

public class WebsiteInfoService(IMapper mapper, IBaseRepositories<WebsiteInfo> baseRepositories)
    : BaseServices<WebsiteInfo>(mapper, baseRepositories), IWebsiteInfoService {
    private readonly IBaseRepositories<WebsiteInfo> _baseRepositories = baseRepositories;
    private new      ISqlSugarClient                Db => _baseRepositories.Db;

    public async Task<WebsiteInfoVO> GetWebsiteInfo() {
        var websiteInfoVo = (await Query<WebsiteInfoVO>()).First();
        // 查询文章数
        websiteInfoVo.ArticleCount = await Db.Queryable<Article>().CountAsync();
        // 查询评论数
        websiteInfoVo.CommentCount = await Db.Queryable<Comment>().CountAsync();
        // 查询分类数
        websiteInfoVo.CategoryCount = await Db.Queryable<Category>().CountAsync();
        // 查询文章访问量
        websiteInfoVo.VisitCount = await Db.Queryable<Article>().SumAsync(it => it.VisitCount);
        // 查询文章最新更新时间
        websiteInfoVo.LastUpdateTime = await Db.Queryable<Article>().MaxAsync(it => it.UpdateTime);
        // 查询文章总字数
        var allContent = await Db.Queryable<Article>().Select(it => it.ArticleContent).ToListAsync();
        var merged = string.Join("", allContent);
        websiteInfoVo.WordCount = MarkdownUtil.ExtractTextFromMarkdown(merged).Length;

        return websiteInfoVo;
    }
}