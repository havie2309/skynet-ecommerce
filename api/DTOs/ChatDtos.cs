using System.Text.Json.Serialization;

namespace Skinet.Api.DTOs;

public record ChatRequest(string Message);

public record ChatSuggestion(
    [property: JsonPropertyName("productId")] int ProductId,
    [property: JsonPropertyName("reason")]    string Reason
);

public record ChatAiResponse(
    [property: JsonPropertyName("message")]     string Message,
    [property: JsonPropertyName("tone")]        string Tone,
    [property: JsonPropertyName("suggestions")] List<ChatSuggestion> Suggestions
);

// Gemini response shape
public record GeminiPart(
    [property: JsonPropertyName("text")] string Text
);

public record GeminiContent(
    [property: JsonPropertyName("parts")] List<GeminiPart> Parts
);

public record GeminiCandidate(
    [property: JsonPropertyName("content")] GeminiContent Content
);

public record GeminiResponse(
    [property: JsonPropertyName("candidates")] List<GeminiCandidate> Candidates
);
