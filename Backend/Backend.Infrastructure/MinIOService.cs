using Backend.Common.Enums;
using Backend.Common.Option;
using Backend.Common.Static;
using Backend.Contracts.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Minio;
using Minio.DataModel.Args;
using Minio.Exceptions;

namespace Backend.Infrastructure;

public class MinIOService : IMinIOService {
    private readonly IMinioClient _minio;
    private readonly MinioOptions _options;

    public string BucketName => _options.BucketName;

    public MinIOService(IOptions<MinioOptions> options) {
        _options = options.Value;
        _minio = new MinioClient()
                .WithEndpoint(_options.Endpoint.Replace("http://", "").Replace("https://", ""))
                .WithCredentials(_options.AccessKey, _options.SecretKey)
                .Build();
    }

    public async Task<string> UploadAsync(UploadEnum type, IFormFile file, string? fileName = null, string? extraDir = null) {
        var rule = UploadRules.Get(type);
        var ext = Path.GetExtension(file.FileName).ToLower();

        if (!rule.Formats.Contains(ext)) throw new Exception("不支持的文件格式");

        if (ConvertToMB(file.Length) > rule.LimitSizeMB) throw new Exception($"文件超过大小限制 {rule.LimitSizeMB}MB");

        var finalDir = rule.Dir + (string.IsNullOrWhiteSpace(extraDir) ? "" : extraDir.Trim('/') + "/");
        var finalName = fileName ?? Guid.NewGuid().ToString();
        var objectName = finalDir + finalName + ext;

        await using var stream = file.OpenReadStream();
        await _minio.PutObjectAsync(new PutObjectArgs()
                                   .WithBucket(_options.BucketName)
                                   .WithObject(objectName)
                                   .WithStreamData(stream)
                                   .WithObjectSize(file.Length)
                                   .WithContentType(file.ContentType));

        return $"{_options.Endpoint}/{_options.BucketName}/{objectName}";
    }

    public async Task<bool> DeleteAsync(string objectName) {
        await _minio.RemoveObjectAsync(new RemoveObjectArgs().WithBucket(_options.BucketName).WithObject(objectName));
        return true;
    }

    public async Task<List<string>> ListFilesAsync(string prefix) {
        var results = new List<string>();
        var observable = _minio.ListObjectsEnumAsync(new ListObjectsArgs()
                                                    .WithBucket(_options.BucketName)
                                                    .WithPrefix(prefix)
                                                    .WithRecursive(true));

        await foreach (var item in observable) results.Add(item.Key);
        return results;
    }

    public async Task<bool> FileExistsAsync(string objectName) {
        try {
            await _minio.StatObjectAsync(new StatObjectArgs().WithBucket(_options.BucketName).WithObject(objectName));
            return true;
        } catch (ObjectNotFoundException) {
            return false;
        }
    }

    public string GetFileName(string path) {
        return Path.GetFileName(path);
    }

    public double ConvertToMB(long sizeInBytes) {
        return Math.Round(sizeInBytes / 1024.0 / 1024.0, 2);
    }

    public double ConvertToKB(long sizeInBytes) {
        return Math.Round(sizeInBytes / 1024.0, 2);
    }

    public bool IsFormatValid(string fileName, List<string> allowedExtensions) {
        var ext = Path.GetExtension(fileName)?.ToLower();
        return allowedExtensions.Any(f => f.ToLower() == ext);
    }

    public bool IsFileTooLarge(long fileSize, double limitMB) {
        return ConvertToMB(fileSize) > limitMB;
    }
}