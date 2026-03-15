export interface Product {
  id: number;
  name: string;
  description: string;
  price: number;
  stockQuantity: number;
  imageUrl: string | null;
}

export interface Pagination<T> {
  page: number;
  pageSize: number;
  totalCount: number;
  data: T[];
}

export interface ProductFilters {
  brands: string[];
  categories: string[];
}

export interface ProductQueryParams {
  pageIndex: number;
  pageSize: number;
  search: string;
  brand: string;
  category: string;
  minPrice: number | null;
  maxPrice: number | null;
  sort: string;
}
