import { Component, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { Navbar } from './layout/navbar/navbar';
import { ToastComponent } from './shared/toast/toast';
import { AuthService } from './core/services/auth';
import { BasketService } from './core/services/basket';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, Navbar, ToastComponent],
  templateUrl: './app.html',
  styleUrl: './app.scss'
})
export class App implements OnInit {
  constructor(
    private authService: AuthService,
    private basketService: BasketService
  ) {}

  ngOnInit() {
    this.authService.loadCurrentUser();
    const basketId = localStorage.getItem('basket_id');
    if (basketId) this.basketService.getBasket(basketId);
  }
}
