import { Routes } from '@angular/router';
import { authGuard } from './core/services/guards/auth.guard';
import { adminGuard } from './core/services/guards/admin.guard';

export const routes: Routes = [
  { path: '', redirectTo: 'shop', pathMatch: 'full' },

  {
    path: 'shop',
    loadComponent: () =>
      import('./shop/product-list/product-list').then(m => m.ProductList)
  },
  {
    path: 'shop/:id',
    loadComponent: () =>
      import('./shop/product-detail/product-detail').then(m => m.ProductDetail)
  },
  {
    path: 'basket',
    canActivate: [authGuard],
    loadComponent: () =>
      import('./basket/basket').then(m => m.Basket)
  },
  {
    path: 'checkout',
    canActivate: [authGuard],
    loadComponent: () =>
      import('./checkout/checkout').then(m => m.Checkout)
  },
  {
    path: 'login',
    loadComponent: () =>
      import('./auth/login/login').then(m => m.Login)
  },
  {
    path: 'register',
    loadComponent: () =>
      import('./auth/register/register').then(m => m.Register)
  },
  {
    path: 'order-confirmation/:id',
    canActivate: [authGuard],
    loadComponent: () =>
      import('./order-confirmation/order-confirmation').then(m => m.OrderConfirmation)
  },
  {
    path: 'orders',
    canActivate: [authGuard],
    loadComponent: () =>
      import('./orders/orders').then(m => m.Orders)
  },
  {
    path: 'orders/:id',
    canActivate: [authGuard],
    loadComponent: () =>
      import('./core/services/order-detail/order-detail').then(m => m.OrderDetail)
  },
  {
    path: 'admin/products/new',
    canActivate: [authGuard, adminGuard],
    loadComponent: () =>
      import('./admin/admin-product-form').then(m => m.AdminProductForm)
  },
  {
    path: 'admin/products/edit/:id',
    canActivate: [authGuard, adminGuard],
    loadComponent: () =>
      import('./admin/admin-product-form').then(m => m.AdminProductForm)
  },
  {
    path: 'admin/products',
    canActivate: [authGuard, adminGuard],
    loadComponent: () =>
      import('./admin/admin-products').then(m => m.AdminProducts)
  },
  {
    path: 'admin/orders',
    canActivate: [authGuard, adminGuard],
    loadComponent: () =>
      import('./admin/admin-orders').then(m => m.AdminOrders)
  },
  {
    path: 'profile',
    canActivate: [authGuard],
    loadComponent: () =>
      import('./profile/profile').then(m => m.Profile)
  }
];
