using Backend.Common.IP;
using SqlSugar;

namespace Backend.Domain.Entities;

[SugarTable("t_black_list")]
public class BlackList {
    [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
    public long Id { get; set; }

    public long UserId { get; set; }

    public string Reason { get; set; }

    // 封禁时间（插入时填充）
    [SugarColumn(IsOnlyIgnoreInsert = false, IsOnlyIgnoreUpdate = true)]
    public DateTime BannedTime { get; set; }

    public DateTime? ExpiresTime { get; set; }

    /// <summary>
    /// 类型（1：用户，2：攻击者）
    /// </summary>
    public int Type { get; set; }

    /// <summary>
    /// ip信息，仅当 type = 2 时有值
    /// </summary>
    [SugarColumn(ColumnName = "ip_info", ColumnDataType = "json")]
    public BlackListIpInfo IpInfo { get; set; }

    [SugarColumn(IsOnlyIgnoreInsert = false, IsOnlyIgnoreUpdate = true)]
    public DateTime CreateTime { get; set; }

    [SugarColumn(IsOnlyIgnoreInsert = true, IsOnlyIgnoreUpdate = false)]
    public DateTime UpdateTime { get; set; }

    public int IsDeleted { get; set; } = 0;
}