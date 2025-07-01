using System.Net;
using System.Net.Sockets;
using Microsoft.AspNetCore.Http;

namespace Backend.Common.Utils;

public class IpUtils {
    /// <summary>
    /// 获取客户端IP地址（支持多级代理）
    /// </summary>
    public static string? GetIpAddr(HttpContext? context) {
        if (context == null) return "unknown";

        var headers = context.Request.Headers;
        var ip = headers["X-Forwarded-For"].FirstOrDefault();

        if (IsUnknown(ip)) ip = headers["X-Real-IP"].FirstOrDefault();
        if (IsUnknown(ip)) ip = headers["Proxy-Client-IP"].FirstOrDefault();
        if (IsUnknown(ip)) ip = headers["WL-Proxy-Client-IP"].FirstOrDefault();
        if (IsUnknown(ip)) ip = context.Connection.RemoteIpAddress?.ToString();

        if (ip is "::1" or "0:0:0:0:0:0:0:1") ip = "127.0.0.1";

        return GetMultistageReverseProxyIp(ip);
    }

    public static string GetHostIp() {
        try {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList) {
                // 只取IPv4地址
                if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork) {
                    return ip.ToString();
                }
            }
        } catch {
            return "127.0.0.1";
        }

        return "127.0.0.1";
    }

    /// <summary>
    /// 从多级代理中获取第一个有效IP
    /// </summary>
    private static string? GetMultistageReverseProxyIp(string? ip) {
        if (!string.IsNullOrWhiteSpace(ip) && ip.Contains(',')) {
            foreach (var subIp in ip.Split(',')) {
                if (!IsUnknown(subIp)) {
                    return subIp.Trim();
                }
            }
        }

        return ip?.Trim();
    }

    /// <summary>
    /// 判断IP是否是unknown
    /// </summary>
    private static bool IsUnknown(string? ip) {
        return string.IsNullOrWhiteSpace(ip) || ip.Equals("unknown", StringComparison.OrdinalIgnoreCase);
    }

    /// <summary>
    /// 判断是否内网IP
    /// </summary>
    public static bool IsInternalIp(string ip) {
        if (string.IsNullOrWhiteSpace(ip)) return false;

        if (ip is "127.0.0.1" or "::1") return true;

        if (IPAddress.TryParse(ip, out var ipAddr)) {
            var bytes = ipAddr.GetAddressBytes();
            return bytes[0] switch {
                       10  => true,
                       172 => bytes[1] >= 16 && bytes[1] <= 31,
                       192 => bytes[1] == 168,
                       _   => false
                   };
        }

        return false;
    }

    /// <summary>
    /// 获取本机IP
    /// </summary>
    public static string GetLocalIp() {
        try {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList) {
                if (ip.AddressFamily == AddressFamily.InterNetwork) {
                    return ip.ToString();
                }
            }
        } catch {
            return "127.0.0.1";
        }

        return "127.0.0.1";
    }

    /// <summary>
    /// 获取本地主机名
    /// </summary>
    public static string GetHostName() {
        try {
            return Dns.GetHostName();
        } catch {
            return "未知";
        }
    }
}