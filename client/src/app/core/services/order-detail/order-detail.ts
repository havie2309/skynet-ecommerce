import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { Order, OrderItem } from '../../../models/order';
import { OrderService } from '../order';

@Component({
  selector: 'app-order-detail',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './order-detail.html'
})
export class OrderDetail implements OnInit {
  order: Order | null = null;
  loading = false;
  error = '';

  constructor(
    private route: ActivatedRoute,
    private orderService: OrderService
  ) {}

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');
    if (!id) return;

    this.loading = true;
    this.error = '';

    this.orderService.getOrderById(+id).subscribe({
      next: (order: Order) => {
        this.order = order;
        this.loading = false;
      },
      error: () => {
        this.error = 'Failed to load order';
        this.loading = false;
      }
    });
  }

  get total(): number {
    return this.order?.items.reduce((sum: number, item: OrderItem) => sum + item.price * item.quantity, 0) ?? 0;
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
