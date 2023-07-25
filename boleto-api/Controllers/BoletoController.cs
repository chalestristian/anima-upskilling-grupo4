using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using ZXing;
using ZXing.Common;
using boleto_api.Models;
using boleto_api.Models.DTO;

namespace boleto_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BoletoController : ControllerBase
    {
        [HttpPost]
        public IActionResult GerarBoleto([FromBody] BoletoCreateModel boletoModel)
        {
            var boleto = new Boleto
            {
                DataVencimento = boletoModel.DataVencimento,
                Valor = boletoModel.Valor,
                NossoNumero = boletoModel.NossoNumero,
                PagadorNome = boletoModel.PagadorNome,
                PagadorCpf = boletoModel.PagadorCpf,
                PagadorEmail = boletoModel.PagadorEmail,
                PagadorTelefone = boletoModel.PagadorTelefone,
                Descricao = boletoModel.Descricao
            };

            boleto.GerarLinhaDigitavelECodigoBarras();

            using (var memoryStream = new MemoryStream())
            {
                var document = new Document(PageSize.A4, 20f, 20f, 20f, 20f);
                var writer = PdfWriter.GetInstance(document, memoryStream);
                document.Open();

                var font = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                var fontSize = 12;

                var logoPath = Path.Combine(AppContext.BaseDirectory, "Assets", "anima-gama.jpg");
                iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(logoPath);
                logo.ScaleAbsolute(100f, 50f);
                document.Add(logo);

                AddParagraph(document, $"Banco: {boleto.NomeBanco}", font, fontSize);
                AddParagraph(document, $"Beneficiário: {boleto.BeneficiarioNome}", font, fontSize);
                AddParagraph(document, $"CNPJ: {boleto.BeneficiarioCnpj}", font, fontSize);
                AddParagraph(document, $"Data de Vencimento: {boleto.DataVencimento.ToString("dd/MM/yyyy")}", font, fontSize);
                AddParagraph(document, $"Valor: R$ {boleto.Valor:F2}", font, fontSize);

                AddParagraph(document, $"Descrição: {boleto.Descricao}", font, fontSize);
                AddParagraph(document, $"Pagador: {boleto.PagadorNome}", font, fontSize);
                AddParagraph(document, $"CPF: {boleto.PagadorCpf}", font, fontSize);

                AddParagraph(document, "Linha Digitável: " + boleto.LinhaDigitavel, font, fontSize);
                AddBarcode(document, boleto.CodigoBarras);

                document.Close();

                return File(memoryStream.ToArray(), "application/pdf", "boleto.pdf");
            }
        }

        static void AddParagraph(Document document, string text, BaseFont font, int fontSize)
        {
            var paragraph = new Paragraph(text, new Font(font, fontSize));
            document.Add(paragraph);
        }

        static void AddBarcode(Document document, string code)
        {
            var barcodeWriter = new BarcodeWriter
            {
                Format = BarcodeFormat.CODE_128,
                Options = new EncodingOptions
                {
                    Height = 50,
                    PureBarcode = true,
                    Margin = 0
                }
            };
            var bitmap = barcodeWriter.Write(code);
            var barcodeImage = iTextSharp.text.Image.GetInstance(bitmap, BaseColor.BLACK);
            document.Add(barcodeImage);
        }
    }
}
