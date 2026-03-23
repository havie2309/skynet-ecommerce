export interface CreatePaymentIntentDto {
  basketId: string;
}

export interface PaymentIntentResponse {
  clientSecret: string;
  paymentIntentId: string;
}

export interface PublishableKeyResponse {
  publishableKey: string;
}
