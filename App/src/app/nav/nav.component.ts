import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../_services/auth.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {

  constructor(public authService: AuthService, public router: Router, private toatr: ToastrService) { }

  ngOnInit() {
  }

  logado() {
    return this.authService.logado();
  }

  entrar() {
    this.router.navigate(['/user/login']);
  }

  logout() {
    localStorage.removeItem('token');
    this.router.navigate(['/user/login']);
    this.toatr.show('Log out');
  }
}
