import { HttpClient } from '@angular/common/http';
import { inject, Injectable, signal } from '@angular/core';
import { AppUser, LoginCreds, RegisterCreds } from '../../types/appUser';
import { tap } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  private http = inject(HttpClient);
  currentUser = signal<AppUser | null>(null);

  baseUrl = 'https://localhost:5001/api/';

  register(creds: RegisterCreds) {
    return this.http.post<AppUser>(this.baseUrl + 'account/register', creds).pipe(
      tap(user => {
        if(user) {
          this.setCurrentUser(user);
        }
      })
    )
  }

  login(creds: LoginCreds) {
    return this.http.post<AppUser>(this.baseUrl + 'account/login', creds).pipe(
      tap(user => {
        if(user) {
          this.setCurrentUser(user);
        }
      })
    )
  }

  setCurrentUser(appUser: AppUser) {
    localStorage.setItem('appUser', JSON.stringify(appUser));
    this.currentUser.set(appUser)
  }

  logout() {
    localStorage.removeItem('appUser');
    this.currentUser.set(null);
  }
}
