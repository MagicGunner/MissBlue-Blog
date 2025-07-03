namespace Backend.Modules.Blog.Contracts.DTO;

public class SearchLeaveWordDTO {
    public string UserName  { get; set; }
    public int    isCheck   { get; set; }
    public string startTime { get; set; }
    public string endTime   { get; set; }
}