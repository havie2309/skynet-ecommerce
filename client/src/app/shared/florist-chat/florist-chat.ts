import { Component, ElementRef, ViewChild } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { ChatService, ChatMessage } from '../../core/services/chat';
import { BasketService } from '../../core/services/basket';
import { Product } from '../../models/product';

@Component({
  selector: 'app-florist-chat',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterModule],
  templateUrl: './florist-chat.html',
  styleUrl: './florist-chat.scss'
})
export class FloristChat {
  @ViewChild('messagesEl') messagesEl!: ElementRef<HTMLDivElement>;

  open = false;
  input = '';
  messages: ChatMessage[] = [
    {
      role: 'assistant',
      text: "Hi! I'm your personal florist 🌸 Tell me who you're shopping for, the occasion, and your budget — I'll find the perfect flowers for you!"
    }
  ];

  constructor(
    private chatService: ChatService,
    private basketService: BasketService
  ) {}

  toggle() { this.open = !this.open; }

  send() {
    const text = this.input.trim();
    if (!text) return;

    this.messages.push({ role: 'user', text });
    this.messages.push({ role: 'assistant', loading: true });
    this.input = '';
    this.scrollToBottom();

    this.chatService.send(text).subscribe({
      next: (res) => {
        this.messages[this.messages.length - 1] = { role: 'assistant', response: res };
        this.scrollToBottom();
      },
      error: () => {
        this.messages[this.messages.length - 1] = {
          role: 'assistant',
          text: "Sorry, I'm having trouble connecting right now. Please try again in a moment."
        };
        this.scrollToBottom();
      }
    });
  }

  addToBasket(product: Product) {
    this.basketService.addItemToBasket(product);
  }

  onKeydown(event: KeyboardEvent) {
    if (event.key === 'Enter' && !event.shiftKey) {
      event.preventDefault();
      this.send();
    }
  }

  private scrollToBottom() {
    setTimeout(() => {
      if (this.messagesEl) {
        this.messagesEl.nativeElement.scrollTop = this.messagesEl.nativeElement.scrollHeight;
      }
    }, 50);
  }

  toneEmoji(tone: string): string {
    const map: Record<string, string> = {
      romantic: '💕', apology: '🙏', celebration: '🎉',
      sympathy: '🤍', friendship: '🌻', gratitude: '💛', birthday: '🎂'
    };
    return map[tone] ?? '🌸';
  }
}
