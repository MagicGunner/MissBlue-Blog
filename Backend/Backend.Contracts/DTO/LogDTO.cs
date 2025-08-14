using System.ComponentModel.DataAnnotations;

namespace Backend.Contracts.DTO;

public class LogDTO {
    /// <summary>
    /// IP 地址
    /// </summary>
    public string Ip { get; set; }

    /// <summary>
    /// 模块名称
    /// </summary>
    public string Module { get; set; }

    /// <summary>
    /// 操作人员
    /// </summary>
    public string UserName { get; set; }

    /// <summary>
    /// 操作类型
    /// </summary>
    public string Operation { get; set; }

    /// <summary>
    /// 操作状态 (0：成功，1：失败)
    /// </summary>
    public int? State { get; set; }

    /// <summary>
    /// 操作时间开始
    /// </summary>
    public DateTime? LogTimeStart { get; set; }

    /// <summary>
    /// 操作时间结束
    /// </summary>
    public DateTime? LogTimeEnd { get; set; }

    /// <summary>
    /// 当前页
    /// </summary>
    [Required(ErrorMessage = "当前页不能为空")]
    public long Current { get; set; }

    /// <summary>
    /// 每页数量
    /// </summary>
    [Required(ErrorMessage = "每页数量不能为空")]
    public long PageSize { get; set; }
}