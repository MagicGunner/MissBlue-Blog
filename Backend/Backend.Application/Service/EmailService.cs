using Backend.Common.Option;
using Backend.Contracts.IService;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;

namespace Backend.Application.Service;

public class EmailService(IOptions<MailOptions> settings, ILogger<EmailService> logger) : IEmailService {
    private readonly MailOptions _settings = settings.Value;

    public async Task Send(string to, string subject, string htmlBody) {
        var email = new MimeMessage();
        email.From.Add(new MailboxAddress(_settings.DisplayName, _settings.UserName));
        email.To.Add(MailboxAddress.Parse(to));
        email.Subject = subject;

        var builder = new BodyBuilder {
                                          HtmlBody = htmlBody
                                      };

        email.Body = builder.ToMessageBody();

        try {
            using var smtp = new SmtpClient();
            await smtp.ConnectAsync(_settings.Host, _settings.Port, SecureSocketOptions.SslOnConnect);
            await smtp.AuthenticateAsync(_settings.UserName, _settings.Password);
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);

            logger.LogInformation("邮件发送成功：To={To}, Subject={Subject}", to, subject);
        } catch (Exception ex) {
            logger.LogError(ex, "邮件发送失败：To={To}, Subject={Subject}", to, subject);
            throw;
        }
    }
}