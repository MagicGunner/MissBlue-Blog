using Microsoft.AspNetCore.Http;

namespace Backend.Common.Utils;

public static class WebUtil {
    /// <summary>
    /// 将字符串直接写入到 HTTP 响应中（用于非 MVC 异步输出）
    /// </summary>
    /// <param name="response">HttpResponse 对象</param>
    /// <param name="content">输出内容（字符串）</param>
    /// <param name="contentType">Content-Type，如 application/json、text/html</param>
    /// <returns>Task</returns>
    public static async Task RenderString(HttpResponse response, string content, string contentType = "application/json") {
        response.StatusCode = 200;
        response.ContentType = contentType;
        response.Headers["Charset"] = "utf-8";
        await response.WriteAsync(content);
    }
}