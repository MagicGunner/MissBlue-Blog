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
    public string? Msg { get; set; }

    /// <summary>返回数据</summary>
    public T? Data { get; set; }

    private ResponseResult() { }


    public static ResponseResult<T> Success(T? data = default, string? msg = null) =>
        new() {
                  Code = (int)RespEnum.Success,
                  Msg = msg ?? RespEnum.Success.GetDescription(),
                  Data = data
              };

    public static ResponseResult<T> Failure(int code = (int)RespEnum.Failure, string? msg = null, T? data = default) =>
        new() {
                  Code = code,
                  Msg = msg ?? RespEnum.Failure.GetDescription(),
                  Data = data
              };
}