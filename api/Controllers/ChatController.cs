using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers;
using System.Text.Json;
using Skinet.Api.Data;
using Skinet.Api.DTOs;

namespace Skinet.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ChatController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IHttpClientFactory _httpFactory;
    private readonly IConfiguration _config;
    private readonly ILogger<ChatController> _logger;

    public ChatController(
        AppDbContext context,
        IHttpClientFactory httpFactory,
        IConfiguration config,
        ILogger<ChatController> logger)
    {
        _context = context;
        _httpFactory = httpFactory;
        _config = config;
        _logger = logger;
    }

    [HttpPost]
    public async Task<ActionResult> Chat([FromBody] ChatRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Message))
            return BadRequest("Message is required.");

        var apiKey = _config["OpenAI:ApiKey"];
        if (string.IsNullOrEmpty(apiKey))
            return StatusCode(503, new { message = "AI assistant is not configured yet." });

        var products = await _context.Products
            .Where(p => p.StockQuantity > 0)
            .Select(p => new { p.Id, p.Name, p.Description, p.Price, p.Category, p.Brand })
            .ToListAsync();

        var catalog = JsonSerializer.Serialize(products);

        var systemPrompt = $"""
            You are a warm, friendly florist assistant for Snoopy Petal — a boutique flower shop.
            Help customers find the perfect flowers based on their needs, occasion, and budget.

            Our current in-stock catalog:
            {catalog}

            Respond with a JSON object in this exact structure:
            {{
              "message": "A warm 2-3 sentence personal response explaining your suggestions",
              "tone": "one of: romantic, apology, celebration, sympathy, friendship, gratitude, birthday",
              "suggestions": [
                {{ "productId": 1, "reason": "Short reason why this fits (1 sentence)" }}
              ]
            }}

            Rules:
            - Suggest 1 to 3 products only
            - Only suggest products from the catalog above
            - Respect any budget the customer mentions
            - Match tone to the occasion
            - Be warm and personal, not generic
            - Return only valid JSON, no markdown
            """;

        try
        {
            var client = _httpFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", apiKey);

            var body = new
            {
                model = "gpt-4o-mini",
                messages = new[]
                {
                    new { role = "system", content = systemPrompt },
                    new { role = "user",   content = request.Message }
                },
                response_format = new { type = "json_object" },
                temperature = 0.75,
                max_tokens = 600
            };

            var response = await client.PostAsJsonAsync(
                "https://api.openai.com/v1/chat/completions", body);

            if (!response.IsSuccessStatusCode)
            {
                var err = await response.Content.ReadAsStringAsync();
                _logger.LogError("OpenAI error: {Error}", err);
                return StatusCode(502, new { message = "AI service temporarily unavailable." });
            }

            var openAiResult = await response.Content.ReadFromJsonAsync<OpenAiResponse>();
            var content = openAiResult?.Choices.FirstOrDefault()?.Message.Content;

            if (string.IsNullOrEmpty(content))
                return StatusCode(502, new { message = "No response from AI." });

            var aiResponse = JsonSerializer.Deserialize<ChatAiResponse>(content,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            if (aiResponse is null)
                return StatusCode(502, new { message = "Could not parse AI response." });

            var productIds = aiResponse.Suggestions.Select(s => s.ProductId).ToList();
            var suggestedProducts = await _context.Products
                .Where(p => productIds.Contains(p.Id))
                .Select(p => new ProductDto(p.Id, p.Name, p.Description, p.Price, p.StockQuantity, p.ImageUrl))
                .ToListAsync();

            return Ok(new
            {
                aiResponse.Message,
                aiResponse.Tone,
                Suggestions = aiResponse.Suggestions.Select(s => new
                {
                    s.ProductId,
                    s.Reason,
                    Product = suggestedProducts.FirstOrDefault(p => p.Id == s.ProductId)
                }).Where(s => s.Product is not null)
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Chat error");
            return StatusCode(500, new { message = "Something went wrong. Please try again." });
        }
    }
}
