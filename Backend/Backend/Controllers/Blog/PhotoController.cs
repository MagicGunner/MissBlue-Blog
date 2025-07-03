using System.ComponentModel.DataAnnotations;
using Backend.Common.Attributes;
using Backend.Common.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Backend.Controllers.Blog;

[ApiController]
[Route("api/photo")]
[SwaggerTag("相册相关接口")]
public class PhotoController : ControllerBase {
    [HttpGet("back/list")]
    [Authorize(Policy = "blog:photo:list")]
    [AccessLimit(60, 30)]
    [SwaggerOperation(Summary = "后台相册或照片列表", Description = "后台相册或照片列表")]
    public Task<ResponseResult<object>> BackList([
                                                     FromQuery]
                                                 long pageNum = 1,
                                                 [FromQuery] long  pageSize = 10,
                                                 [FromQuery] long? parentId = null) {
        throw new NotImplementedException();
    }

    [HttpGet("list")]
    [AccessLimit(60, 30)]
    [SwaggerOperation(Summary = "前台相册或照片列表", Description = "前台相册或照片列表")]
    public Task<ResponseResult<object>> GetList([
                                                    FromQuery]
                                                long pageNum = 1,
                                                [FromQuery] long  pageSize = 16,
                                                [FromQuery] long? parentId = null) {
        throw new NotImplementedException();
    }

    [HttpPost("album/create")]
    [Authorize(Policy = "blog:album:create")]
    [AccessLimit(60, 30)]
    [SwaggerOperation(Summary = "后台创建相册", Description = "后台创建相册")]
    public Task<ResponseResult<object>> CreateAlbum([FromBody] object albumDTO) {
        throw new NotImplementedException();
    }

    [HttpPost("upload")]
    [Authorize(Policy = "blog:photo:upload")]
    [AccessLimit(60, 30)]
    [SwaggerOperation(Summary = "后台上传照片", Description = "后台上传照片")]
    public Task<ResponseResult<object>> UploadPhoto(IFormFile file,
                                                    [FromForm] [StringLength(20, MinimumLength = 1)]
                                                    string name,
                                                    [FromForm] long? parentId = null) {
        throw new NotImplementedException();
    }

    [HttpPost("album/update")]
    [Authorize(Policy = "blog:album:update")]
    [AccessLimit(60, 30)]
    [SwaggerOperation(Summary = "后台修改相册", Description = "后台修改相册")]
    public Task<ResponseResult<object>> UpdateAlbum([FromBody] object albumDTO) {
        throw new NotImplementedException();
    }

    [HttpDelete("delete")]
    [Authorize(Policy = "blog:photo:delete")]
    [AccessLimit(60, 30)]
    [SwaggerOperation(Summary = "后台删除相册或照片", Description = "后台删除相册或照片")]
    public Task<ResponseResult<object>> DeletePhotoOrAlbum([FromBody] object deletePhotoOrAlbum) {
        throw new NotImplementedException();
    }
}