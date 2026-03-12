using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using Stripe;
using System.Text.Json;
using Skinet.Api.Data;
using Skinet.Api.DTOs;
using Skinet.Api.Models;
using Microsoft.Extensions.Options;
using Skinet.Api.Services;


namespace Skinet.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class PaymentsController : ControllerBase
{
    private readonly IConnectionMultiplexer _redis;
    private readonly AppDbContext _context;
    private readonly StripeSettings _stripeSettings;

    public PaymentsController(
        IConnectionMultiplexer redis,
        AppDbContext context,
        IOptions<StripeSettings> stripeOptions)
    {
        _redis = redis;
        _context = context;
        _stripeSettings = stripeOptions.Value;
    }

    [HttpPost("create-payment-intent")]
    public async Task<ActionResult<PaymentIntentResponseDto>> CreatePaymentIntent(CreatePaymentIntentDto dto)
    {
        var db = _redis.GetDatabase();
        var basketJson = await db.StringGetAsync(dto.BasketId);

        if (basketJson.IsNullOrEmpty)
            return BadRequest("Basket not found");

        var basket = JsonSerializer.Deserialize<CustomerBasket>(basketJson!);

        if (basket == null || basket.Items.Count == 0)
            return BadRequest("Basket is empty");

        decimal total = 0;

        foreach (var item in basket.Items)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == item.ProductId);

            if (product == null)
                return BadRequest($"Product with id {item.ProductId} not found");

            total += product.Price * item.Quantity;
        }

        var service = new PaymentIntentService();
        var options = new PaymentIntentCreateOptions
        {
            Amount = (long)(total * 100),
            Currency = "usd",
            PaymentMethodTypes = new List<string> { "card" }
        };

        var paymentIntent = await service.CreateAsync(options);

        return Ok(new PaymentIntentResponseDto(paymentIntent.ClientSecret));
    }
    
    [HttpGet("publishable-key")]
    [AllowAnonymous]
    public ActionResult GetPublishableKey()
    {
        return Ok(new { publishableKey = _stripeSettings.PublishableKey });
    }

}
