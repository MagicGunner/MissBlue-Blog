namespace Backend.Application.Interface;

public interface ICurrentUser {
    long?        UserId          { get; }
    string       UserName        { get; }
    List<string> Roles           { get; }
    List<string> Permissions     { get; }
    bool         IsAuthenticated { get; }
}