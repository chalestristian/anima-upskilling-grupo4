import type { IAluno } from "./IAluno";
import type { IPessoa } from "./IPessoa";
export interface ILogin {
  id: number;
  pessoa: IPessoa;
  login: string;
  perfilAdministrativo: boolean;
  perfilAluno: boolean,
  aluno: IAluno;
  matricula: string;
  dataCadastro:  string;
  "dataUltimoAcesso": string;  
}