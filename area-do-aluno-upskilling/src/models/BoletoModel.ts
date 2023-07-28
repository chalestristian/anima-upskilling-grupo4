export class BoletoModel{
	dataVencimento = new Date().toISOString();
	valor!: number;
	nossoNumero = "05406625";
	pagadorNome!: string;
	pagadorCpf!: string;
	pagadorEmail!: string;
	pagadorTelefone!: string;
	descricao = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec tincidunt ultrices nunc, ut consectetur diam dapibus et. Suspendisse viverra mollis dui, et ultricies purus varius eu. ";
}