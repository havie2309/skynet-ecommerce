export interface OrderItem {
  productId: number;
  productName: string;
  price: number;
  quantity: number;
}

export interface PlaceOrderDto {
  basketId: string;
  paymentIntentId?: string | null;
  items: OrderItem[];
}

export interface Order {
  id: number;
  status: string;
  total: number;
  createdAt: string;
  items: OrderItem[];
}