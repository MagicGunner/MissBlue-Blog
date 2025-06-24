using System.ComponentModel.DataAnnotations;
using Backend.Common.Results;
using Backend.Modules.Blog.Contracts.DTO;
using Backend.Modules.Blog.Contracts.Interface;
using Backend.Modules.Blog.Contracts.VO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[ApiController]
[Route("tag")]
public class TagController(ITagService tagService) : ControllerBase {
    /// <summary>获取所有标签</summary>
    [HttpGet("list")]
    [AllowAnonymous] // 可根据权限控制调整
    public async Task<ResponseResult<List<TagVO>>> List() {
        return await ControllerUtils.MessageHandler(() => tagService.ListAllTagAsync());
    }

    /// <summary>新增标签（文章列表用）</summary>
    [HttpPut]
    [Authorize(Policy = "Permission:blog:tag:add")]
    public async Task<ResponseResult<Void>> AddTag([FromBody] [Required] TagDTO tagDTO) {
        return await tagService.AddTagAsync(tagDTO);
    }

    /// <summary>后台：获取所有标签</summary>
    [HttpGet("back/list")]
    [Authorize(Policy = "Permission:blog:tag:list")]
    public async Task<ResponseResult<List<TagVO>>> ListArticleTag() {
        return await ControllerUtils.MessageHandler(() => tagService.ListAllTagAsync());
    }

    /// <summary>搜索标签</summary>
    [HttpPost("back/search")]
    [Authorize(Policy = "Permission:blog:tag:search")]
    public async Task<ResponseResult<List<TagVO>>> SearchTag([FromBody] SearchTagDTO searchTagDTO) {
        return await ControllerUtils.MessageHandler(() => tagService.SearchTagAsync(searchTagDTO));
    }

    /// <summary>根据ID获取标签</summary>
    [HttpGet("back/get/{id}")]
    [Authorize(Policy = "Permission:blog:tag:search")]
    public async Task<ResponseResult<TagVO>> GetTagById([FromRoute] long id) {
        return await ControllerUtils.MessageHandler(() => tagService.GetTagByIdAsync(id));
    }

    /// <summary>新增标签（标签列表用）</summary>
    [HttpPut("back/add")]
    [Authorize(Policy = "Permission:blog:tag:add")]
    public async Task<ResponseResult<Void>> AddOrUpdateTag([FromBody] TagDTO tagDTO) {
        tagDTO.Id = null; // 手动置空
        return await tagService.AddOrUpdateTagAsync(tagDTO);
    }

    /// <summary>修改标签</summary>
    [HttpPost("back/update")]
    [Authorize(Policy = "Permission:blog:tag:update")]
    public async Task<ResponseResult<Void>> UpdateTag([FromBody] TagDTO tagDTO) {
        return await tagService.AddOrUpdateTagAsync(tagDTO);
    }

    /// <summary>删除标签</summary>
    [HttpDelete("back/delete")]
    [Authorize(Policy = "Permission:blog:tag:delete")]
    public async Task<ResponseResult<Void>> DeleteTag([FromBody] List<long> ids) {
        return await tagService.DeleteTagByIdsAsync(ids);
    }