import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { Product } from '../../models/product';
import { BasketService } from '../../core/services/basket';

@Component({
  selector: 'app-product-card',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './product-card.html',
  styleUrl: './product-card.scss'
})
export class ProductCard {
  @Input() product!: Product;
  added = false;

  constructor(private basketService: BasketService) {}

  addToBasket(event: Event) {
    event.preventDefault();
    event.stopPropagation();
    this.basketService.addItemToBasket(this.product);
    this.added = true;
    setTimeout(() => this.added = false, 2000);
  }
}
