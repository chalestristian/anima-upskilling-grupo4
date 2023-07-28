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
                var document = new Document(PageSize.A4);
                var writer = PdfWriter.GetInstance(document, memoryStream);
                document.Open();

                var logoPath = Path.Combine(AppContext.BaseDirectory, "Assets", "anima-gama.jpg");
                iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(logoPath);
                logo.ScaleAbsolute(100f, 50f);
                document.Add(logo);

                var baseFont = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);

                // Definindo estilos de fonte e tamanho
                var fonteTitulo = new Font(baseFont, 8, Font.NORMAL);
                var fonteValor = new Font(baseFont, 10, Font.NORMAL);
                var fonteMaior = new Font(baseFont, 16, Font.NORMAL);

                var table = new PdfPTable(11)
                {
                    TotalWidth = 520f,
                    LockedWidth = true
                };

                table.SetWidths(new float[] { 9.692307692307692f, 10.256410256410257f, 19.487179487179485f, 5.128205128205128f,
                                             13.333333333333334f, 8.205128205128204f, 10.256410256410257f, 19.487179487179485f,
                                             19.487179487179485f, 38.97435897435898f, 38.97435897435898f });

                // Cabeçalho
                table.AddCell(new PdfPCell(new Phrase(" ", fonteTitulo)) { Border = Rectangle.NO_BORDER, FixedHeight = 14f });
                table.AddCell(new PdfPCell(new Phrase(" ", fonteTitulo)) { Border = Rectangle.NO_BORDER, FixedHeight = 14f });
                table.AddCell(new PdfPCell(new Phrase(" ", fonteTitulo)) { Border = Rectangle.NO_BORDER, FixedHeight = 14f });
                table.AddCell(new PdfPCell(new Phrase(" ", fonteTitulo)) { Border = Rectangle.NO_BORDER, FixedHeight = 14f });
                table.AddCell(new PdfPCell(new Phrase(" ", fonteTitulo)) { Border = Rectangle.NO_BORDER, FixedHeight = 14f });
                table.AddCell(new PdfPCell(new Phrase(" ", fonteTitulo)) { Border = Rectangle.NO_BORDER, FixedHeight = 14f });
                table.AddCell(new PdfPCell(new Phrase(" ", fonteTitulo)) { Border = Rectangle.NO_BORDER, FixedHeight = 14f });
                table.AddCell(new PdfPCell(new Phrase(" ", fonteTitulo)) { Border = Rectangle.NO_BORDER, FixedHeight = 14f });
                table.AddCell(new PdfPCell(new Phrase(" ", fonteTitulo)) { Border = Rectangle.NO_BORDER, FixedHeight = 14f });
                table.AddCell(new PdfPCell(new Phrase(" ", fonteTitulo)) { Border = Rectangle.NO_BORDER, FixedHeight = 14f });
                table.AddCell(new PdfPCell(new Phrase(" ", fonteTitulo)) { Border = Rectangle.NO_BORDER, FixedHeight = 14f });


                // Código do banco e Linha Digitável
                table.AddCell(new PdfPCell(new Phrase("123-0", fonteMaior)) { Colspan = 2, Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_RIGHT });
                table.AddCell(new PdfPCell(new Phrase(boleto.LinhaDigitavel, fonteMaior)) { Colspan = 10, Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_RIGHT });

                // Local de Pagamento e Vencimento
                table.AddCell(new PdfPCell(new Phrase("Local de Pagamento", fonteTitulo)) { Colspan = 10 });
                table.AddCell(new PdfPCell(new Phrase("Vencimento", fonteTitulo)));

                // Valores de Local de Pagamento e Vencimento
                table.AddCell(new PdfPCell(new Phrase("INTERNET BANKING", fonteValor)) { Colspan = 10 });
                table.AddCell(new PdfPCell(new Phrase(boleto.DataVencimento.ToString("dd/MM/yyyy"), fonteValor)));

                // Cedente e Agência/Código do Cedente
                table.AddCell(new PdfPCell(new Phrase("Cedente", fonteTitulo)) { Colspan = 10 });
                table.AddCell(new PdfPCell(new Phrase("Agência/Código do Cedente", fonteTitulo)));

                // Valores de Cedente e Agência/Código do Cedente
                table.AddCell(new PdfPCell(new Phrase("Empresa DMTW - Ânima Upskilling Grupo 4 Ltda.", fonteValor)) { Colspan = 10 });
                table.AddCell(new PdfPCell(new Phrase("1234", fonteValor)));

                // Dados do Documento
                table.AddCell(new PdfPCell(new Phrase("Data do Documento", fonteTitulo)) { Colspan = 3 });
                table.AddCell(new PdfPCell(new Phrase("Número do Documento", fonteTitulo)) { Colspan = 4 });
                table.AddCell(new PdfPCell(new Phrase("Espécie", fonteTitulo)));
                table.AddCell(new PdfPCell(new Phrase("Aceite", fonteTitulo)));
                table.AddCell(new PdfPCell(new Phrase("Data do Processamento", fonteTitulo)));
                table.AddCell(new PdfPCell(new Phrase("Nosso Número", fonteTitulo)));

                // Valores dos Dados do Documento
                table.AddCell(new PdfPCell(new Phrase(DateTime.UtcNow.ToString("dd/MM/yyyy"), fonteValor)) { Colspan = 3 });
                table.AddCell(new PdfPCell(new Phrase(boleto.NossoNumero.ToString(), fonteValor)) { Colspan = 4 });
                table.AddCell(new PdfPCell(new Phrase("RC", fonteValor)));
                table.AddCell(new PdfPCell(new Phrase("N", fonteValor)));
                table.AddCell(new PdfPCell(new Phrase(DateTime.UtcNow.ToString("dd/MM/yyyy"), fonteValor)));
                table.AddCell(new PdfPCell(new Phrase(boleto.NossoNumero.ToString(), fonteValor)));

                // Uso do Banco, Carteira, Moeda, Quantidade, Valor e Valor do Documento
                table.AddCell(new PdfPCell(new Phrase("Uso do Banco", fonteTitulo)) { Colspan = 3 });
                table.AddCell(new PdfPCell(new Phrase("Carteira", fonteTitulo)) { Colspan = 2 });
                table.AddCell(new PdfPCell(new Phrase("Moeda", fonteTitulo)) { Colspan = 2 });
                table.AddCell(new PdfPCell(new Phrase("Quantidade", fonteTitulo)) { Colspan = 2 });
                table.AddCell(new PdfPCell(new Phrase("(x) Valor", fonteTitulo)));
                table.AddCell(new PdfPCell(new Phrase("(=) Valor do Documento", fonteTitulo)));

                // Valores de Uso do Banco, Carteira, Moeda, Quantidade, Valor e Valor do Documento
                table.AddCell(new PdfPCell(new Phrase(" ", fonteValor)) { Colspan = 3 });
                table.AddCell(new PdfPCell(new Phrase("SR", fonteValor)) { Colspan = 2 });
                table.AddCell(new PdfPCell(new Phrase("R$", fonteValor)) { Colspan = 2 });
                table.AddCell(new PdfPCell(new Phrase("1", fonteValor)) { Colspan = 2 });
                table.AddCell(new PdfPCell(new Phrase(" ", fonteValor)));
                table.AddCell(new PdfPCell(new Phrase(String.Format("{0:F2}", boleto.Valor), fonteValor)));

                // Instruções e Desconto
                table.AddCell(new PdfPCell(new Phrase("Instruções", fonteTitulo)) { Colspan = 10 });
                table.AddCell(new PdfPCell(new Phrase("(-) Desconto", fonteTitulo)));

                // Valor de Instruções e Desconto
                table.AddCell(new PdfPCell(new Phrase(boleto.Descricao, fonteValor)) { Colspan = 10 });
                table.AddCell(new PdfPCell(new Phrase(" ", fonteValor)));

                // Outras Deduções/Abatimento e Mora/Multa/Juros
                table.AddCell(new PdfPCell(new Phrase("(-) Outras Deduções/Abatimento", fonteTitulo)) { Colspan = 10 });
                table.AddCell(new PdfPCell(new Phrase(" ", fonteValor)));

                // Valor de Outras Deduções/Abatimento e Mora/Multa/Juros
                table.AddCell(new PdfPCell(new Phrase(" ", fonteValor)) { Colspan = 10, Border = Rectangle.LEFT_BORDER });
                table.AddCell(new PdfPCell(new Phrase("(+) Mora/Multa/Juros", fonteTitulo)));

                // Valor de Mora/Multa/Juros
                table.AddCell(new PdfPCell(new Phrase(" ", fonteValor)) { Colspan = 10, Border = Rectangle.LEFT_BORDER });
                table.AddCell(new PdfPCell(new Phrase(" ", fonteValor)));

                // Outros Acréscimos e Valor Cobrado
                table.AddCell(new PdfPCell(new Phrase(" ", fonteValor)) { Colspan = 10, Border = Rectangle.LEFT_BORDER });
                table.AddCell(new PdfPCell(new Phrase("(+) Outros Acréscimos", fonteTitulo)));

                // Valor de Outros Acréscimos e Valor Cobrado
                table.AddCell(new PdfPCell(new Phrase(" ", fonteValor)) { Colspan = 10, Border = Rectangle.LEFT_BORDER });
                table.AddCell(new PdfPCell(new Phrase(" ", fonteValor)));

                // Linha com informações do sacado
                table.AddCell(new PdfPCell(new Phrase("Sacado:", fonteTitulo)) { Rowspan = 3 });
                table.AddCell(new PdfPCell(new Phrase(boleto.PagadorNome, fonteValor)) { Colspan = 8 });
                table.AddCell(new PdfPCell(new Phrase(boleto.PagadorCpf, fonteValor)) { Colspan = 2 });

                table.AddCell(new PdfPCell(new Phrase(boleto.PagadorEmail, fonteValor)) { Colspan = 10 });

                table.AddCell(new PdfPCell(new Phrase(boleto.PagadorTelefone, fonteValor)) { Colspan = 10 });

                // Recibo do Sacado - Autenticação Mecânica
                table.AddCell(new PdfPCell(new Phrase("Recibo do Sacado - Autenticação Mecânica", fonteTitulo)) { Colspan = 11 });

                table.AddCell(new PdfPCell(new Phrase(" ", fonteValor)) { Colspan = 11, Border = Rectangle.NO_BORDER });

                document.Add(table);
                // Linha com código de barras
                //AddBarcode(document, boleto.CodigoBarras);
                var codigobarrasPath = Path.Combine(AppContext.BaseDirectory, "Assets", "codigobarras.png");
                iTextSharp.text.Image codigobarras = iTextSharp.text.Image.GetInstance(codigobarrasPath);
                codigobarras.ScaleAbsolute(500f, 50f);
                document.Add(codigobarras);

                document.Close();

                //Devolver o próprio arquivo
                //return File(memoryStream.ToArray(), "application/pdf", "boleto.pdf");

                var pdfBytes = memoryStream.ToArray();

                string base64Pdf = Convert.ToBase64String(pdfBytes);
                string dataUri = base64Pdf;

                return Ok(dataUri);
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
