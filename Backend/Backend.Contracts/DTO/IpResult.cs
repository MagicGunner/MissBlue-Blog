namespace Backend.Contracts.DTO;

/// <summary>
/// 通用响应结果封装
/// </summary>
/// <typeparam name="T">返回数据类型</typeparam>
public class IpResult<T> {
    /// <summary>
    /// 状态码（0表示成功）
    /// </summary>
    public int? Code { get; set; }

    /// <summary>
    /// 提示信息
    /// </summary>
    public string Msg { get; set; }

    /// <summary>
    /// 返回数据
    /// </summary>
    public T Data { get; set; }

    /// <summary>
    /// 是否成功（code==0）
    /// </summary>
    public bool IsSuccess => Code is 0;
}