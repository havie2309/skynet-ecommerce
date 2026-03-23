namespace Skinet.Api.DTOs;

public record CreatePaymentIntentDto(string BasketId);

public record PaymentIntentResponseDto(string ClientSecret, string PaymentIntentId);
