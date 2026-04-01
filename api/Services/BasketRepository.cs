using System.Text.Json;
using StackExchange.Redis;
using Skinet.Api.Models;

namespace Skinet.Api.Services;

public class BasketRepository
{
    private readonly IDatabase _db;
    private readonly ILogger<BasketRepository> _logger;

    public BasketRepository(IConnectionMultiplexer redis, ILogger<BasketRepository> logger)
    {
        _db = redis.GetDatabase();
        _logger = logger;
    }

    public async Task<CustomerBasket?> GetBasketAsync(string id)
    {
        try
        {
            var data = await _db.StringGetAsync(id);
            return data.IsNullOrEmpty
                ? null
                : JsonSerializer.Deserialize<CustomerBasket>(data!);
        }
        catch (Exception ex) { _logger.LogError(ex, "Redis GetBasket failed for {Id}", id); return null; }
    }

    public async Task<CustomerBasket?> UpdateBasketAsync(CustomerBasket basket)
    {
        try
        {
            var created = await _db.StringSetAsync(
                basket.Id,
                JsonSerializer.Serialize(basket),
                TimeSpan.FromDays(30));
            return created ? basket : null;
        }
        catch (Exception ex) { _logger.LogError(ex, "Redis UpdateBasket failed for {Id}", basket.Id); return null; }
    }

    public async Task<bool> DeleteBasketAsync(string id)
    {
        try { return await _db.KeyDeleteAsync(id); }
        catch (Exception ex) { _logger.LogError(ex, "Redis DeleteBasket failed for {Id}", id); return false; }
    }
}