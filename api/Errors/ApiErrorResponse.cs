namespace Skinet.Api.Errors;

public record ApiErrorResponse(
    int StatusCode,
    string Message,
    string? Details = null,
    Dictionary<string, string[]>? Errors = null,
    string? TraceId = null);
