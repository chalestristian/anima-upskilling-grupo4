export interface IUsuario {
	id: number;
	pessoaId: number;
	login: string;
	senha: string;
	alunoId: number;
	perfilAdministrativo: boolean;
	perfilAluno: boolean;
	dataUltimoAcesso: string;
	dataCadastro: string;
}