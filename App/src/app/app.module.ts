import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import {ModalModule, BsDropdownModule, TooltipModule} from 'ngx-bootstrap';
import {BsDatepickerModule} from 'ngx-bootstrap/datepicker';

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
import {ToastrModule} from 'ngx-toastr';



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
      TituloComponent
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
         { path: 'eventos', component: EventosComponent, pathMatch: 'full' },
         { path: 'palestrantes', component: PalestranteComponent, pathMatch: 'full' },
         { path: 'dashboard', component: DashboardComponent, pathMatch: 'full' },
         { path: 'contatos', component: ContatosComponent, pathMatch: 'full' },
         { path: '', redirectTo: 'dashboard', pathMatch: 'full' },
         { path: '**', redirectTo: 'dashboard', pathMatch: 'full' },
      ])
   ],
   providers: [],
   bootstrap: [AppComponent]
})
export class AppModule { }
