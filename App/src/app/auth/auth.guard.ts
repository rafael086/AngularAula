import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { AuthService } from '../_services/auth.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  /**
   *
   */
  constructor(private router: Router, private authService: AuthService) {

  }
  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): boolean {
    if (localStorage.getItem('token') !== null) {
      if(!this.authService.logado()){
       this.authService.limpaToken();
       this.router.navigate(['/user/login']);
       return false;
      }
      return true;
    } else {
      this.router.navigate(['/user/login']);
      return false;
    }
  }
}
