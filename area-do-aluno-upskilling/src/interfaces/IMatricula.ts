import type { IAluno } from '@/interfaces/IAluno';
import type { ICurso } from '@/interfaces/ICurso';

export interface IMatricula{
    id: number;
    aluno:  IAluno;
    curso: ICurso;
    dataMatricula: Date;
    valorMatricula: number;
    dataConclusao: Date;
    media: number;
    status: string;
    certificado: string;
    dataSolicitacaoCertificado: Date;
    boleto: string;
    matriculaConfirmada: boolean;
}