using System.Net;
using System.Net.Mail;

namespace P237_Nest.Services;

public class EmailService : IEmailService
{
    private readonly IConfiguration _configuration;

    public EmailService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void SendEmail(EmailDto emailDto)
    {
        SmtpClient smtpClient = new SmtpClient(_configuration["EmailSettings:Host"], int.Parse(_configuration["EmailSettings:Port"]))
        {
            Credentials = new NetworkCredential(_configuration["EmailSettings:Email"], _configuration["EmailSettings:Password"]),
            EnableSsl = bool.Parse(_configuration["EmailSettings:EnableSsl"]),
        };
        MailMessage mailMessage = new MailMessage()
        {
            Subject = emailDto.subject,
            From = new MailAddress(_configuration["EmailSettings:Email"]),
            IsBodyHtml = bool.Parse(_configuration["EmailSettings:IsHtml"]),
        };
        mailMessage.To.Add(emailDto.to);

        mailMessage.Body = @$"<a href=https://localhost:7075{emailDto.body} 
styles='border-radius:12px;text-decoration:none; background:gray; color:green;'>
Verify Email</a>";

        smtpClient.Send(mailMessage);
    }
}

public interface IEmailService
{
    void SendEmail(EmailDto emailDto);
}


public record EmailDto(string to, string subject, string body);

