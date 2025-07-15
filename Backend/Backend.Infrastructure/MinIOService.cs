using Backend.Common.Enums;
using Backend.Common.Option;
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

    private readonly Dictionary<UploadEnum, (string[] Formats, double MaxSizeMB, string Dir)> _rules = new() {
                                                                                                                 { UploadEnum.Avatar, ([".jpg", ".png", ".jpeg"], 2.0, "avatar/") },
                                                                                                                 { UploadEnum.Article, ([".jpg", ".png", ".jpeg", ".gif"], 5.0, "article/") },
                                                                                                                 { UploadEnum.Other, ([".zip", ".pdf", ".docx"], 10.0, "other/") }
                                                                                                             };

    public MinIOService(IOptions<MinioOptions> options) {
        _options = options.Value;
        _minio = new MinioClient()
                .WithEndpoint(_options.Endpoint.Replace("http://", "").Replace("https://", ""))
                .WithCredentials(_options.AccessKey, _options.SecretKey)
                .Build();
    }

    public async Task<string> UploadAsync(UploadEnum type, IFormFile file, string? fileName = null, string? extraDir = null) {
        var (formats, maxSizeMB, baseDir) = _rules[type];
        var ext = Path.GetExtension(file.FileName).ToLower();
        if (!formats.Contains(ext)) throw new Exception("不支持的文件格式");
        if (ConvertToMB(file.Length) > maxSizeMB) throw new Exception($"文件超过大小限制 {maxSizeMB}MB");

        var finalDir = baseDir + (string.IsNullOrWhiteSpace(extraDir) ? "" : extraDir.Trim('/') + "/");
        var finalName = fileName ?? Guid.NewGuid().ToString();
        var objectName = finalDir + finalName + ext;

        using var stream = file.OpenReadStream();
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

    private static double ConvertToMB(long sizeInBytes) => Math.Round(sizeInBytes / 1024.0 / 1024.0, 2);
}