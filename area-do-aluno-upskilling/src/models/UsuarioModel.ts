export class UsuarioModel {
	id!: number;
	pessoaId!: number;
	login!: string;
	senha!: string;
	alunoId!: number;
	perfilAdministrativo = false;
	perfilAluno = true;
	dataUltimoAcesso = new Date().toISOString();
	dataCadastro = new Date().toISOString();
}