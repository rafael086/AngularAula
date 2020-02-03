import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-eventos',
  templateUrl: './eventos.component.html',
  styleUrls: ['./eventos.component.css']
})
export class EventosComponent implements OnInit {

  //eventos: any = [
  //  { Id: 1, Tema: "Angular", Local: "BH" },
  //  { Id: 2, Tema: ".NET Core", Local: "SP" },
  //  { Id: 3, Tema: "Angular e .NET Core", Local: "RJ" }
  //]
  eventos: any = [];
  constructor(private http: HttpClient) {

  }

  ngOnInit() {
    this.getEventos();
  }

  getEventos() {
    this.http.get("htt://localhost:44321/api/evento/").subscribe(
      r => { this.eventos = r; },
      error => {
        console.log(error);
      }
    )
  }
}
