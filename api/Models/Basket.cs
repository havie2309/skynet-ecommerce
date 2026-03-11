namespace Skinet.Api.Models;

public class BasketItem
{
    public int ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public string? ImageUrl { get; set; }
}

public class CustomerBasket
{
    public string Id { get; set; } = string.Empty;
    public List<BasketItem> Items { get; set; } = new();
}