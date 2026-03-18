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
        localStorage.setItem('refreshToken', res.refreshToken);
        this.currentUser.set(res);
      })
    );
  }

  register(dto: RegisterDto) {
    return this.http.post<AuthResponse>(`${this.baseUrl}/register`, dto).pipe(
      tap(res => {
        localStorage.setItem('token', res.token);
        localStorage.setItem('refreshToken', res.refreshToken);
        this.currentUser.set(res);
      })
    );
  }

  logout() {
    localStorage.removeItem('token');
    localStorage.removeItem('refreshToken');
    this.currentUser.set(null);
    this.router.navigate(['/shop']);
  }

  refreshToken() {
  const user = this.currentUser();
  if (!user) return;

  return this.http.post<AuthResponse>(`${this.baseUrl}/refresh-token`, {
    email: user.email,
    refreshToken: user.refreshToken
  }).pipe(
    tap(res => {
      localStorage.setItem('token', res.token);
      localStorage.setItem('refreshToken', res.refreshToken);
      this.currentUser.set(res);
    })
  );
}

logoutFromServer() {
  const user = this.currentUser();
  if (!user) {
    this.logout();
    return;
  }

  return this.http.post(`${this.baseUrl}/logout`, {
    email: user.email,
    refreshToken: user.refreshToken
  }).pipe(
    tap(() => this.logout())
  );
}


  loadCurrentUser() {
    const token = localStorage.getItem('token');
    const refreshToken = localStorage.getItem('refreshToken');
    if (!token || !refreshToken) return;

    try {
      const payload = JSON.parse(atob(token.split('.')[1]));
      const exp = payload.exp;

      if (!exp || Date.now() >= exp * 1000) {
        this.logout();
        return;
      }

      const email =
        payload.email ??
        payload['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress'];

      const role =
        payload.role ??
        payload['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'];

      this.currentUser.set({ token, refreshToken, email, role });
    } catch {
      this.logout();
    }
  }
}
