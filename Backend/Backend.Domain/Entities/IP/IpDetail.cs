namespace Backend.Domain.Entities.IP;

public class IpDetail {
    /// <summary>
    /// 注册时的 IP
    /// </summary>
    public string Ip { get; set; }

    /// <summary>
    /// 最新登录的 ISP 名称
    /// </summary>
    public string Isp { get; set; }

    /// <summary>
    /// 最新登录的 ISP 编号
    /// </summary>
    public string IspId { get; set; }

    public string City { get; set; }

    public string CityId { get; set; }

    public string Country { get; set; }

    public string CountryId { get; set; }

    public string Region { get; set; }

    public string RegionId { get; set; }
}