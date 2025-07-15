namespace Backend.Application.Interface;

public interface ICurrentUser {
    long?          UserId          { get; }
    public string? IpAddress       { get; }
    string         UserName        { get; }
    List<string>   Roles           { get; }
    List<string>   Permissions     { get; }
    bool           IsAuthenticated { get; }
}