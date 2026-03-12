using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Skinet.Api.Data;
using Skinet.Api.DTOs;
using Skinet.Api.Models;
using Skinet.Api.Services;

namespace Skinet.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class OrdersController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly BasketRepository _basketRepo;

    public OrdersController(AppDbContext context, BasketRepository basketRepo)
    {
        _context = context;
        _basketRepo = basketRepo;
    }

    [HttpPost]
    public async Task<ActionResult<OrderResponseDto>> PlaceOrder(PlaceOrderDto dto)
    {
        // 1. Get logged-in user from JWT
        var email = User.FindFirstValue(ClaimTypes.Email);
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        if (user == null) return Unauthorized();

        // 2. Create order items
        var orderItems = dto.Items.Select(i => new OrderItem
        {
            ProductId = i.ProductId,
            PriceAtPurchase = i.Price,
            Quantity = i.Quantity
        }).ToList();

        // 3. Create order
        var order = new Order
        {
            UserId = user.Id,
            CreatedAt = DateTime.UtcNow,
            OrderItems = orderItems
        };

        // 4. Save to PostgreSQL
        _context.Orders.Add(order);
        await _context.SaveChangesAsync();

        // 5. Clear Redis basket
        await _basketRepo.DeleteBasketAsync(dto.BasketId);

        // 6. Return response
        var total = orderItems.Sum(i => i.PriceAtPurchase * i.Quantity);
        return Ok(new OrderResponseDto(
            order.Id,
            "Pending",
            total,
            order.CreatedAt,
            dto.Items
        ));
    }

    [HttpGet]
    public async Task<ActionResult<List<OrderResponseDto>>> GetOrders()
    {
        var email = User.FindFirstValue(ClaimTypes.Email);
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        if (user == null) return Unauthorized();

        var orders = await _context.Orders
            .Include(o => o.OrderItems)
            .Where(o => o.UserId == user.Id)
            .OrderByDescending(o => o.CreatedAt)
            .ToListAsync();

        return Ok(orders.Select(o => new OrderResponseDto(
            o.Id,
            "Pending",
            o.OrderItems.Sum(i => i.PriceAtPurchase * i.Quantity),
            o.CreatedAt,
            o.OrderItems.Select(i => new OrderItemDto(
                i.ProductId, "", i.PriceAtPurchase, i.Quantity)).ToList()
        )));
    }
}