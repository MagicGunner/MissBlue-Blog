using Backend.Common.Results;
using Backend.Modules.Blog.Contracts.DTO;
using Backend.Modules.Blog.Contracts.Interface;
using Backend.Modules.Blog.Contracts.VO;
using SqlSugar;

namespace Backend.Modules.Blog.Application.Service;

public class TagService(ISqlSugarClient db) : ITagService {
    public Task<List<TagVO>> ListAllTagAsync() {
        throw new NotImplementedException();
    }

    public Task<ResponseResult<object>> AddTagAsync(TagDTO tagDTO) {
        throw new NotImplementedException();
    }

    public Task<List<TagVO>> SearchTagAsync(SearchTagDTO searchTagDTO) {
        throw new NotImplementedException();
    }

    public Task<TagVO?> GetTagByIdAsync(long id) {
        throw new NotImplementedException();
    }

    public Task<ResponseResult<object>> AddOrUpdateTagAsync(TagDTO tagDTO) {
        throw new NotImplementedException();
    }

    public Task<ResponseResult<object>> DeleteTagByIdsAsync(List<long> ids) {
        throw new NotImplementedException();
    }
}