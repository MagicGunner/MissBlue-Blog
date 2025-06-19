namespace Backend.Modules.Blog.Contracts.DTO;

public class UserSearchDTO {
    // 用户名
    public string Username { get; set; }

    // 用户邮箱
    public string Email { get; set; }

    // 是否禁用：0 否，1 是
    public int? IsDisable { get; set; }

    // 创建时间开始
    public DateTime? CreateTimeStart { get; set; }

    // 创建时间结束
    public DateTime? CreateTimeEnd { get; set; }
}