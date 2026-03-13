export interface CreatePaymentIntentDto {
  basketId: string;
}

export interface PaymentIntentResponse {
  clientSecret: string;
}

export interface PublishableKeyResponse {
  publishableKey: string;
}
