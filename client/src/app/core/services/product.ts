import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { Product, Pagination, ProductFilters, ProductQueryParams } from '../../models/product';

@Injectable({ providedIn: 'root' })
export class ProductService {
  private baseUrl = environment.apiUrl + 'products';

  constructor(private http: HttpClient) {}

  getProducts(paramsObj: ProductQueryParams): Observable<Pagination<Product>> {
    let params = new HttpParams()
      .set('page', paramsObj.pageIndex)
      .set('pageSize', paramsObj.pageSize);

    if (paramsObj.search) params = params.set('search', paramsObj.search);
    if (paramsObj.brand) params = params.set('brand', paramsObj.brand);
    if (paramsObj.category) params = params.set('category', paramsObj.category);
    if (paramsObj.minPrice !== null) params = params.set('minPrice', paramsObj.minPrice);
    if (paramsObj.maxPrice !== null) params = params.set('maxPrice', paramsObj.maxPrice);
    if (paramsObj.sort) params = params.set('sort', paramsObj.sort);

    return this.http.get<Pagination<Product>>(this.baseUrl, { params });
  }

  getFilters(): Observable<ProductFilters> {
    return this.http.get<ProductFilters>(`${this.baseUrl}/filters`);
  }

  getProduct(id: number): Observable<Product> {
    return this.http.get<Product>(`${this.baseUrl}/${id}`);
  }

  createProduct(product: Product): Observable<Product> {
    return this.http.post<Product>(this.baseUrl, product);
  }

  updateProduct(id: number, product: Product): Observable<Product> {
    return this.http.put<Product>(`${this.baseUrl}/${id}`, product);
  }

  deleteProduct(id: number): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`);
  }
}
