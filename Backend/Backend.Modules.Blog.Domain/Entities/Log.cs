using SqlSugar;

namespace Backend.Domain.Entities;

[SugarTable("sys_log")]
public class Log : RootEntity<long> {
    /// <summary>
    /// 模块名称
    /// </summary>
    public string Module { get; set; }

    /// <summary>
    /// 操作名称
    /// </summary>
    public string Operation { get; set; }

    /// <summary>
    /// 操作人员用户名
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
    /// 操作状态（0：成功，1：失败，2：异常）
    /// </summary>
    public int State { get; set; }

    /// <summary>
    /// 操作方法名（类名.方法名）
    /// </summary>
    public string Method { get; set; }

    /// <summary>
    /// 请求方式（如 GET/POST）
    /// </summary>
    public string ReqMapping { get; set; }

    /// <summary>
    /// 请求参数（JSON 字符串）
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
    /// 请求耗时（毫秒）
    /// </summary>
    public long Time { get; set; }

    /// <summary>
    /// 操作描述
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// 创建时间（插入时自动填充）
    /// </summary>
    [SugarColumn(IsOnlyIgnoreInsert = false, IsOnlyIgnoreUpdate = true)]
    public DateTime CreateTime { get; set; }

    /// <summary>
    /// 更新时间（插入和更新时自动填充）
    /// </summary>
    [SugarColumn(IsOnlyIgnoreInsert = false, IsOnlyIgnoreUpdate = false)]
    public DateTime UpdateTime { get; set; }

    /// <summary>
    /// 是否删除（0：未删除，1：已删除）
    /// </summary>
    public int IsDeleted { get; set; }
}