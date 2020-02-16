import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { ModalModule, BsDropdownModule, TooltipModule } from 'ngx-bootstrap';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';

import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { EventosComponent } from './eventos/eventos.component';
import { NavComponent } from './nav/nav.component';
import { PalestranteComponent } from './palestrante/palestrante.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { ContatosComponent } from './contatos/contatos.component';
import { TituloComponent } from './_shared/titulo/titulo.component';

import { DateTimeFormatPipePipe } from './_helps/DateTimeFormatPipe.pipe';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastrModule } from 'ngx-toastr';
import { UserComponent } from './user/user.component';
import { LoginComponent } from './user/login/login.component';
import { RegistrationComponent } from './user/registration/registration.component';
import { AuthGuard } from './auth/auth.guard';
import { AuthInterceptor } from './auth/auth.interceptor';



@NgModule({
   declarations: [
      AppComponent,
      HomeComponent,
      EventosComponent,
      NavComponent,
      DateTimeFormatPipePipe,
      PalestranteComponent,
      DashboardComponent,
      ContatosComponent,
      TituloComponent,
      UserComponent,
      LoginComponent,
      RegistrationComponent
   ],
   imports: [
      BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
      HttpClientModule,
      FormsModule,
      BrowserAnimationsModule,
      BsDropdownModule.forRoot(),
      TooltipModule.forRoot(),
      ModalModule.forRoot(),
      ToastrModule.forRoot({
         timeOut: 10000,
         positionClass: 'toast-bottom-right',
         preventDuplicates: true,
      }),
      BsDatepickerModule.forRoot(),
      ReactiveFormsModule,
      RouterModule.forRoot([
         {
            path: 'user', component: UserComponent,
            children: [
               { path: 'login', component: LoginComponent },
               { path: 'registration', component: RegistrationComponent },
            ]
         },
         { path: 'eventos', component: EventosComponent, pathMatch: 'full', canActivate: [AuthGuard] },
         { path: 'palestrantes', component: PalestranteComponent, pathMatch: 'full', canActivate: [AuthGuard] },
         { path: 'dashboard', component: DashboardComponent, pathMatch: 'full', canActivate: [AuthGuard] },
         { path: 'contatos', component: ContatosComponent, pathMatch: 'full', canActivate: [AuthGuard] },
         { path: '', redirectTo: 'dashboard', pathMatch: 'full' },
         { path: '**', redirectTo: 'dashboard', pathMatch: 'full' },
      ])
   ],
   providers: [{
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi: true
   }],
   bootstrap: [AppComponent]
})
export class AppModule { }
