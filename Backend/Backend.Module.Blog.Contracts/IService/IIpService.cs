namespace Backend.Modules.Blog.Contracts.IService;

public interface IIpService {
    /// <summary>
    /// 异步刷新 IP 详情（黑名单）
    /// </summary>
    /// <param name="bid">黑名单 ID</param>
    Task RefreshIpDetailAsyncByBid(long bid);

    /// <summary>
    /// 异步刷新 IP 详情（注册）
    /// </summary>
    /// <param name="uid">用户 ID</param>
    Task RefreshIpDetailAsyncByUidAndRegister(long uid);

    /// <summary>
    /// 异步刷新 IP 详情（登录）
    /// </summary>
    /// <param name="uid">用户 ID</param>
    Task RefreshIpDetailAsyncByUidAndLogin(long uid);

    /// <summary>
    /// 异步刷新 IP 详情（登录日志）
    /// </summary>
    /// <param name="loginLogId">登录日志 ID</param>
    Task RefreshIpDetailAsyncByLogIdAndLogin(long loginLogId);

    /// <summary>
    /// 异步刷新 IP 详情（操作日志）
    /// </summary>
    /// <param name="logId">操作日志 ID</param>
    Task RefreshIpDetailAsyncByLogId(long logId);
}