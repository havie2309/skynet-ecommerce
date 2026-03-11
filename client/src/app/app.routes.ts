import { Routes } from '@angular/router';
import { ProductList } from './shop/product-list/product-list';
import { ProductDetail } from './shop/product-detail/product-detail';
import { Basket } from './basket/basket';

export const routes: Routes = [
  { path: '', redirectTo: 'shop', pathMatch: 'full' },
  { path: 'shop', component: ProductList },
  { path: 'shop/:id', component: ProductDetail },
  { path: 'basket', component: Basket }
];