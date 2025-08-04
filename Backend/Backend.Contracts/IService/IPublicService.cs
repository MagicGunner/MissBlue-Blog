namespace Backend.Contracts.IService;

public interface IPublicService {
    Task<string> RegisterEmailVerifyCode(string type, string email);
    void         SendEmail(string               type, string email, Dictionary<string, object>? content);
}