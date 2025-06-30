using System.ComponentModel.DataAnnotations;
using Backend.Common.Results;
using Backend.Modules.Blog.Contracts.DTO;
using Backend.Modules.Blog.Contracts.IService;
using Backend.Modules.Blog.Contracts.VO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers.Blog;

[ApiController]
[Route("api/tag")]
[Tags("标签")]
public class TagController(ITagService tagService) : ControllerBase {
    /// <summary>新增标签（文章列表用）</summary>
    [HttpPut]
    // [Authorize(Policy = "Permission:blog:tag:add")]
    public async Task<ResponseResult<object>> AddTag([FromBody] [Required] TagDTO tagDto) => new(await tagService.AddAsync(tagDto) > 0);

    /// <summary>新增标签（标签列表用）</summary>
    [HttpPut("back/add")]
    // [Authorize(Policy = "Permission:blog:tag:add")]
    public async Task<ResponseResult<object>> AddOrUpdateTag([FromBody] TagDTO tagDto) {
        tagDto.Id = null; // 手动置空
        return await AddTag(tagDto);
    }

    /// <summary>删除标签</summary>
    [HttpDelete("back/delete")]
    // [Authorize(Policy = "Permission:blog:tag:delete")]
    public async Task<ResponseResult<object>> DeleteTag([FromBody] List<long> ids) => new(await tagService.DeleteByIdsAsync(ids));

    /// <summary>修改标签</summary>
    [HttpPost("back/update")]
    // [Authorize(Policy = "Permission:blog:tag:update")]
    public async Task<ResponseResult<object>> UpdateTag([FromBody] TagDTO tagDto) => new(await tagService.UpdateAsync(tagDto));

    /// <summary>根据ID获取标签</summary>
    [HttpGet("back/get/{id}")]
    // [Authorize(Policy = "Permission:blog:tag:search")]
    public async Task<ResponseResult<TagVO?>> GetTagById([FromRoute] long id) {
        var tagVo = await tagService.GetByIdAsync(id);
        return new ResponseResult<TagVO?>(tagVo != null, tagVo);
    }

    /// <summary>获取所有标签</summary>
    [HttpGet("list")]
    [AllowAnonymous] // 可根据权限控制调整
    public async Task<ResponseResult<List<TagVO>>> List() {
        var list = await tagService.ListAllAsync();
        return new ResponseResult<List<TagVO>>(list.Count > 0, list);
    }

    /// <summary>搜索标签</summary>
    [HttpPost("back/search")]
    // [Authorize(Policy = "Permission:blog:tag:search")]
    public async Task<ResponseResult<List<TagVO>>> SearchTag([FromBody] SearchTagDTO searchTagDTO) {
        var list = await tagService.SearchTagAsync(searchTagDTO);
        return new ResponseResult<List<TagVO>>(list.Count > 0, list);
    }


    /// <summary>后台：获取所有标签</summary>
    [HttpGet("back/list")]
    // [Authorize(Policy = "Permission:blog:tag:list")]
    public async Task<ResponseResult<List<TagVO>>> ListArticleTag() {
        var list = await tagService.ListAllAsync();
        return new ResponseResult<List<TagVO>>(list.Count > 0, list);
    }
}