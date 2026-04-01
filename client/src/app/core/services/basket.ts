import { Injectable, signal, computed } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment';
import { Basket, BasketItem } from '../../models/basket';
import { Product } from '../../models/product';
import { ToastService } from './toast';

@Injectable({ providedIn: 'root' })
export class BasketService {
  private baseUrl = environment.apiUrl + 'basket';
  basket = signal<Basket | null>(null);
  itemCount = computed(() =>
    this.basket()?.items.reduce((sum, i) => sum + i.quantity, 0) ?? 0
  );

  constructor(private http: HttpClient, private toast: ToastService) {}

  getBasket(id: string) {
    this.http.get<Basket>(`${this.baseUrl}?id=${id}`).subscribe({
      next: b => {
        if (b && b.items?.length > 0) this.basket.set(b);
      },
      error: () => {}
    });
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
    const updated = { ...basket, items: this.addOrUpdateItem([...basket.items], item) };
    this.basket.set(updated);
    this.saveBasket(updated);
    this.toast.success(`${product.name} added to basket`);
  }

  incrementItem(productId: number) {
    const basket = this.basket();
    if (!basket) return;
    const updated = {
      ...basket,
      items: basket.items.map(i =>
        i.productId === productId ? { ...i, quantity: i.quantity + 1 } : i
      )
    };
    this.basket.set(updated);
    this.saveBasket(updated);
  }

  decrementItem(productId: number) {
    const basket = this.basket();
    if (!basket) return;
    const item = basket.items.find(i => i.productId === productId);
    if (!item) return;
    if (item.quantity === 1) {
      this.removeItem(productId);
      return;
    }
    const updated = {
      ...basket,
      items: basket.items.map(i =>
        i.productId === productId ? { ...i, quantity: i.quantity - 1 } : i
      )
    };
    this.basket.set(updated);
    this.saveBasket(updated);
  }

  removeItem(productId: number) {
    const basket = this.basket();
    if (!basket) return;
    const items = basket.items.filter(i => i.productId !== productId);
    if (items.length === 0) {
      this.deleteBasket(basket.id);
      return;
    }
    const updated = { ...basket, items };
    this.basket.set(updated);
    this.saveBasket(updated);
  }

  deleteBasket(id: string) {
    this.basket.set(null);
    localStorage.removeItem('basket_id');
    this.http.delete(`${this.baseUrl}?id=${id}`).subscribe({ error: () => {} });
  }

  private saveBasket(basket: Basket) {
    this.http.post<Basket>(this.baseUrl, basket).subscribe({ error: () => {} });
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
