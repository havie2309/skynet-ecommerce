import { Routes } from '@angular/router';
import { ProductList } from './shop/product-list/product-list';
import { ProductDetail } from './shop/product-detail/product-detail';
import { Basket } from './basket/basket';
import { Login } from './auth/login/login';
import { Register } from './auth/register/register';
import { authGuard } from './core/services/guards/auth.guard';

export const routes: Routes = [
  { path: '', redirectTo: 'shop', pathMatch: 'full' },
  { path: 'shop', component: ProductList },
  { path: 'shop/:id', component: ProductDetail },
  { path: 'basket', component: Basket, canActivate: [authGuard] },
  { path: 'login', component: Login },
  { path: 'register', component: Register }
];