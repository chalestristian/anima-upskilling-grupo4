namespace boleto_api.Models;

using System.Globalization;

public class Boleto
{
    public Boleto()
    {
        CodigoBanco = 123;
        CodigoMoeda = 9;
        CodigoBeneficiario = 1234;
        NomeBanco = "Caixa Econômica Federal";
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
    public int NossoNumero { get; set; }
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
        string primeiroSegmento = $"{CodigoBanco}{CodigoMoeda}";
        string segundoSegmento = $"{CodigoBeneficiario}"; // Exemplo fictício de código do beneficiário
        string terceiroSegmento = NossoNumero.ToString().PadLeft(11, '0');
        string quartoSegmento = "9"; // Exemplo fictício de dígito verificador geral
        string quintoSegmento = $"{NossoNumero}{DataVencimento:ddMMyyyy}{Valor.ToString("F", CultureInfo.InvariantCulture).Replace(".", "").PadLeft(10, '0')}";

        string linhaDigitavelSemDV = $"{primeiroSegmento}{segundoSegmento}{quartoSegmento}{quintoSegmento}";
        string dvQuintoSegmento = CalcularDVMod10(quintoSegmento);
        string linhaDigitavelComDV = $"{primeiroSegmento}{dvQuintoSegmento}{segundoSegmento}{terceiroSegmento}{quartoSegmento}{quintoSegmento}";

        LinhaDigitavel = FormatarLinhaDigitavel(linhaDigitavelComDV);
        CodigoBarras = LinhaDigitavel; // Exemplo fictício de código de barras
    }

    private string CalcularDVMod10(string segmento)
    {
        int dv = 0;
        int multiplicador = 2;

        for (int i = segmento.Length - 1; i >= 0; i--)
        {
            int valor = int.Parse(segmento[i].ToString()) * multiplicador;

            if (valor >= 10)
                valor = (valor % 10) + 1;

            dv += valor;

            multiplicador = multiplicador == 2 ? 1 : 2;
        }

        int resto = dv % 10;
        return (10 - resto).ToString();
    }

    private string FormatarLinhaDigitavel(string linhaDigitavel)
    {
        return $"{linhaDigitavel.Substring(0, 5)}.{linhaDigitavel.Substring(5, 5)} " +
               $"{linhaDigitavel.Substring(10, 5)}.{linhaDigitavel.Substring(15, 6)} " +
               $"{linhaDigitavel.Substring(21, 5)}.{linhaDigitavel.Substring(26, 6)} " +
               $"{linhaDigitavel.Substring(32, 1)} " +
               $"{linhaDigitavel.Substring(33)}";
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