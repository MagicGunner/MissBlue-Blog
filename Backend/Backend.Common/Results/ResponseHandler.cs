using System.Collections;
using Backend.Common.Enums;
using Backend.Common.Extensions;

namespace Backend.Common.Results;

/// <summary>
/// 统一API的相应格式
/// </summary>
public static class ResponseHandler<T> {
    /// <summary>
    /// 只传入数据，自动判断是否成功
    /// </summary>
    public static ResponseResult<T> Create(T? data, RespCode errorType = RespCode.Success, string? msg = null) {
        var isSuccess = data switch {
                            null                   => false,
                            string str             => !string.IsNullOrWhiteSpace(str),
                            ICollection collection => collection.Count > 0,
                            IEnumerable enumerable => enumerable.Cast<object>().Any(), // 泛型枚举
                            bool b                 => b,                               // 加这行！
                            _                      => true                             // 其他对象类型只要非 null 就认为成功
                        };
        return new ResponseResult<T>(isSuccess, data, errorType, msg);
    }

    public static ResponseResult<T> Create((bool isSuccess, string? msg) result) {
        return new ResponseResult<T>(result.isSuccess, msg: result.msg);
    }
}