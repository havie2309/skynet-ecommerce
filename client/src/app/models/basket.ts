export interface BasketItem {
  productId: number;
  productName: string;
  price: number;
  quantity: number;
  imageUrl: string | null;
}

export interface Basket {
  id: string;
  items: BasketItem[];
}