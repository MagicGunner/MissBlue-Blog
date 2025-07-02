using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;

namespace Backend.Common.Attributes;

public class AccessLimitAttribute(int seconds, int maxCount) : Attribute, IAsyncActionFilter {
    private static readonly MemoryCache Cache = new(new MemoryCacheOptions());

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next) {
        var ip = context.HttpContext.Connection.RemoteIpAddress?.ToString() ?? "unknown";
        var path = context.HttpContext.Request.Path;
        var key = $"{ip}:{path}";

        if (!Cache.TryGetValue(key, out int count)) {
            Cache.Set(key, 1, TimeSpan.FromSeconds(seconds));
        } else if (count >= maxCount) {
            context.Result = new ContentResult {
                                                   StatusCode = StatusCodes.Status429TooManyRequests,
                                                   Content = "请求过于频繁，请稍后再试"
                                               };
            return;
        } else {
            Cache.Set(key, count + 1, TimeSpan.FromSeconds(seconds));
        }

        await next();
    }
}