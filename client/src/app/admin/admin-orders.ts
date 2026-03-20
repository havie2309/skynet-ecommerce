import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { Order } from '../models/order';
import { OrderService } from '../core/services/order';

@Component({
  selector: 'app-admin-orders',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './admin-orders.html',
  styleUrl: './admin-orders.scss'
})

export class AdminOrders implements OnInit {
  orders: Order[] = [];
  loading = false;
  error = '';

  statuses = ['Pending', 'Paid', 'Shipped', 'Delivered', 'Cancelled'];

  constructor(private orderService: OrderService) {}

  ngOnInit(): void {
    this.loadOrders();
  }

  loadOrders(): void {
    this.loading = true;
    this.error = '';

    this.orderService.getAllOrders().subscribe({
      next: (orders: Order[]) => {
        this.orders = orders;
        this.loading = false;
      },
      error: () => {
        this.error = 'Failed to load orders';
        this.loading = false;
      }
    });
  }

  updateStatus(orderId: number, status: string): void {
    this.orderService.updateOrderStatus(orderId, status).subscribe({
      next: updated => {
        this.orders = this.orders.map(order =>
          order.id === updated.id ? updated : order
        );
      },
      error: () => {
        this.error = 'Failed to update order status';
      }
    });
  }
}
