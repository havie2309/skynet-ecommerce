import { inject, Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import {
  CreatePaymentIntentDto,
  PaymentIntentResponse,
  PublishableKeyResponse
} from '../../models/payment';

@Injectable({
  providedIn: 'root'
})
export class PaymentService {
  private http = inject(HttpClient);
  private baseUrl = 'http://localhost:5283/api/payments';

  private getAuthHeaders(): HttpHeaders {
    const token = localStorage.getItem('token');
    return new HttpHeaders({ Authorization: `Bearer ${token}` });
  }

  createPaymentIntent(dto: CreatePaymentIntentDto): Observable<PaymentIntentResponse> {
    return this.http.post<PaymentIntentResponse>(
      `${this.baseUrl}/create-payment-intent`,
      dto,
      { headers: this.getAuthHeaders() }
    );
  }

  getPublishableKey(): Observable<PublishableKeyResponse> {
    return this.http.get<PublishableKeyResponse>(`${this.baseUrl}/publishable-key`);
  }
}