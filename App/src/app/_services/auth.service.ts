import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { JwtHelperService } from '@auth0/angular-jwt';
import { map } from 'rxjs/operators';
@Injectable({
  providedIn: 'root'
})
export class AuthService {

  baseURL = 'https://localhost:44321/api/user/';
  decodedToken: any;

  constructor(private http: HttpClient) { }

  jwtHelper = new JwtHelperService();

  login(model: any) {
    return this.http.post(`${this.baseURL}login`, model).pipe(
      map((r: any) => {
        const user = r;
        if (user) {
          localStorage.setItem('token', user.token);
          this.decodedToken = this.jwtHelper.decodeToken(user.token);
        }
      })
    );
  }

  register(model: any) {
    return this.http.post(`${this.baseURL}register`, model);
  }

  logado() {
    const token = localStorage.getItem('token');
    return !this.jwtHelper.isTokenExpired(token);
  }
}
