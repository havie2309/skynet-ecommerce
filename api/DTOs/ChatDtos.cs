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

// OpenAI response shape
public record OpenAiMessage(
    [property: JsonPropertyName("content")] string Content
);

public record OpenAiChoice(
    [property: JsonPropertyName("message")] OpenAiMessage Message
);

public record OpenAiResponse(
    [property: JsonPropertyName("choices")] List<OpenAiChoice> Choices
);
