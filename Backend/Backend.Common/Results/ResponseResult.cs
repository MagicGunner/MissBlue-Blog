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

    public ResponseResult(bool result, T? data = default, RespEnum errorType = RespEnum.Success, string? msg = null) {
        if (result) {
            Code = (int)RespEnum.Success;
        } else {
            Code = int.Max((int)errorType, 500);
        }

        Data = data;
        Message = msg ?? ((RespEnum)Code).GetDescription();
    }
}