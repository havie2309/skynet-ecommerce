import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { AuthService } from '../../core/services/auth';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterModule],
  templateUrl: './register.html',
  styleUrl: './register.scss'
})
export class Register {
  email = '';
  password = '';
  errorMessage = '';
  passwordHint = 'Use at least 8 characters with uppercase, lowercase, and a number.';

  constructor(private authService: AuthService, private router: Router) {}

  onSubmit() {
    this.errorMessage = '';

    const passwordPattern = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$/;
    if (!passwordPattern.test(this.password)) {
      this.errorMessage = this.passwordHint;
      return;
    }

    this.authService.register({ email: this.email, password: this.password })
      .subscribe({
        next: () => this.router.navigate(['/shop']),
        error: (err) => {
          if (err.error?.errors) {
            const validationErrors = Object.values(err.error.errors).flat();
            this.errorMessage = validationErrors.join(' ');
          } else {
            this.errorMessage = err.error?.message || 'Registration failed';
          }
        }
      });
  }
}
