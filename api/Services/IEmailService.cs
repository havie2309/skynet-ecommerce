namespace Skinet.Api.Services;

public interface IEmailService
{
    Task SendOrderConfirmationAsync(string toEmail, int orderId);
}
