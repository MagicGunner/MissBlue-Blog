using System.ComponentModel.DataAnnotations;
using Backend.Common.Results;
using Backend.Modules.Blog.Contracts.DTO;
using Backend.Modules.Blog.Contracts.IService;
using Backend.Modules.Blog.Contracts.VO;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers.Blog;

[ApiController]
[Route("api/category")]
[Tags("分类")]
public class CategoryController(ICategoryService categoryService) : ControllerBase {
    /// <summary>新增分类（文章列表用）</summary>
    [HttpPut]
    // [Authorize(Policy = "Permission:blog:tag:add")]
    public async Task<ResponseResult<object>> AddCategory([FromBody] [Required] CategoryDto categoryDto) => new(await categoryService.AddAsync(categoryDto) > 0);

    /// <summary>新增分类（标签列表用）</summary>
    [HttpPut("back/add")]
    // [Authorize(Policy = "Permission:blog:tag:add")]
    public async Task<ResponseResult<object>> AddOrUpdateCategory([FromBody] CategoryDto categoryDto) {
        categoryDto.Id = null; // 手动置空
        return await AddCategory(categoryDto);
    }

    /// <summary>删除标签</summary>
    [HttpDelete("back/delete")]
    // [Authorize(Policy = "Permission:blog:tag:delete")]
    public async Task<ResponseResult<object>> DeleteCategory([FromBody] List<long> ids) => new(await categoryService.DeleteByIdsAsync(ids));

    /// <summary>根据ID获取分类</summary>
    [HttpGet("back/get/{id}")]
    // [Authorize(Policy = "Permission:blog:tag:search")]
    public async Task<ResponseResult<CategoryVO?>> GetCategoryById([FromRoute] long id) {
        var categoryVo = await categoryService.GetByIdAsync(id);
        return new ResponseResult<CategoryVO?>(categoryVo != null, categoryVo);
    }

    /// <summary>搜索分类</summary>
    [HttpPost("back/search")]
    // [Authorize(Policy = "Permission:blog:tag:search")]
    public async Task<ResponseResult<List<CategoryVO>>> SearchCategory([FromBody] SearchCategoryDTO searchCategoryDto) {
        var list = await categoryService.SearchCategoryAsync(searchCategoryDto);
        return new ResponseResult<List<CategoryVO>>(list.Count > 0, list);
    }

    /// <summary>修改分类</summary>
    [HttpPost("back/update")]
    // [Authorize(Policy = "Permission:blog:tag:update")]
    public async Task<ResponseResult<object>> UpdateCategory([FromBody] CategoryDto categoryDto) => new(await categoryService.UpdateAsync(categoryDto));

    [HttpGet("list")]
    public async Task<ResponseResult<List<CategoryVO>>> ListAllAsync() {
        var list = await categoryService.ListAllAsync();
        return new ResponseResult<List<CategoryVO>>(list.Count > 0, list);
    }
}