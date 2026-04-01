import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { Product } from '../../models/product';

export interface ChatSuggestion {
  productId: number;
  reason: string;
  product: Product;
}

export interface ChatResponse {
  message: string;
  tone: string;
  suggestions: ChatSuggestion[];
}

export interface ChatMessage {
  role: 'user' | 'assistant';
  text?: string;
  response?: ChatResponse;
  loading?: boolean;
}

@Injectable({ providedIn: 'root' })
export class ChatService {
  private baseUrl = environment.apiUrl + 'chat';

  constructor(private http: HttpClient) {}

  send(message: string): Observable<ChatResponse> {
    return this.http.post<ChatResponse>(this.baseUrl, { message });
  }
}
