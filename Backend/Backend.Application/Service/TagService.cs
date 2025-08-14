using AutoMapper;
using Backend.Contracts.DTO;
using Backend.Contracts.IService;
using Backend.Contracts.VO;
using Backend.Domain.Entity;
using Backend.Domain.IRepository;

namespace Backend.Application.Service;

public class TagService(IMapper mapper, IBaseRepositories<Tag> baseRepositories) : BaseServices<Tag>(mapper, baseRepositories), ITagService {
    private readonly IMapper    _mapper = mapper;
    public async     Task<long> AddAsync(TagDTO tagDTO) => await Add(_mapper.Map<Tag>(tagDTO));

    public async Task<bool> DeleteByIdsAsync(List<long> ids) => await DeleteByIds(ids);

    public async Task<bool> UpdateAsync(TagDTO tagDTO) => await Update(_mapper.Map<Tag>(tagDTO));


    public async Task<List<TagVO>> ListAllAsync() => await Query<TagVO>();


    public async Task<List<TagVO>> SearchTagAsync(SearchTagDTO searchTagDTO) => await Query<TagVO>(tag => tag.TagName == searchTagDTO.TagName);

    public async Task<TagVO?> GetByIdAsync(long id) => (await Query<TagVO>(tag => tag.Id == id)).FirstOrDefault();
}