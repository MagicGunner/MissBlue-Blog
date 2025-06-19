namespace Backend.Modules.Blog.Contracts.VO;

/// <summary>
/// 操作日志视图对象（LogVO）
/// </summary>
public class LogVO {
    /// <summary>
    /// 编号
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// 模块名称
    /// </summary>
    public string Module { get; set; }

    /// <summary>
    /// 操作
    /// </summary>
    public string Operation { get; set; }

    /// <summary>
    /// 操作人员
    /// </summary>
    public string UserName { get; set; }

    /// <summary>
    /// IP 地址
    /// </summary>
    public string Ip { get; set; }

    /// <summary>
    /// 操作地点
    /// </summary>
    public string Address { get; set; }

    /// <summary>
    /// 操作状态（0：成功，1：失败）
    /// </summary>
    public int State { get; set; }

    /// <summary>
    /// 操作方法
    /// </summary>
    public string Method { get; set; }

    /// <summary>
    /// 请求方式
    /// </summary>
    public string ReqMapping { get; set; }

    /// <summary>
    /// 请求参数
    /// </summary>
    public string ReqParameter { get; set; }

    /// <summary>
    /// 异常信息
    /// </summary>
    public string Exception { get; set; }

    /// <summary>
    /// 返回参数
    /// </summary>
    public string ReturnParameter { get; set; }

    /// <summary>
    /// 请求地址
    /// </summary>
    public string ReqAddress { get; set; }

    /// <summary>
    /// 消耗时间（毫秒）
    /// </summary>
    public long Time { get; set; }

    /// <summary>
    /// 操作描述
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// 时间
    /// </summary>
    public DateTime LoginTime { get; set; }
}