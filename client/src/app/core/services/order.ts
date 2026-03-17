import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { Order, PlaceOrderDto } from '../../models/order';

@Injectable({ providedIn: 'root' })
export class OrderService {
  private baseUrl = environment.apiUrl + 'orders';

  constructor(private http: HttpClient) {}

  placeOrder(dto: PlaceOrderDto): Observable<Order> {
    return this.http.post<Order>(this.baseUrl, dto);
  }

  getOrders(): Observable<Order[]> {
    return this.http.get<Order[]>(this.baseUrl);
  }

  getOrderById(id: number): Observable<Order> {   
    return this.http.get<Order>(`${this.baseUrl}/${id}`);
  }
}