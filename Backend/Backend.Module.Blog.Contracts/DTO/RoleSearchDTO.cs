namespace Backend.Modules.Blog.Contracts.DTO;

public class RoleSearchDTO {
    // 角色名称
    public string RoleName { get; set; }

    // 角色字符
    public string RoleKey { get; set; }

    // 状态（0：正常，1：停用）
    public int? Status { get; set; }

    // 创建时间范围 - 开始
    public DateTime? CreateTimeStart { get; set; }

    // 创建时间范围 - 结束
    public DateTime? CreateTimeEnd { get; set; }
}