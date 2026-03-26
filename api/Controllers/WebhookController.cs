using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Stripe;
using Skinet.Api.Data;
using Skinet.Api.Services;

namespace Skinet.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WebhookController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly StripeSettings _stripeSettings;
    private readonly ILogger<WebhookController> _logger;
    private readonly AppMetrics _metrics;

    public WebhookController(
        AppDbContext context,
        IOptions<StripeSettings> stripeOptions,
        ILogger<WebhookController> logger,
        AppMetrics metrics)
    {
        _context = context;
        _stripeSettings = stripeOptions.Value;
        _logger = logger;
        _metrics = metrics;
    }

    [HttpPost("stripe")]
    public async Task<IActionResult> HandleStripeWebhook()
    {
        var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();

        Event stripeEvent;

        try
        {
            var signatureHeader = Request.Headers["Stripe-Signature"];
            stripeEvent = EventUtility.ConstructEvent(
                json,
                signatureHeader,
                _stripeSettings.WebhookSecret
            );
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Invalid Stripe webhook signature.");
            return BadRequest("Invalid webhook signature.");
        }

        if (stripeEvent.Type == "payment_intent.succeeded")
        {
            var paymentIntent = stripeEvent.Data.Object as PaymentIntent;

            if (paymentIntent != null)
            {
                var order = await _context.Orders
                    .FirstOrDefaultAsync(o => o.PaymentIntentId == paymentIntent.Id);

                if (order != null)
                {
                    // Basic idempotency: ignore repeated processing of the same Stripe event
                    if (order.ProcessedWebhookEventId == stripeEvent.Id)
                    {
                        return Ok();
                    }

                    order.Status = "Paid";
                    order.PaidAt = DateTime.UtcNow;
                    order.ProcessedWebhookEventId = stripeEvent.Id;

                    _metrics.Increment("payments.webhook.succeeded");
                    await _context.SaveChangesAsync();
                }
            }
        }

        return Ok();
    }
}
