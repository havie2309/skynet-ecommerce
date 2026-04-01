import { Injectable, signal, computed } from '@angular/core';

@Injectable({ providedIn: 'root' })
export class WishlistService {
  private ids = signal<Set<number>>(
    new Set(JSON.parse(localStorage.getItem('wishlist') ?? '[]'))
  );

  count = computed(() => this.ids().size);

  isWishlisted(id: number) { return this.ids().has(id); }

  toggle(id: number) {
    const next = new Set(this.ids());
    next.has(id) ? next.delete(id) : next.add(id);
    this.ids.set(next);
    localStorage.setItem('wishlist', JSON.stringify([...next]));
  }
}
