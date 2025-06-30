namespace Backend.Contracts.VO;

/// <summary>
/// 授权信息视图对象（AuthorizeVO）
/// </summary>
public class AuthorizeVO {
    /// <summary>
    /// 访问令牌（JWT Token）
    /// </summary>
    public string Token { get; set; }

    /// <summary>
    /// Token 过期时间
    /// </summary>
    public DateTime Expire { get; set; }
}