namespace Backend.Domain.Entities.IP;

public class BlackListIpInfo {
    /// <summary>
    /// 黑名单的 IP 地址
    /// </summary>
    public string CreateIp { get; set; }

    /// <summary>
    /// IP 详情（子对象）
    /// </summary>
    public IpDetail IpDetail { get; set; }
}