using Microsoft.AspNetCore.Mvc;
using Skinet.Api.Errors;

namespace Skinet.Api.Extensions;

public static class ControllerExtensions
{
    public static ObjectResult ApiError(
        this ControllerBase controller,
        int statusCode,
        string message,
        string? details = null,
        Dictionary<string, string[]>? errors = null)
    {
        return new ObjectResult(new ApiErrorResponse(
            statusCode,
            message,
            details,
            errors,
            controller.HttpContext.TraceIdentifier))
        {
            StatusCode = statusCode
        };
    }
}
    