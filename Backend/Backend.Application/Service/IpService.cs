namespace Backend.Application.Service;

public class IpService {
    // private readonly ILogger<IpService>   _logger;
    // private readonly IBlackListRepository _blackListRepo;
    // private readonly IUserRepository      _userRepo;
    // private readonly ILoginLogRepository  _loginLogRepo;
    // private readonly ILogRepository       _logRepo;
    // private readonly IHttpClientFactory   _httpClientFactory;
    //
    // private readonly BlockingCollection<Func<Task>> _taskQueue = new(500);
    // private readonly CancellationTokenSource        _cts       = new();
    // private readonly Task                           _worker;
    //
    // public IpService(ILogger<IpService>   logger,
    //                  IBlackListRepository blackListRepo,
    //                  IUserRepository      userRepo,
    //                  ILoginLogRepository  loginLogRepo,
    //                  ILogRepository       logRepo,
    //                  IHttpClientFactory   httpClientFactory) {
    //     _logger = logger;
    //     _blackListRepo = blackListRepo;
    //     _userRepo = userRepo;
    //     _loginLogRepo = loginLogRepo;
    //     _logRepo = logRepo;
    //     _httpClientFactory = httpClientFactory;
    //
    //     // 启动后台线程
    //     _worker = Task.Run(WorkerLoopAsync);
    // }
    //
    // private async Task WorkerLoopAsync() {
    //     foreach (var task in _taskQueue.GetConsumingEnumerable(_cts.Token)) {
    //         try {
    //             await task();
    //         } catch (Exception ex) {
    //             _logger.LogError(ex, "执行 IP 刷新任务失败");
    //         }
    //     }
    // }
    //
    // private void Enqueue(Func<Task> task) {
    //     if (!_taskQueue.TryAdd(task)) _logger.LogWarning("任务队列已满，丢弃 IP 刷新任务");
    // }
    //
    // public Task RefreshIpDetailAsyncByBid(long bid) {
    //     Enqueue(async () => {
    //                 var bl = await _blackListRepo.GetByIdAsync(bid);
    //                 var ip = bl?.IpInfo?.CreateIp;
    //                 if (string.IsNullOrWhiteSpace(ip)) return;
    //
    //                 var detail = await TryGetIpDetailRetryAsync(ip);
    //                 if (detail != null) {
    //                     bl.IpInfo.IpDetail = detail;
    //                     await _blackListRepo.UpdateAsync(bl);
    //                 } else _logger.LogError("黑名单 IP 获取失败: {Ip}, Bid: {Bid}", ip, bid);
    //             });
    //
    //     return Task.CompletedTask;
    // }
    //
    // public Task RefreshIpDetailAsyncByUidAndRegister(long uid) {
    //     Enqueue(async () => {
    //                 var user = await _userRepo.GetByIdAsync(uid);
    //                 var ip = user?.RegisterIp;
    //                 if (string.IsNullOrWhiteSpace(ip)) return;
    //
    //                 var detail = await TryGetIpDetailRetryAsync(ip);
    //                 user.RegisterAddress = detail != null
    //                                            ? BuildAddr(detail.Region, detail.City, detail.Country)
    //                                            : "未知";
    //
    //                 if (detail == null) _logger.LogError("注册 IP 获取失败: {Ip}, Uid: {Uid}", ip, uid);
    //
    //                 await _userRepo.UpdateAsync(user);
    //             });
    //
    //     return Task.CompletedTask;
    // }
    //
    // public Task RefreshIpDetailAsyncByUidAndLogin(long uid) {
    //     Enqueue(async () => {
    //                 var user = await _userRepo.GetByIdAsync(uid);
    //                 var ip = user?.LoginIp;
    //                 if (string.IsNullOrWhiteSpace(ip)) return;
    //
    //                 var detail = await TryGetIpDetailRetryAsync(ip);
    //                 user.LoginAddress = detail != null
    //                                         ? BuildAddr(detail.Region, detail.City, detail.Country)
    //                                         : "未知";
    //
    //                 if (detail == null) _logger.LogError("登录 IP 获取失败: {Ip}, Uid: {Uid}", ip, uid);
    //
    //                 await _userRepo.UpdateAsync(user);
    //             });
    //
    //     return Task.CompletedTask;
    // }
    //
    // public Task RefreshIpDetailAsyncByLogIdAndLogin(long logId) {
    //     Enqueue(async () => {
    //                 var log = await _loginLogRepo.GetByIdAsync(logId);
    //                 var ip = log?.Ip;
    //                 if (string.IsNullOrWhiteSpace(ip)) return;
    //
    //                 var detail = await TryGetIpDetailRetryAsync(ip);
    //                 log.Address = detail != null
    //                                   ? BuildAddr(detail.Region, detail.City, detail.Country)
    //                                   : "未知";
    //
    //                 if (detail == null) _logger.LogError("登录日志 IP 获取失败: {Ip}, LogId: {LogId}", ip, logId);
    //
    //                 await _loginLogRepo.UpdateAsync(log);
    //             });
    //
    //     return Task.CompletedTask;
    // }
    //
    // public Task RefreshIpDetailAsyncByLogId(long logId) {
    //     Enqueue(async () => {
    //                 var log = await _logRepo.GetByIdAsync(logId);
    //                 var ip = log?.Ip;
    //                 if (string.IsNullOrWhiteSpace(ip)) return;
    //
    //                 var detail = await TryGetIpDetailRetryAsync(ip);
    //                 log.Address = detail != null
    //                                   ? BuildAddr(detail.Region, detail.City, detail.Country)
    //                                   : "未知";
    //
    //                 if (detail == null) _logger.LogError("操作日志 IP 获取失败: {Ip}, LogId: {LogId}", ip, logId);
    //
    //                 await _logRepo.UpdateAsync(log);
    //             });
    //
    //     return Task.CompletedTask;
    // }
    //
    // private static string BuildAddr(string region, string city, string country) {
    //     if (city == "内网IP") return "内网IP";
    //     if (country != "中国") return country;
    //     if (region == "XX" && city == "XX") return "未知";
    //     if (region == "XX") return city;
    //     if (city == "XX") return region;
    //     if (region == city) return region;
    //     return $"{region} {city}";
    // }
    //
    // private async Task<IpDetail?> TryGetIpDetailRetryAsync(string ip) {
    //     for (int i = 0; i < 3; i++) {
    //         var detail = await GetIpDetailAsync(ip);
    //         if (detail != null) return detail;
    //         await Task.Delay(2000);
    //     }
    //
    //     return null;
    // }
    //
    // private async Task<IpDetail?> GetIpDetailAsync(string ip) {
    //     try {
    //         var url = string.Format(ThirdPartyInterfaceConst.TAOBAO_IP_DETAIL, ip);
    //         var client = _httpClientFactory.CreateClient();
    //         var response = await client.GetStringAsync(url);
    //
    //         var result = JsonSerializer.Deserialize<IpResult<IpDetail>>(response);
    //         return result?.Code == 0 ? result.Data : null;
    //     } catch (Exception ex) {
    //         _logger.LogError(ex, "IP 查询失败");
    //         return null;
    //     }
    // }
    //
    // public void Dispose() {
    //     _cts.Cancel();
    //     _taskQueue.CompleteAdding();
    //     _worker.Wait(TimeSpan.FromSeconds(30));
    // }
}