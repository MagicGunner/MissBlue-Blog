namespace Backend.Modules.Blog.Contracts.IService;

public interface IPublicService {
    string RegisterEmailVerifyCode(string type, string email);
    void   SendEmail(string               type, string email, Dictionary<string, object> content);
}