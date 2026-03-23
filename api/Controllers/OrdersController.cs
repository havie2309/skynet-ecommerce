using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Skinet.Api.Data;
using Skinet.Api.DTOs;
using Skinet.Api.Models;
using Skinet.Api.Services;
using Skinet.Api.Extensions;


namespace Skinet.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class OrdersController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly BasketRepository _basketRepo;
    private readonly IEmailService _emailService;

    public OrdersController(AppDbContext context, BasketRepository basketRepo, IEmailService emailService)
    {
        _context = context;
        _basketRepo = basketRepo;
        _emailService = emailService;
    }

    [HttpPost]
    public async Task<ActionResult<OrderResponseDto>> PlaceOrder(PlaceOrderDto dto)
    {
        var email = User.FindFirstValue(ClaimTypes.Email);
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        if (user == null)
            return this.ApiError(StatusCodes.Status401Unauthorized, "User account was not found.");

        var orderItems = dto.Items.Select(i => new OrderItem
        {
            ProductId = i.ProductId,
            PriceAtPurchase = i.Price,
            Quantity = i.Quantity
        }).ToList();

        var order = new Order
        {
            UserId = user.Id,
            CreatedAt = DateTime.UtcNow,
            Status = "Pending",
            PaymentIntentId = dto.PaymentIntentId,
            OrderItems = orderItems
        };
        
        _context.Orders.Add(order);
        await _context.SaveChangesAsync();

        try
        {
            await _emailService.SendOrderConfirmationAsync(user.Email, order.Id);
        }
        catch
        {
            // Email failure should not block order creation.
        }

        await _basketRepo.DeleteBasketAsync(dto.BasketId);

        var total = orderItems.Sum(i => i.PriceAtPurchase * i.Quantity);

        return Ok(new OrderResponseDto(
            order.Id,
            order.Status,
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
            o.Status,
            o.OrderItems.Sum(i => i.PriceAtPurchase * i.Quantity),
            o.CreatedAt,
            o.OrderItems.Select(i => new OrderItemDto(
                i.ProductId, "", i.PriceAtPurchase, i.Quantity)).ToList()
        )));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<OrderResponseDto>> GetOrderById(int id)
    {
        var email = User.FindFirstValue(ClaimTypes.Email);
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        if (user == null) return Unauthorized();

        var order = await _context.Orders
            .Include(o => o.OrderItems)
            .FirstOrDefaultAsync(o => o.Id == id && o.UserId == user.Id);

        if (order == null)
            return this.ApiError(StatusCodes.Status404NotFound, $"Order {id} was not found.");


        var total = order.OrderItems.Sum(i => i.PriceAtPurchase * i.Quantity);

        return Ok(new OrderResponseDto(
            order.Id,
            order.Status,
            total,
            order.CreatedAt,
            order.OrderItems.Select(i => new OrderItemDto(
                i.ProductId,
                "",
                i.PriceAtPurchase,
                i.Quantity
            )).ToList()
        ));
    }

    [Authorize(Roles = "Admin")]
    [HttpGet("admin")]
    public async Task<ActionResult<List<OrderResponseDto>>> GetAllOrders()
    {
        var orders = await _context.Orders
            .Include(o => o.OrderItems)
            .OrderByDescending(o => o.CreatedAt)
            .ToListAsync();

        return Ok(orders.Select(o => new OrderResponseDto(
            o.Id,
            o.Status,
            o.OrderItems.Sum(i => i.PriceAtPurchase * i.Quantity),
            o.CreatedAt,
            o.OrderItems.Select(i => new OrderItemDto(
                i.ProductId,
                "",
                i.PriceAtPurchase,
                i.Quantity
            )).ToList()
        )));
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("admin/{id}/status")]
    public async Task<ActionResult<OrderResponseDto>> UpdateOrderStatus(int id, UpdateOrderStatusDto dto)
    {
        var order = await _context.Orders
            .Include(o => o.OrderItems)
            .FirstOrDefaultAsync(o => o.Id == id);

        if (order == null) return NotFound();

        order.Status = dto.Status;
        await _context.SaveChangesAsync();

        return Ok(new OrderResponseDto(
            order.Id,
            order.Status,
            order.OrderItems.Sum(i => i.PriceAtPurchase * i.Quantity),
            order.CreatedAt,
            order.OrderItems.Select(i => new OrderItemDto(
                i.ProductId,
                "",
                i.PriceAtPurchase,
                i.Quantity
            )).ToList()
        ));
    }
}
