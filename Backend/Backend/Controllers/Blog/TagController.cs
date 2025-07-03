using System.ComponentModel.DataAnnotations;
using Backend.Common.Attributes;
using Backend.Common.Results;
using Backend.Modules.Blog.Contracts.DTO;
using Backend.Modules.Blog.Contracts.IService;
using Backend.Modules.Blog.Contracts.VO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Backend.Controllers.Blog;

[ApiController]
[Route("api/tag")]
[SwaggerTag("标签")]
public class TagController(ITagService tagService) : ControllerBase {
    [HttpGet("list")]
    [AllowAnonymous]
    [AccessLimit(60, 60)]
    [SwaggerOperation(Summary = "获取标签列表", Description = "获取标签列表")]
    public async Task<ResponseResult<List<TagVO>>> List() {
        var list = await tagService.ListAllAsync();
        return new ResponseResult<List<TagVO>>(list.Count > 0, list);
    }

    [HttpPut]
    [Authorize(Policy = "blog:tag:add")]
    [AccessLimit(60, 30)]
    [SwaggerOperation(Summary = "新增标签-文章列表", Description = "新增标签-文章列表")]
    public async Task<ResponseResult<object>> AddTag([FromBody] [Required] TagDTO tagDto) => new(await tagService.AddAsync(tagDto) > 0);

    [HttpGet("back/list")]
    [Authorize(Policy = "blog:tag:list")]
    [AccessLimit(60, 30)]
    [SwaggerOperation(Summary = "获取标签列表", Description = "获取标签列表")]
    public async Task<ResponseResult<List<TagVO>>> ListArticleTag() {
        var list = await tagService.ListAllAsync();
        return new ResponseResult<List<TagVO>>(list.Count > 0, list);
    }

    [HttpPost("back/search")]
    [Authorize(Policy = "blog:tag:search")]
    [AccessLimit(60, 30)]
    [SwaggerOperation(Summary = "搜索标签列表", Description = "搜索标签列表")]
    public async Task<ResponseResult<List<TagVO>>> SearchTag([FromBody] SearchTagDTO searchTagDTO) {
        var list = await tagService.SearchTagAsync(searchTagDTO);
        return new ResponseResult<List<TagVO>>(list.Count > 0, list);
    }

    [HttpGet("back/get/{id}")]
    [Authorize(Policy = "blog:tag:search")]
    [AccessLimit(60, 30)]
    [SwaggerOperation(Summary = "根据id查询标签", Description = "根据id查询标签")]
    public async Task<ResponseResult<TagVO?>> GetTagById([FromRoute] long id) {
        var tagVo = await tagService.GetByIdAsync(id);
        return new ResponseResult<TagVO?>(tagVo != null, tagVo);
    }

    [HttpPut("back/add")]
    [Authorize(Policy = "blog:tag:add")]
    [AccessLimit(60, 30)]
    [SwaggerOperation(Summary = "新增标签-标签列表", Description = "新增标签-标签列表")]
    public async Task<ResponseResult<object>> AddOrUpdateTag([FromBody] TagDTO tagDto) {
        tagDto.Id = null; // 手动置空
        return await AddTag(tagDto);
    }

    [HttpPost("back/update")]
    [Authorize(Policy = "blog:tag:update")]
    [AccessLimit(60, 30)]
    [SwaggerOperation(Summary = "修改标签", Description = "修改标签")]
    public async Task<ResponseResult<object>> UpdateTag([FromBody] TagDTO tagDto) => new(await tagService.UpdateAsync(tagDto));

    [HttpDelete("back/delete")]
    [Authorize(Policy = "blog:tag:delete")]
    [AccessLimit(60, 30)]
    [SwaggerOperation(Summary = "删除标签", Description = "删除标签")]
    public async Task<ResponseResult<object>> DeleteTag([FromBody] List<long> ids) => new(await tagService.DeleteByIdsAsync(ids));
}