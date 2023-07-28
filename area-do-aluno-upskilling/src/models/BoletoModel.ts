export class BoletoModel{
	dataVencimento = new Date().toISOString();
	valor!: number;
	nossoNumero = "05406625";
	pagadorNome!: string;
	pagadorCpf!: string;
	pagadorEmail!: string;
	pagadorTelefone!: string;
	descricao = "";
}