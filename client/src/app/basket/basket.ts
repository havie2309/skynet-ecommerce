import { Component, OnInit } from '@angular/core';
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
  constructor(public basketService: BasketService) {}

  ngOnInit() {
    if (!this.basketService.basket()) {
      const id = localStorage.getItem('basket_id');
      if (id) this.basketService.getBasket(id);
    }
  }

  get total() {
    return this.basketService.basket()?.items
      .reduce((sum, i) => sum + i.price * i.quantity, 0) ?? 0;
  }
}
