using Microsoft.AspNetCore.Mvc;
using Skinet.Api.Services;

namespace Skinet.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MetricsController : ControllerBase
{
    private readonly AppMetrics _metrics;

    public MetricsController(AppMetrics metrics)
    {
        _metrics = metrics;
    }

    [HttpGet]
    public IActionResult GetMetrics()
    {
        return Ok(_metrics.Snapshot());
    }
}
