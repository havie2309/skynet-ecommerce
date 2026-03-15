import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { Product, ProductFilters, ProductQueryParams } from '../../models/product';
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

  filters: ProductFilters = {
    brands: [],
    categories: []
  };

  queryParams: ProductQueryParams = {
    pageIndex: 1,
    pageSize: 8,
    search: '',
    brand: '',
    category: '',
    minPrice: null,
    maxPrice: null,
    sort: ''
  };

  constructor(private productService: ProductService) {}

  ngOnInit() {
    this.loadFilters();
    this.loadProducts();
  }

  loadFilters() {
    this.productService.getFilters().subscribe({
      next: res => {
        this.filters = res;
      },
      error: err => console.error('Filter API error:', err)
    });
  }

  loadProducts() {
    this.productService.getProducts(this.queryParams)
      .subscribe({
        next: res => {
          this.products = [...res.data];
          this.totalCount = res.totalCount;
        },
        error: err => console.error('API error:', err)
      });
  }

  onSearch(term: string) {
    this.queryParams.search = term;
    this.queryParams.pageIndex = 1;
    this.loadProducts();
  }

  onSort(val: string) {
    this.queryParams.sort = val;
    this.queryParams.pageIndex = 1;
    this.loadProducts();
  }

  onBrandChange(val: string) {
    this.queryParams.brand = val;
    this.queryParams.pageIndex = 1;
    this.loadProducts();
  }

  onCategoryChange(val: string) {
    this.queryParams.category = val;
    this.queryParams.pageIndex = 1;
    this.loadProducts();
  }

  onMinPriceChange(val: string) {
    this.queryParams.minPrice = val ? Number(val) : null;
    this.queryParams.pageIndex = 1;
    this.loadProducts();
  }

  onMaxPriceChange(val: string) {
    this.queryParams.maxPrice = val ? Number(val) : null;
    this.queryParams.pageIndex = 1;
    this.loadProducts();
  }

  onPageChange(p: number) {
    this.queryParams.pageIndex = p;
    this.loadProducts();
  }

  clearFilters() {
    this.queryParams = {
      ...this.queryParams,
      pageIndex: 1,
      search: '',
      brand: '',
      category: '',
      minPrice: null,
      maxPrice: null,
      sort: ''
    };
    this.loadProducts();
  }
}
