using System.Linq.Expressions;
using AutoMapper;
using Backend.Application;
using Backend.Common.Results;
using Backend.Contracts;
using Backend.Domain;
using Backend.Modules.Blog.Contracts.DTO;
using Backend.Modules.Blog.Contracts.IService;
using Backend.Modules.Blog.Contracts.VO;
using Backend.Modules.Blog.Domain.Entities;
using SqlSugar;

namespace Backend.Modules.Blog.Application.Service;

public class TagService(IMapper mapper, IBaseRepositories<Tag> baseRepositories, IBaseServices<Tag, TagVO> baseServices) : ITagService {
    public ISqlSugarClient Db => baseRepositories.Db;

    public async Task<ResponseResult<object>> AddAsync(TagDTO tagDTO) {
        var tag = mapper.Map<Tag>(tagDTO);
        var tagId = await baseServices.AddAsync(tag);
        return tagId > 0 ? ResponseResult<object>.Success(tagId) : ResponseResult<object>.Failure(msg: "添加失败");
    }

    public async Task<ResponseResult<object>> DeleteByIdsAsync(List<long> ids) => await baseServices.DeleteByIdsAsync(ids);

    public async Task<ResponseResult<object>> UpdateAsync(TagDTO tagDTO) => await baseServices.UpdateAsync(mapper.Map<Tag>(tagDTO));


    public async Task<List<TagVO>> ListAllAsync() => await baseServices.QueryAsync();


    public async Task<List<TagVO>> SearchTagAsync(SearchTagDTO searchTagDTO) => await baseServices.QueryAsync(tag => tag.TagName == searchTagDTO.TagName);

    public async Task<TagVO?> GetByIdAsync(long id) => (await baseServices.QueryAsync(tag => tag.Id == id)).FirstOrDefault();
}