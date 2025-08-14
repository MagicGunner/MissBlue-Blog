using System.ComponentModel.DataAnnotations;

namespace Backend.Contracts.DTO;

public class StationmasterInfoDTO {
    /// <summary>
    /// 站长名称（最多30个字符）
    /// </summary>
    [MaxLength(30, ErrorMessage = "站长名称字数不能超过30")]
    public string WebmasterName { get; set; }

    /// <summary>
    /// 站长文案（最多100个字符）
    /// </summary>
    [MaxLength(100, ErrorMessage = "站长文案字数不能超过100")]
    public string WebmasterCopy { get; set; }

    /// <summary>
    /// Gitee 链接（最多100个字符）
    /// </summary>
    [MaxLength(100, ErrorMessage = "gitee链接字数不能超过100")]
    public string GiteeLink { get; set; }

    /// <summary>
    /// GitHub 链接（最多100个字符）
    /// </summary>
    [MaxLength(100, ErrorMessage = "github链接字数不能超过100")]
    public string GithubLink { get; set; }
}