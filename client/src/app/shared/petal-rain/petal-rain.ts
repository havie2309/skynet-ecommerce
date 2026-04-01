import { Component } from '@angular/core';

interface Petal {
  left: number;
  delay: number;
  duration: number;
  size: number;
  color: string;
  radius: string;
  drift: number;
}

@Component({
  selector: 'app-petal-rain',
  standalone: true,
  template: `
    <div class="petal-container" aria-hidden="true">
      @for (p of petals; track $index) {
        <span class="petal"
          [style.--left]="p.left + '%'"
          [style.--delay]="p.delay + 's'"
          [style.--dur]="p.duration + 's'"
          [style.--size]="p.size + 'px'"
          [style.--color]="p.color"
          [style.--radius]="p.radius"
          [style.--drift]="p.drift + 'px'">
        </span>
      }
    </div>
  `,
  styleUrl: './petal-rain.scss'
})
export class PetalRain {
  petals: Petal[] = Array.from({ length: 22 }, () => this.randomPetal());

  private randomPetal(): Petal {
    const colors = ['#fbc8e0', '#fde8f2', '#f5a0c8', '#ffd6ea', '#e8609a', '#ffb3d0'];
    const shapes = ['50% 0 50% 0', '50%', '50% 0 50% 50%', '0 50% 50% 50%', '40% 60% 60% 40%'];
    return {
      left:     Math.random() * 100,
      delay:   -(Math.random() * 18),
      duration: 10 + Math.random() * 12,
      size:     6  + Math.random() * 12,
      color:    colors[Math.floor(Math.random() * colors.length)],
      radius:   shapes[Math.floor(Math.random() * shapes.length)],
      drift:    (Math.random() - 0.5) * 80
    };
  }
}
