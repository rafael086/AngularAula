import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { User } from 'src/app/_models/User';
import { AuthService } from 'src/app/_services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.css']
})
export class RegistrationComponent implements OnInit {

  registerForm: FormGroup;
  user: User;

  constructor(public fb: FormBuilder, private toastr: ToastrService, private authService: AuthService, private router: Router) { }

  ngOnInit() {
    this.validation();
  }

  validation() {
    this.registerForm = this.fb.group({
      fullName: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      userName: ['', Validators.required],
      passwords: this.fb.group({
        confirmPassword: ['', Validators.required],
        password: ['', [Validators.required, Validators.minLength(4)]]
      }, { validator: this.compararSenhas }),
    });
  }

  compararSenhas(fg: FormGroup) {
    const confirmSenha = fg.get('confirmPassword');
    if (confirmSenha.errors == null || 'mismatch' in confirmSenha.errors) {
      const senha = fg.get('password');
      if (senha.value !== confirmSenha.value) {
        confirmSenha.setErrors({ mismatch: true });
      } else {
        confirmSenha.setErrors(null);
      }
    }
  }

  cadastrarUsuario() {
    if (this.registerForm.valid) {
      this.user = Object.assign({
        password: this.registerForm.get('passwords.password').value
      }, this.registerForm.value);
    }
    this.authService.register(this.user).subscribe(
      () => {
        this.router.navigate(['/user/login']);
        this.toastr.success('Cadastrado com sucesso');
      },
      error => {
        const erro = error.error;
        console.log(error);
        erro.forEach(element => {
          switch (element.code) {
            case 'DuplicateUserName':
              this.toastr.error('Cadastro duplicado');
              break;
            default:
              this.toastr.error(`Erro no cadastro! Codigo: ${element.code}`);
              break;
          }
        });
      }
    )
    console.log(this.user);
  }

}
