export interface OrderItem {
  productId: number;
  productName: string;
  price: number;
  quantity: number;
}

export interface PlaceOrderDto {
  basketId: string;
  items: OrderItem[];
}

export interface Order {
  id: number;
  status: string;
  total: number;
  createdAt: string;
  items: OrderItem[];
}