import { Injectable, signal, computed } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment';
import { Basket, BasketItem } from '../../models/basket';
import { Product } from '../../models/product';

@Injectable({ providedIn: 'root' })
export class BasketService {
  private baseUrl = environment.apiUrl + 'basket';
  basket = signal<Basket | null>(null);
  itemCount = computed(() =>
    this.basket()?.items.reduce((sum, i) => sum + i.quantity, 0) ?? 0
  );

  constructor(private http: HttpClient) {}

  getBasket(id: string) {
    this.http.get<Basket>(`${this.baseUrl}?id=${id}`)
      .subscribe(b => this.basket.set(b));
  }

  addItemToBasket(product: Product, quantity = 1) {
    const item: BasketItem = {
      productId: product.id,
      productName: product.name,
      price: product.price,
      quantity,
      imageUrl: product.imageUrl
    };
    const basket = this.basket() ?? this.createBasket();
    basket.items = this.addOrUpdateItem(basket.items, item);
    this.setBasket(basket);
  }

  setBasket(basket: Basket) {
    this.http.post<Basket>(this.baseUrl, basket)
      .subscribe(b => this.basket.set(b));
  }

  removeItem(productId: number) {
    const basket = this.basket();
    if (!basket) return;
    basket.items = basket.items.filter(i => i.productId !== productId);
    basket.items.length ? this.setBasket(basket) : this.deleteBasket(basket.id);
  }

  deleteBasket(id: string) {
    this.http.delete(`${this.baseUrl}?id=${id}`).subscribe(() => {
      this.basket.set(null);
      localStorage.removeItem('basket_id');
    });
  }

  private createBasket(): Basket {
    const id = crypto.randomUUID();
    localStorage.setItem('basket_id', id);
    return { id, items: [] };
  }

  private addOrUpdateItem(items: BasketItem[], item: BasketItem): BasketItem[] {
    const existing = items.find(i => i.productId === item.productId);
    existing ? existing.quantity += item.quantity : items.push(item);
    return items;
  }
}