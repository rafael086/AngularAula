import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class EventoService {

  constructor(private http: HttpClient) { }

  baseURL = 'https://localhost:44321/api/evento/'; 

  getEvento(){
    return this.http.get('');
  }
}
