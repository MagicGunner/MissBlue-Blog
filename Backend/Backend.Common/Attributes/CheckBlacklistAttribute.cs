using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Backend.Common.Attributes;

public class CheckBlacklistAttribute : Attribute, IAsyncActionFilter {
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next) {
        var httpContext = context.HttpContext;
        var user = httpContext.User;

        // if (true) {                              // 黑名单校验逻辑，例如 userId 在黑名单中
        //     context.Result = new ForbidResult(); // 或返回 403 错误
        //     return;
        // }
        // todo 完善黑名单校验逻辑

        await next(); // 继续执行管道
    }
}