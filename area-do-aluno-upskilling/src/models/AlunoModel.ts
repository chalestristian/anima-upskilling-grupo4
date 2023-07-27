export class AlunoModel{
    id!: number;
    pessoaId!: number;
    matricula!: string;
    dataCadastro = new Date().toISOString();
}