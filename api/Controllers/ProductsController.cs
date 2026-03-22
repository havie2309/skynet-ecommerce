using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Skinet.Api.Data;
using Skinet.Api.DTOs;
using Skinet.Api.Models;
using Skinet.Api.Extensions;


namespace Skinet.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly AppDbContext _context;

    public ProductsController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<object>> GetProducts(
        [FromQuery] string? search,
        [FromQuery] string? brand,
        [FromQuery] string? category,
        [FromQuery] decimal? minPrice,
        [FromQuery] decimal? maxPrice,
        [FromQuery] string? sort,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10)
    {
        var query = _context.Products.AsQueryable();

        if (!string.IsNullOrWhiteSpace(search))
            query = query.Where(p => p.Name.ToLower().Contains(search.ToLower()));

        if (!string.IsNullOrWhiteSpace(brand))
            query = query.Where(p => p.Brand == brand);

        if (!string.IsNullOrWhiteSpace(category))
            query = query.Where(p => p.Category == category);

        if (minPrice.HasValue)
            query = query.Where(p => p.Price >= minPrice.Value);

        if (maxPrice.HasValue)
            query = query.Where(p => p.Price <= maxPrice.Value);

        var totalCount = await query.CountAsync();

        query = sort?.Trim() switch
        {
            "priceAsc" => query.OrderBy(p => p.Price),
            "priceDesc" => query.OrderByDescending(p => p.Price),
            "name" => query.OrderBy(p => p.Name),
            _ => query.OrderBy(p => p.Id)  
        };

        var products = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(p => new ProductDto(p.Id, p.Name, p.Description, p.Price, p.StockQuantity, p.ImageUrl))
            .ToListAsync();

        return Ok(new
        {
            TotalCount = totalCount,
            Page = page,
            PageSize = pageSize,
            Data = products
        });
    }

    [HttpGet("filters")]
    public async Task<ActionResult<object>> GetFilters()
    {
        var brands = await _context.Products
            .Select(p => p.Brand)
            .Where(b => !string.IsNullOrWhiteSpace(b))
            .Distinct()
            .OrderBy(b => b)
            .ToListAsync();

        var categories = await _context.Products
            .Select(p => p.Category)
            .Where(c => !string.IsNullOrWhiteSpace(c))
            .Distinct()
            .OrderBy(c => c)
            .ToListAsync();

        return Ok(new
        {
            Brands = brands,
            Categories = categories
        });
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductDto>> GetProduct(int id)
    {
        var product = await _context.Products
            .Where(p => p.Id == id)
            .Select(p => new ProductDto(p.Id, p.Name, p.Description, p.Price, p.StockQuantity, p.ImageUrl))
            .FirstOrDefaultAsync();

        if (product == null) return NotFound();

        return Ok(product);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<ActionResult<ProductDto>> CreateProduct(ProductDto dto)
    {
        var product = new Product
        {
            Name = dto.Name,
            Description = dto.Description,
            Price = dto.Price,
            StockQuantity = dto.StockQuantity,
            ImageUrl = dto.ImageUrl
        };

        _context.Products.Add(product);
        await _context.SaveChangesAsync();

        var result = new ProductDto(
            product.Id,
            product.Name,
            product.Description,
            product.Price,
            product.StockQuantity,
            product.ImageUrl
        );

        return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, result);
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("{id}")]
    public async Task<ActionResult<ProductDto>> UpdateProduct(int id, ProductDto dto)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null) 
            return this.ApiError(StatusCodes.Status404NotFound, $"Product {id} was not found.");


        product.Name = dto.Name;
        product.Description = dto.Description;
        product.Price = dto.Price;
        product.StockQuantity = dto.StockQuantity;
        product.ImageUrl = dto.ImageUrl;

        await _context.SaveChangesAsync();

        var result = new ProductDto(
            product.Id,
            product.Name,
            product.Description,
            product.Price,
            product.StockQuantity,
            product.ImageUrl
        );

        return Ok(result);
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteProduct(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null)
            return this.ApiError(StatusCodes.Status404NotFound, $"Product {id} was not found.");


        _context.Products.Remove(product);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
