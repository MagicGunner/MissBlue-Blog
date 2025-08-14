namespace Backend.Contracts.DTO;

public class MenuDTO {
    public long? Id { get; set; }

    public long? ParentId { get; set; }

    public string Title { get; set; }

    public List<long>? RoleId { get; set; }

    public int? OrderNum { get; set; }

    public string Icon { get; set; }

    public int? RouterType { get; set; }

    public string? Component { get; set; }

    public string? Redirect { get; set; }

    public string? Path { get; set; }

    public string? Url { get; set; }

    public string? Target { get; set; }

    public int? Affix { get; set; }

    public int? KeepAlive { get; set; }

    public int? HideInMenu { get; set; }

    public int? IsDisable { get; set; }
}