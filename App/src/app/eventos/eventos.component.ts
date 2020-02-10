import { Component, OnInit, TemplateRef } from '@angular/core';
import { EventoService } from '../_services/Evento.service';
import { Evento } from '../_models/Evento';
import { BsModalRef, BsModalService, defineLocale, ptBrLocale, BsLocaleService } from 'ngx-bootstrap';
import { FormGroup, Validators, FormBuilder } from '@angular/forms';
import { TipoOperacao } from '../_enuns/TipoOperacao.enum';
import { ToastrService } from 'ngx-toastr';
defineLocale('pt-br', ptBrLocale);
@Component({
  selector: 'app-eventos',
  templateUrl: './eventos.component.html',
  styleUrls: ['./eventos.component.css']
})

export class EventosComponent implements OnInit {
  constructor(
    private eventoService: EventoService,
    private modalService: BsModalService,
    private fb: FormBuilder,
    private localeService: BsLocaleService,
    private toastr: ToastrService
  ) {
    this.localeService.use('pt-br');
  }
  titulo = 'Eventos';
  eventos: Evento[];
  evento: Evento;
  imagemLargura = 50;
  imagemMargem = 2;
  mostrarImagem = false;
  bodyDeletarEvento = '';
  eventosFiltrados: Evento[];
  registerForm: FormGroup;
  tipoOperacao: TipoOperacao;

  file: File;

  _filtroLista: string;

  get filtroLista(): string {
    return this._filtroLista;
  }

  set filtroLista(value: string) {
    this._filtroLista = value;
    this.eventosFiltrados = this.filtroLista ? this.filtrarEventos(this.filtroLista) : this.eventos;
  }

  openModal(template: any) {
    this.registerForm.reset();
    template.show();
  }

  ngOnInit() {
    this.validation();
    this.getEventos();
  }
  getEventos() {
    this.eventoService.getEvento().subscribe(
      (_eventos: Evento[]) => {
        console.log(_eventos);
        this.eventos = _eventos;
        this.eventosFiltrados = _eventos;
      },
      error => {
        console.log(error);
      }
    );
  }
  filtrarEventos(filtrarPor: string): Evento[] {
    filtrarPor = filtrarPor.toLocaleLowerCase();
    return this.eventos.filter(x => x.tema.toLocaleLowerCase().indexOf(filtrarPor) !== -1);
  }
  alternarImagem() {
    this.mostrarImagem = !this.mostrarImagem;
  }

  validation() {
    this.registerForm = this.fb.group({
      tema: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(50)]],
      local: ['', Validators.required],
      dataEvento: ['', Validators.required],
      qtdPessoas: ['', [Validators.required, Validators.max(120000)]],
      telefone: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      imagemURL: ['', Validators.required]
    });
  }

  editarEvento(evento: Evento, template: any) {
    this.tipoOperacao = TipoOperacao.Editar;
    this.openModal(template);
    this.evento = evento;
    evento.imagemURL = '';
    this.registerForm.patchValue(evento);
  }

  novoEvento(template: any) {
    this.tipoOperacao = TipoOperacao.Novo;
    this.openModal(template);
  }

  excluirEvento(evento: Evento, template: any) {
    this.openModal(template);
    this.evento = evento;
    this.bodyDeletarEvento = `Deseja excluir este evento: ${evento.tema}`;
  }

  confirmeDelete(template: any) {
    this.eventoService.deleteEvento(this.evento.id).subscribe(() => {
      this.toastr.success('Deletado com sucesso');
      template.hide();
      this.getEventos();
    }, error => {
      this.toastr.error('Erro ao tentar deletar');
      console.log(error);
    });
  }

  onFileChange(event) {
    const reader = new FileReader();
    if (event.target.files && event.target.files.length) {
      this.file = event.target.files;
      console.log(this.file);
    }
  }

  uploadImagem() {
    this.eventoService.postUpload(this.file).subscribe();

    const nomeArquivo = this.evento.imagemURL.split('\\', 3);

    this.evento.imagemURL = nomeArquivo[2];
  }

  salvarAlteracao(template: any) {
    if (this.registerForm.valid) {
      if (this.tipoOperacao === TipoOperacao.Novo) {
        this.evento = Object.assign({}, this.registerForm.value);

        this.uploadImagem();

        this.eventoService.postEvento(this.evento).subscribe((ev: Evento) => {
          console.log(ev);
          template.hide();
          this.getEventos();
          this.toastr.success('Incluido com sucesso');

        }, error => {
          console.log(error);
        });
      } else {
        this.evento = Object.assign({ id: this.evento.id }, this.registerForm.value);
        this.uploadImagem();
        this.eventoService.putEvento(this.evento).subscribe((ev: Evento) => {
          console.log(ev);
          template.hide();
          this.getEventos();
          this.toastr.success('Alterado com sucesso');

        }, error => {
          console.log(error);
        });
      }
    }
  }
}
