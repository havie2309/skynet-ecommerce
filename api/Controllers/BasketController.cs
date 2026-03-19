using Microsoft.AspNetCore.Mvc;
using Skinet.Api.Models;
using Skinet.Api.Services;
using Skinet.Api.Extensions;


namespace Skinet.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BasketController : ControllerBase
{
    private readonly BasketRepository _repo;

    public BasketController(BasketRepository repo)
    {
        _repo = repo;
    }

    [HttpGet]
    public async Task<ActionResult<CustomerBasket>> GetBasket(string id)
    {
        var basket = await _repo.GetBasketAsync(id) 
            ?? new CustomerBasket { Id = id };
        return Ok(basket);
    }

    [HttpPost]
    public async Task<ActionResult<CustomerBasket>> UpdateBasket(CustomerBasket basket)
    {
        var updated = await _repo.UpdateBasketAsync(basket);
        if (updated == null) return this.ApiError(StatusCodes.Status400BadRequest, "Problem updating basket.");
        return Ok(updated);
    }

    [HttpDelete]
    public async Task<ActionResult> DeleteBasket(string id)
    {
        await _repo.DeleteBasketAsync(id);
        return Ok();
    }
}