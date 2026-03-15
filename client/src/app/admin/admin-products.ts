import { CommonModule } from '@angular/common';
import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { RouterModule } from '@angular/router';
import { ProductService } from '../core/services/product';
import { Product, ProductQueryParams } from '../models/product';

@Component({
  selector: 'app-admin-products',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './admin-products.html'
})
export class AdminProducts implements OnInit {
  products: Product[] = [];
  loading = false;
  error = '';

  queryParams: ProductQueryParams = {
    pageIndex: 1,
    pageSize: 100,
    search: '',
    brand: '',
    category: '',
    minPrice: null,
    maxPrice: null,
    sort: ''
  };

  constructor(
    private productService: ProductService,
    private cdr: ChangeDetectorRef
  ) {}

  ngOnInit(): void {
    this.loadProducts();
  }

  loadProducts(): void {
    this.loading = true;
    this.error = '';
    this.cdr.detectChanges();

    this.productService.getProducts(this.queryParams).subscribe({
      next: response => {
        this.products = response.data;
        this.loading = false;
        this.cdr.detectChanges();
      },
      error: () => {
        this.error = 'Failed to load products';
        this.loading = false;
        this.cdr.detectChanges();
      }
    });
  }

  deleteProduct(id: number): void {
    if (!confirm('Delete this product?')) return;

    this.productService.deleteProduct(id).subscribe({
      next: () => {
        this.products = this.products.filter(p => p.id !== id);
        this.cdr.detectChanges();
      },
      error: () => {
        this.error = 'Failed to delete product';
        this.cdr.detectChanges();
      }
    });
  }
}
