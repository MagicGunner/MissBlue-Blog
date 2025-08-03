namespace Backend.Modules.Blog.Contracts.DTO;

public class SearchLeaveWordDTO {
    public string? UserName  { get; set; }
    public int?    IsCheck   { get; set; }
    public string? StartTime { get; set; }
    public string? EndTime   { get; set; }
}