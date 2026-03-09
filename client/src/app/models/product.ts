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