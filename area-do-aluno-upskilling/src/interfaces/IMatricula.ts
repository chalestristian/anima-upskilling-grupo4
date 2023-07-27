export interface IMatricula{
    id: number;
    alunoId: number;
    cursoId: string;
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