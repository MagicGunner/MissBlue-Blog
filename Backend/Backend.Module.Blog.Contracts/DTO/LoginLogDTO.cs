namespace Backend.Modules.Blog.Contracts.DTO;

public class LoginLogDTO {
    /// <summary>用户名称</summary>
    public string UserName { get; set; }

    /// <summary>登录地址</summary>
    public string Address { get; set; }

    /// <summary>登录状态 (0：成功，1：失败)</summary>
    public int? State { get; set; }

    /// <summary>登录开始时间</summary>
    public DateTime? LoginTimeStart { get; set; }

    /// <summary>登录结束时间</summary>
    public DateTime? LoginTimeEnd { get; set; }
}