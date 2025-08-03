using System.Collections;
using Backend.Common.Enums;
using Backend.Common.Extensions;

namespace Backend.Common.Results;

/// <summary>
/// 统一API的相应格式
/// </summary>
public class ResponseResult<T> {
    /// <summary>状态码</summary>
    public int Code { get; set; }

    /// <summary>提示信息</summary>
    public string? Message { get; set; }

    /// <summary>返回数据</summary>
    public T? Data { get; set; }

    public ResponseResult(bool result, T? data = default, RespCode errorType = RespCode.Success, string? msg = null) {
        if (result) {
            Code = (int)RespCode.Success;
        } else {
            Code = int.Max((int)errorType, 500);
        }
    
        Data = data;
        Message = msg ?? ((RespCode)Code).GetDescription();
    }

    /// <summary>
    /// 只传入数据，自动判断是否成功
    /// </summary>
    public ResponseResult(T? data, RespCode errorType = RespCode.Success, string? msg = null) {
        var isSuccess = data switch {
                            null                   => false,
                            string str             => !string.IsNullOrWhiteSpace(str),
                            ICollection collection => collection.Count > 0,
                            IEnumerable enumerable => enumerable.Cast<object>().Any(), // 泛型枚举
                            bool b                 => b,                               // 加这行！
                            _                      => true                             // 其他对象类型只要非 null 就认为成功
                        };
        return new ResponseResult(isSuccess, data, errorType, msg);
    }
}