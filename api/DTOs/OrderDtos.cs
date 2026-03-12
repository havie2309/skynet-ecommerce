namespace Skinet.Api.DTOs;

public record OrderItemDto(int ProductId, string ProductName, decimal Price, int Quantity);

public record PlaceOrderDto(string BasketId, List<OrderItemDto> Items);

public record OrderResponseDto(
    int Id,
    string Status,
    decimal Total,
    DateTime CreatedAt,
    List<OrderItemDto> Items
);