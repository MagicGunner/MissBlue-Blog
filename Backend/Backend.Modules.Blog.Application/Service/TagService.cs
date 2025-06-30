using System.Linq.Expressions;
using AutoMapper;
using Backend.Application;
using Backend.Common.Results;
using Backend.Contracts;
using Backend.Contracts.IService;
using Backend.Domain;
using Backend.Modules.Blog.Contracts.DTO;
using Backend.Modules.Blog.Contracts.IService;
using Backend.Modules.Blog.Contracts.VO;
using Backend.Modules.Blog.Domain.Entities;
using SqlSugar;

namespace Backend.Modules.Blog.Application.Service;

public class TagService(IMapper mapper, IBaseRepositories<Tag> baseRepositories, IBaseServices<Tag> baseServices) : ITagService {
    public async Task<long> AddAsync(TagDTO tagDTO) => await baseServices.Add(mapper.Map<Tag>(tagDTO));

    public async Task<bool> DeleteByIdsAsync(List<long> ids) => await baseServices.DeleteByIds(ids);

    public async Task<bool> UpdateAsync(TagDTO tagDTO) => await baseServices.Update(mapper.Map<Tag>(tagDTO));


    public async Task<List<TagVO>> ListAllAsync() => await baseServices.Query<TagVO>();


    public async Task<List<TagVO>> SearchTagAsync(SearchTagDTO searchTagDTO) => await baseServices.Query<TagVO>(tag => tag.TagName == searchTagDTO.TagName);

    public async Task<TagVO?> GetByIdAsync(long id) => (await baseServices.Query<TagVO>(tag => tag.Id == id)).FirstOrDefault();
}