import { CommonModule } from '@angular/common';
import { Component, ElementRef, OnInit, ViewChild, inject } from '@angular/core';
import { Router } from '@angular/router';
import { loadStripe, Stripe, StripeCardElement, StripeElements } from '@stripe/stripe-js';
import { firstValueFrom } from 'rxjs';
import { BasketService } from '../core/services/basket';
import { PaymentService } from '../core/services/payment';
import { OrderService } from '../core/services/order';

@Component({
  selector: 'app-checkout',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './checkout.html'
})
export class Checkout implements OnInit {
  private basketService = inject(BasketService);
  private paymentService = inject(PaymentService);
  private orderService = inject(OrderService);
  private router = inject(Router);

  @ViewChild('cardElement', { static: true }) cardElementRef!: ElementRef<HTMLDivElement>;

  stripe: Stripe | null = null;
  elements: StripeElements | null = null;
  card: StripeCardElement | null = null;
  loading = false;
  error = '';

  async ngOnInit(): Promise<void> {
    const basketId = localStorage.getItem('basket_id');

    if (basketId) {
      this.basketService.getBasket(basketId);
    }

    try {
      const keyResponse = await firstValueFrom(this.paymentService.getPublishableKey());
      this.stripe = await loadStripe(keyResponse.publishableKey);

      if (!this.stripe) {
        this.error = 'Stripe failed to load';
        return;
      }

      this.elements = this.stripe.elements();
      this.card = this.elements.create('card');
      this.card.mount(this.cardElementRef.nativeElement);
    } catch (error) {
      console.error('Checkout init failed:', error);
      this.error = 'Failed to initialize payment form';
    }
  }

  async pay(): Promise<void> {
    const basket = this.basketService.basket();

    if (!basket?.id || !basket.items.length || !this.stripe || !this.card) {
      this.error = 'Basket or payment form is not ready';
      return;
    }

    this.loading = true;
    this.error = '';

    try {
      const paymentIntent = await firstValueFrom(
        this.paymentService.createPaymentIntent({ basketId: basket.id })
      );

      const result = await this.stripe.confirmCardPayment(paymentIntent.clientSecret, {
        payment_method: {
          card: this.card
        }
      });

      if (result.error) {
        this.error = result.error.message ?? 'Payment failed';
        return;
      }

      const order = await firstValueFrom(
        this.orderService.placeOrder({
          basketId: basket.id,
          items: basket.items.map(item => ({
            productId: item.productId,
            productName: item.productName,
            price: item.price,
            quantity: item.quantity
          }))
        })
      );

      this.basketService.basket.set(null);
      localStorage.removeItem('basket_id');

      this.router.navigate(['/order-confirmation', order.id]);
    } catch (error) {
      console.error('Payment failed:', error);
      this.error = 'Something went wrong during checkout';
    } finally {
      this.loading = false;
    }
  }
}
