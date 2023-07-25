namespace boleto_api.Models;

public class Boleto
{
    public Boleto()
    {
        CodigoBanco = 104;
        CodigoMoeda = 9;
        CodigoBeneficiario = 1234;
        NomeBanco = "Caixa Econ�mica Federal";
        BeneficiarioNome = "Empresa DMTW Ltda.";
        BeneficiarioCnpj = "12.345.678/0001-90";
        BeneficiarioEndereco = "Avenida do Contorno, 123";
        BeneficiarioCidadeUF = "Belo Horizonte - MG";
    }

    public int CodigoBanco { get; set; }
    public int CodigoBeneficiario { get; set; }
    public int CodigoMoeda { get; set; }
    public string NomeBanco { get; set; }
    public DateTime DataVencimento { get; set; }
    public decimal Valor { get; set; }
    public string NossoNumero { get; set; }
    public string LinhaDigitavel { get; set; }
    public string CodigoBarras { get; set; }

    public string PagadorNome { get; set; }
    public string PagadorCpf { get; set; }
    public string PagadorEmail { get; set; }
    public string PagadorTelefone { get; set; }

    public string BeneficiarioNome { get; }
    public string BeneficiarioCnpj { get; }
    public string BeneficiarioEndereco { get; }
    public string BeneficiarioCidadeUF { get; }

    public string Descricao { get; set; }
    public void GerarLinhaDigitavelECodigoBarras()
    {
        LinhaDigitavel = GerarValor(48);
        CodigoBarras = GerarValor(44);
    }
    public static string GerarValor(int length)
    {
        var random = new Random();
        string number = "";

        for (int i = 0; i < length; i++)
        {
            number += random.Next(0, 9).ToString();
        }

        return number;
    }


}