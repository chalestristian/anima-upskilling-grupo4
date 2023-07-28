import type { AlunoModel } from "./AlunoModel";
import type { CursoModel } from "./CursoModel";

export class MatriculaModel{
    id!: number;
    aluno!: AlunoModel;
    curso!: CursoModel;
    dataMatricula?: Date;
    valorMatricula!: number;
    dataConclusao?: Date;
    media?: number;
    status?: string;
    certificado?: string;
    dataSolicitacaoCertificado?: Date;
    boleto?: string;
    matriculaConfirmada?: boolean;
}