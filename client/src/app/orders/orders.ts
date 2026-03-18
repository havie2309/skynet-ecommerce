import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { Order } from '../models/order';
import { OrderService } from '../core/services/order';

@Component({
  selector: 'app-orders',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './orders.html'
})
export class Orders implements OnInit {
  orders: Order[] = [];

  constructor(private orderService: OrderService) {}

  ngOnInit(): void {
    this.orderService.getOrders().subscribe({
      next: (orders: Order[]) => {
        this.orders = orders;
      },
      error: err => console.error('Failed to load orders', err)
    });
  }

  getStatusClass(status: string): string {
    switch (status) {
      case 'Pending':
        return 'border border-warning text-warning';
      case 'Paid':
        return 'border border-primary text-primary';
      case 'Shipped':
        return 'border border-info text-info';
      case 'Delivered':
        return 'border border-success text-success';
      case 'Cancelled':
        return 'border border-danger text-danger';
      default:
        return 'border border-secondary text-secondary';
    }
  }
}
