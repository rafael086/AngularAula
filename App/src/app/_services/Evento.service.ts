import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Evento } from '../_models/Evento';

@Injectable({
  providedIn: 'root'
})
export class EventoService {

  constructor(private http: HttpClient) { }
  baseURL = 'https://localhost:44321/api/evento';
  getEvento(): Observable<Evento[]> {
    return this.http.get<Evento[]>(this.baseURL);
  }
  getEventoByTema(tema: string): Observable<Evento[]> {
    return this.http.get<Evento[]>(`${this.baseURL}/getByTema/${tema}`);
  }
  getEventoById(id: number): Observable<Evento> {
    return this.http.get<Evento>(`${this.baseURL}/${id}`);
  }

  postUpload(file: File, name: string) {
    const fileToUplaod = <File>file[0];
    const formData = new FormData();
    formData.append('file', fileToUplaod, name);
    return this.http.post(`${this.baseURL}/upload`, formData);
  }

  postEvento(evento: Evento) {
    return this.http.post(this.baseURL, evento);
  }
  putEvento(evento: Evento) {
    return this.http.put(`${this.baseURL}/${evento.id}`, evento);
  }
  deleteEvento(id: number) {
    return this.http.delete(`${this.baseURL}/${id}`);
  }
}
