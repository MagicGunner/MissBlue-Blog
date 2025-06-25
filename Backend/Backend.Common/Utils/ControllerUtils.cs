using Backend.Common.Results;

namespace Backend.Common.Utils;

public static class ControllerUtils {
    public static async Task<ResponseResult<T>> MessageHandler<T>(Func<Task<T>> func) {
        try {
            var result = await func();
            return ResponseResult<T>.Success(result);
        } catch (Exception ex) {
            // 可以在这里记录日志
            Console.WriteLine(ex); // 或者使用 ILogger
            return ResponseResult<T>.Failure(msg: ex.Message);
        }
    }
}