import { Component, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { Navbar } from './layout/navbar/navbar';
import { ToastComponent } from './shared/toast/toast';
import { PetalRain } from './shared/petal-rain/petal-rain';
import { FloristChat } from './shared/florist-chat/florist-chat';
import { AuthService } from './core/services/auth';
import { BasketService } from './core/services/basket';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, Navbar, ToastComponent, PetalRain, FloristChat],
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
