using Backend.Common.Redis;
using Backend.Modules.Blog.Domain.Entities;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SqlSugar;

namespace Backend.Extensions.ServiceExtensions;

public class VisitCountSyncService(ILogger<VisitCountSyncService> logger,
                                   ISqlSugarClient                db,
                                   IRedisBasketRepository         redis)
    : BackgroundService {
    private readonly TimeSpan _interval = TimeSpan.FromMinutes(5);

    protected override async Task ExecuteAsync(CancellationToken stoppingToken) {
        while (!stoppingToken.IsCancellationRequested) {
            logger.LogInformation("开始同步文章访问量...");
            try {
                var articleIds = await db.Queryable<Article>().Select(a => a.Id).ToListAsync(stoppingToken);

                foreach (var id in articleIds) {
                    var redisKey = $"article:count:visit:{id}";
                    var visitCountStr = await redis.GetValue(redisKey);

                    if (long.TryParse(visitCountStr, out var visitCount)) {
                        await db.Updateable<Article>()
                                .SetColumns(a => a.VisitCount == visitCount)
                                .Where(a => a.Id == id)
                                .ExecuteCommandAsync(stoppingToken);
                    }
                }

                logger.LogInformation("同步文章访问量成功。");
            } catch (Exception ex) {
                logger.LogError(ex, "同步文章访问量失败");
            }

            await Task.Delay(_interval, stoppingToken); // 等待5分钟
        }
    }
}