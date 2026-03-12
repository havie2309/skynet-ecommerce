import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Router } from '@angular/router';
import { BasketService } from '../core/services/basket';
import { OrderService } from '../core/services/order';
import { PlaceOrderDto } from '../models/order';

@Component({
  selector: 'app-basket',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './basket.html',
  styleUrl: './basket.scss'
})
export class Basket implements OnInit {
  isPlacingOrder = false;

  constructor(
    public basketService: BasketService,
    private orderService: OrderService,
    private router: Router,
    private cdr: ChangeDetectorRef
  ) {}

  ngOnInit() {
    const id = localStorage.getItem('basket_id');
    if (id) {
      this.basketService.getBasket(id);
      setTimeout(() => this.cdr.detectChanges(), 300);
    }
  }

  removeItem(productId: number) {
    this.basketService.removeItem(productId);
    this.cdr.detectChanges();
  }

  get total() {
    return this.basketService.basket()?.items
      .reduce((sum, i) => sum + i.price * i.quantity, 0) ?? 0;
  }

  placeOrder() {
    const basket = this.basketService.basket();
    if (!basket) return;

    const dto: PlaceOrderDto = {
      basketId: basket.id,
      items: basket.items.map(i => ({
        productId: i.productId,
        productName: i.productName,
        price: i.price,
        quantity: i.quantity
      }))
    };

    this.isPlacingOrder = true;
    this.orderService.placeOrder(dto).subscribe({
      next: (order) => {
        this.basketService.basket.set(null);
        localStorage.removeItem('basket_id');
        this.router.navigate(['/order-confirmation', order.id]);
      },
      error: (err) => {
        console.error('Order failed:', err);
        this.isPlacingOrder = false;
      }
    });
  }
}