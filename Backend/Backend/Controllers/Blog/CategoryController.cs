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
[Route("api/category")]
[SwaggerTag("分类相关接口")]
public class CategoryController(ICategoryService categoryService) : ControllerBase {
    [HttpGet("list")]
    [AccessLimit(60, 60)]
    [SwaggerOperation(Summary = "获取所有分类", Description = "获取所有分类")]
    public async Task<ResponseResult<List<CategoryVO>>> ListAll() {
        var list = await categoryService.ListAll();
        return new ResponseResult<List<CategoryVO>>(list.Count > 0, list);
    }

    [HttpPut]
    [Authorize(Policy = "blog:category:add")]
    [AccessLimit(60, 30)]
    [SwaggerOperation(Summary = "新增分类-文章列表", Description = "新增分类-文章列表")]
    public async Task<ResponseResult<object>> AddCategory([FromBody] [Required] CategoryDto categoryDto) => new(await categoryService.Add(categoryDto) > 0);


    [HttpGet("back/list")]
    [Authorize(Policy = "blog:category:list")]
    [AccessLimit(60, 30)]
    [SwaggerOperation(Summary = "获取分类列表", Description = "获取分类列表")]
    public Task<ResponseResult<List<CategoryVO>>> ListArticle() {
        throw new NotImplementedException();
    }

    [HttpPost("back/search")]
    [Authorize(Policy = "blog:category:search")]
    [AccessLimit(60, 30)]
    [SwaggerOperation(Summary = "搜索分类列表", Description = "搜索分类列表")]
    public async Task<ResponseResult<List<CategoryVO>>> SearchCategory([FromBody] SearchCategoryDTO searchCategoryDto) {
        var list = await categoryService.SearchCategory(searchCategoryDto);
        return new ResponseResult<List<CategoryVO>>(list.Count > 0, list);
    }

    [HttpGet("back/get/{id}")]
    [Authorize(Policy = "blog:category:search")]
    [AccessLimit(60, 30)]
    [SwaggerOperation(Summary = "根据id查询分类", Description = "根据id查询分类")]
    public async Task<ResponseResult<CategoryVO?>> GetCategoryById([FromRoute] long id) {
        var categoryVo = await categoryService.GetById(id);
        return new ResponseResult<CategoryVO?>(categoryVo != null, categoryVo);
    }

    [HttpPut("back/add")]
    [Authorize(Policy = "blog:category:add")]
    [AccessLimit(60, 30)]
    [SwaggerOperation(Summary = "新增分类-分类列表", Description = "新增分类-分类列表")]
    public async Task<ResponseResult<object>> AddOrUpdateCategory([FromBody] CategoryDto categoryDto) {
        categoryDto.Id = null; // 手动置空
        return await AddCategory(categoryDto);
    }

    [HttpPost("back/update")]
    [Authorize(Policy = "blog:category:update")]
    [AccessLimit(60, 30)]
    [SwaggerOperation(Summary = "修改分类", Description = "修改分类")]
    public async Task<ResponseResult<object>> UpdateCategory([FromBody] CategoryDto categoryDto) => new(await categoryService.Update(categoryDto));

    [HttpDelete("back/delete")]
    [Authorize(Policy = "blog:category:delete")]
    [AccessLimit(60, 30)]
    [SwaggerOperation(Summary = "删除分类", Description = "删除分类")]
    public async Task<ResponseResult<object>> DeleteCategory([FromBody] List<long> ids) => new(await categoryService.DeleteByIds(ids));
}