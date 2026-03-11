import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { BasketService } from '../core/services/basket';

@Component({
  selector: 'app-basket',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './basket.html',
  styleUrl: './basket.scss'
})
export class Basket implements OnInit {
  constructor(public basketService: BasketService, private cdr: ChangeDetectorRef) {}

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
}