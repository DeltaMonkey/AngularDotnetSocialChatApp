import { HttpClient } from '@angular/common/http';
import { inject, Injectable, signal } from '@angular/core';
import { AppUser, LoginCreds } from '../../types/appUser';
import { tap } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  private http = inject(HttpClient);
  currentUser = signal<AppUser | null>(null);

  baseUrl = 'https://localhost:5001/api/';

  login(creds: LoginCreds) {
    return this.http.post<AppUser>(this.baseUrl + 'account/login', creds).pipe(
      tap(user => {
        if(user) {
          localStorage.setItem('appUser', JSON.stringify(user));
          this.currentUser.set(user)
        }
      })
    )
  }

  logout() {
    localStorage.removeItem('appUser');
    this.currentUser.set(null);
  }
}
