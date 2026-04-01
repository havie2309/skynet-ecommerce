import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
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
  loading = false;
  skeletons = Array(8);

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

  constructor(
    private productService: ProductService,
    private route: ActivatedRoute,
    private router: Router
  ) {}

  ngOnInit() {
    this.loadFilters();

    this.route.queryParams.subscribe(params => {
      this.queryParams = {
        pageIndex: params['page'] ? +params['page'] : 1,
        pageSize: params['pageSize'] ? +params['pageSize'] : 8,
        search: params['search'] ?? '',
        brand: params['brand'] ?? '',
        category: params['category'] ?? '',
        minPrice: params['minPrice'] ? +params['minPrice'] : null,
        maxPrice: params['maxPrice'] ? +params['maxPrice'] : null,
        sort: params['sort'] ?? ''
      };

      this.loadProducts();
    });
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
    this.loading = true;
    this.productService.getProducts(this.queryParams).subscribe({
      next: res => {
        this.products = [...res.data];
        this.totalCount = res.totalCount;
        this.loading = false;
      },
      error: err => {
        console.error('API error:', err);
        this.loading = false;
      }
    });
  }

  runSearch() {
    this.queryParams.pageIndex = 1;
    this.updateUrl();
  }

  updateUrl(): void {
    this.router.navigate([], {
      relativeTo: this.route,
      queryParams: {
        page: this.queryParams.pageIndex,
        pageSize: this.queryParams.pageSize,
        search: this.queryParams.search || null,
        brand: this.queryParams.brand || null,
        category: this.queryParams.category || null,
        minPrice: this.queryParams.minPrice,
        maxPrice: this.queryParams.maxPrice,
        sort: this.queryParams.sort || null
      },
      queryParamsHandling: 'merge'
    });
  }

  onSearch(term: string) {
    this.queryParams.search = term;
    this.queryParams.pageIndex = 1;
    this.updateUrl();
  }

  onSort(val: string) {
    console.log('Sort value:', val);
    this.queryParams.sort = val;
    this.queryParams.pageIndex = 1;
    this.updateUrl();
  }

  onBrandChange(val: string) {
    this.queryParams.brand = val;
    this.queryParams.pageIndex = 1;
    this.updateUrl();
  }

  onCategoryChange(val: string) {
    this.queryParams.category = val;
    this.queryParams.pageIndex = 1;
    this.updateUrl();
  }

  onMinPriceChange(val: string) {
    this.queryParams.minPrice = val ? Number(val) : null;
    this.queryParams.pageIndex = 1;
    this.updateUrl();
  }

  onMaxPriceChange(val: string) {
    this.queryParams.maxPrice = val ? Number(val) : null;
    this.queryParams.pageIndex = 1;
    this.updateUrl();
  }

  onPageChange(p: number) {
    this.queryParams.pageIndex = p;
    this.updateUrl();
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
    this.updateUrl();
  }
}
