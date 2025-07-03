using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Backend.Controllers;

[ApiController]
[Route("api/upload")]
[SwaggerTag("文件上传")]
public class UploadController : ControllerBase {
}