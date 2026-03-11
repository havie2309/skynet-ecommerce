import { Injectable, signal } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { environment } from '../../../environments/environment';
import { AuthResponse, LoginDto, RegisterDto } from '../../models/auth';
import { tap } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class AuthService {
  private baseUrl = environment.apiUrl + 'account';
  currentUser = signal<AuthResponse | null>(null);

  constructor(private http: HttpClient, private router: Router) {}

  login(dto: LoginDto) {
    return this.http.post<AuthResponse>(`${this.baseUrl}/login`, dto).pipe(
      tap(res => {
        localStorage.setItem('token', res.token);
        this.currentUser.set(res);
      })
    );
  }

  register(dto: RegisterDto) {
    return this.http.post<AuthResponse>(`${this.baseUrl}/register`, dto).pipe(
      tap(res => {
        localStorage.setItem('token', res.token);
        this.currentUser.set(res);
      })
    );
  }

  logout() {
    localStorage.removeItem('token');
    this.currentUser.set(null);
    this.router.navigate(['/shop']);
  }

  loadCurrentUser() {
    const token = localStorage.getItem('token');
    if (!token) return;
    // Decode email from token payload
    try {
      const payload = JSON.parse(atob(token.split('.')[1]));
      this.currentUser.set({ token, email: payload.email, role: payload.role });
    } catch {
      this.logout();
    }
  }
}