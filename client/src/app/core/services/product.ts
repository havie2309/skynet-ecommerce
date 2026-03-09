import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { Product, Pagination } from '../../models/product';

@Injectable({ providedIn: 'root' })
export class ProductService {
  private baseUrl = environment.apiUrl + 'products';

  constructor(private http: HttpClient) {}

  getProducts(pageIndex = 1, pageSize = 8, search = '', sort = ''): Observable<Pagination<Product>> {
    let params = new HttpParams()
      .set('pageIndex', pageIndex)
      .set('pageSize', pageSize);
    if (search) params = params.set('search', search);
    if (sort)   params = params.set('sort', sort);
    return this.http.get<Pagination<Product>>(this.baseUrl, { params });
  }

  getProduct(id: number): Observable<Product> {
    return this.http.get<Product>(`${this.baseUrl}/${id}`);
  }
}