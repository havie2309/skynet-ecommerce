using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Options;

namespace Skinet.Api.Services;

public class EmailService : IEmailService
{
    private readonly EmailSettings _emailSettings;

    public EmailService(IOptions<EmailSettings> emailOptions)
    {
        _emailSettings = emailOptions.Value;
    }

    public async Task SendOrderConfirmationAsync(string toEmail, int orderId)
    {
        using var client = new SmtpClient(_emailSettings.SmtpServer, _emailSettings.Port)
        {
            Credentials = new NetworkCredential(_emailSettings.Username, _emailSettings.Password),
            EnableSsl = true
        };

        var mailMessage = new MailMessage
        {
            From = new MailAddress(_emailSettings.From),
            Subject = "Order Confirmation",
            Body = $"Your order #{orderId} has been placed successfully.",
            IsBodyHtml = false
        };

        mailMessage.To.Add(toEmail);

        await client.SendMailAsync(mailMessage);
    }
}
