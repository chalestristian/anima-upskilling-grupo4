
namespace boleto_api.Models.DTO;

public class BoletoCreateModel
{
    public DateTime DataVencimento { get; set; }
    public decimal Valor { get; set; }
    public int NossoNumero { get; set; }

    public string PagadorNome { get; set; }
    public string PagadorCpf { get; set; }
    public string PagadorEmail { get; set; }
    public string PagadorTelefone { get; set; }
    public string Descricao { get; set; }
}