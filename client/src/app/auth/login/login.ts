import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { AuthService } from '../../core/services/auth';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterModule],
  templateUrl: './login.html',
  styleUrl: './login.scss'
})
export class Login {
  email = '';
  password = '';
  errorMessage = '';

  constructor(private authService: AuthService, private router: Router) {}

  onSubmit() {
    this.errorMessage = '';
    this.authService.login({ email: this.email, password: this.password })
      .subscribe({
        next: () => this.router.navigate(['/shop']),
        error: (err) => {
          if (err.error?.errors) {
            const validationErrors = Object.values(err.error.errors).flat();
            this.errorMessage = validationErrors.join(' ');
          } else {
            this.errorMessage = err.error?.message || 'Login failed';
          }
        }
      });
  }
}