import { Lote } from './Lote';
import { RedeSocial } from './RedeSocial';
import { Palestrante } from './Palestrante';

export interface Evento {
    id: number;
    local: string;
    dataEvento: Date;
    tema: string;
    qtdPessoas: number;
    lote: string;
    imagemURL: string;
    telefone: string;
    email: string;

    lotes: Lote[];
    redeSociais: RedeSocial[];
    palestranteEventos: Palestrante[];
}
