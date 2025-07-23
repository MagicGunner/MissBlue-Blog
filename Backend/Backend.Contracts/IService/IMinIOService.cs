using Backend.Common.Enums;
using Microsoft.AspNetCore.Http;

namespace Backend.Contracts.IService;

public interface IMinIOService {
    string             BucketName { get; }
    Task<string>       UploadAsync(UploadEnum type, IFormFile file, string? fileName = null, string? extraDir = null);
    Task<bool>         DeleteAsync(string     objectName);
    Task<List<string>> ListFilesAsync(string  prefix);
    Task<bool>         FileExistsAsync(string objectName);
}