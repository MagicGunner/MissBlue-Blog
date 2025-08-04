namespace Backend.Contracts.IService;

public interface IEmailService {
    Task Send(string to, string subject, string htmlBody);
}