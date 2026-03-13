import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { Product } from '../../models/product';
import { ProductService } from '../../core/services/product';
import { ProductCard } from '../product-card/product-card';

@Component({
  selector: 'app-product-list',
  standalone: true,
  imports: [CommonModule, RouterModule, ProductCard],
  templateUrl: './product-list.html',
  styleUrl: './product-list.scss'
})
export class ProductList implements OnInit {
  products: Product[] = [];
  totalCount = 0;
  pageIndex = 1;
  pageSize = 8;
  search = '';
  sort = '';

  constructor(private productService: ProductService) {}

  ngOnInit() {
    this.loadProducts();
  }

  loadProducts() {
    this.productService.getProducts(this.pageIndex, this.pageSize, this.search, this.sort)
      .subscribe({
        next: res => {
          this.products = [...res.data];
          this.totalCount = res.totalCount;
        },
        error: err => console.error('API error:', err)
      });
  }

  onSearch(term: string) { this.search = term; this.pageIndex = 1; this.loadProducts(); }
  onSort(val: string)    { this.sort = val;    this.pageIndex = 1; this.loadProducts(); }
  onPageChange(p: number) { this.pageIndex = p; this.loadProducts(); }
}