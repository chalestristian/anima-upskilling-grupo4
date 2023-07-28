export interface IBoleto{
	dataVencimento: Date;
	valor: number;
	nossoNumero: number
	pagadorNome: string;
	pagadorCpf: string;
	pagadorEmail: string;
	pagadorTelefone: string;
	descricao: string
}