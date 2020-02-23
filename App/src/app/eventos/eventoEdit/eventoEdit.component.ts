import { Component, OnInit } from '@angular/core';
import { EventoService } from 'src/app/_services/Evento.service';
import { BsLocaleService } from 'ngx-bootstrap';
import { FormBuilder, FormGroup, Validators, FormArray } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { Evento } from 'src/app/_models/Evento';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-evento-edit',
  templateUrl: './eventoEdit.component.html',
  styleUrls: ['./eventoEdit.component.css']
})
export class EventoEditComponent implements OnInit {
  titulo = 'Editar evento';
  evento: Evento = new Evento();
  imagemURL = 'assets/img/upload.png';
  registerForm: FormGroup;
  fileNameToUpdate: string;
  dataAtual: string;
  file: File;

  constructor(private eventoService: EventoService, private router: ActivatedRoute, private fb: FormBuilder,
    // tslint:disable-next-line: align
    private localeService: BsLocaleService, private toastr: ToastrService) { }
  ngOnInit() {
    this.validation();
    this.carregarEvento();
  }
  carregarEvento() {
    const id = +this.router.snapshot.paramMap.get('id');
    this.eventoService.getEventoById(id).subscribe((e: Evento) => {
      this.evento = Object.assign({}, e);
      this.fileNameToUpdate = e.imagemURL.toString();
      this.imagemURL = `https://localhost:44321/Resources/Images/${this.evento.imagemURL}?_ts=${this.dataAtual}`;
      this.evento.imagemURL = '';
      this.registerForm.patchValue(this.evento);
      this.evento.lotes.forEach(x => {
        this.lotes.push(this.criaLote(x));
      });
      this.evento.redesSociais.forEach(x => {
        this.redesSociais.push(this.criaRedeSocial(x));
      });
      console.log(this.evento);
    });
  }

  get lotes(): FormArray {
    return this.registerForm.get('lotes') as FormArray;
  }

  get redesSociais(): FormArray {
    return this.registerForm.get('redesSociais') as FormArray;
  }

  criaLote(lota: any): FormGroup {
    return this.fb.group({
      id: [lota.id],
      nome: [lota.nome, Validators.required],
      quantidade: [lota.quantidade, Validators.required],
      preco: [lota.preco, Validators.required],
      dataInicio: [lota.dataInicio],
      dataFim: [lota.dataFim],
    });
  }

  criaRedeSocial(rede: any): FormGroup {
    return this.fb.group({
      id: [rede.id],
      nome: [rede.nome, Validators.required],
      url: [rede.url, Validators.required],
    });
  }

  removerLote(id: number) {
    this.lotes.removeAt(id);
  }

  removerRedeSocial(id: number) {
    this.redesSociais.removeAt(id);
  }

  adicionarRedeSocial() {
    this.redesSociais.push(this.criaRedeSocial({ id: 0 }));
  }

  adicionarLote() {
    this.lotes.push(this.criaLote({ id: 0 }));
  }

  onFileChange(file: FileList) {
    const reader = new FileReader();

    reader.onload = (event: any) => {
      this.imagemURL = event.target.result;
    };
    this.file =  (<HTMLInputElement>event.target).files[0];

    reader.readAsDataURL(file[0]);
  }

  validation() {
    this.registerForm = this.fb.group({
      id: [],
      tema: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(50)]],
      local: ['', Validators.required],
      dataEvento: ['', Validators.required],
      qtdPessoas: ['', [Validators.required, Validators.max(120000)]],
      telefone: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      imagemURL: [''],
      lotes: this.fb.array([]),
      redesSociais: this.fb.array([])
    });
  }
  uploadImagem() {
    if (this.registerForm.get('imagemURL').value !== '') {
      this.eventoService.postUpload(this.file, this.fileNameToUpdate).subscribe(() => {
        this.dataAtual = new Date().getMilliseconds().toString();
        this.imagemURL = `https://localhost:44321/Resources/Images/${this.evento.imagemURL}?_ts=${this.dataAtual}`;
      });
    }
  }
  salvarEvento() {
    this.evento = Object.assign({ id: this.evento.id }, this.registerForm.value);
    this.evento.imagemURL = this.fileNameToUpdate;
    this.uploadImagem();
    this.eventoService.putEvento(this.evento).subscribe((ev: Evento) => {
      console.log(ev);
      this.toastr.success('Alterado com sucesso');
    }, error => {
      console.log(error);
    });
  }
}
