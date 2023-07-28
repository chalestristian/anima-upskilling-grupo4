using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using api.Models;
using api.Data;
using Microsoft.AspNetCore.Authorization;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Text;
using RabbitMQ.Client;

namespace api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CertificadoController : ControllerBase
    {

        private readonly AppDbContext _dbContext;

        public CertificadoController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost("GerarCertificado/{matriculaId}")]
        public IActionResult GerarCertificado(int matriculaId)
        {
            try
            {
                var matricula = _dbContext.Matriculas
                    .Include(m => m.Aluno.Pessoa)
                    .Include(m => m.Curso)
                    .SingleOrDefault(m => m.Id == matriculaId);

                if (matricula == null)
                {
                    return NotFound(new { Message = "MatriculaId não encontrada." });
                }

                string nomeAluno = matricula.Aluno?.Pessoa?.Nome;
                string cpfAluno = matricula.Aluno?.Pessoa?.CPF;
                string nomeCurso = matricula.Curso?.Nome;
                int cargaHoraria = matricula.Curso?.CH ?? 0;
                string dataConclusaoCurso = matricula.DataConclusao?.ToString("dd/MM/yyyy") ?? "";

                Document document = new Document(new Rectangle(792f, 612f));

                var memoryStream = new MemoryStream();

                PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);

                document.Open();

                var backgroundImage = Image.GetInstance(Path.Combine(AppContext.BaseDirectory, "Assets", "fundo-certificado.jpg"));
                backgroundImage.SetAbsolutePosition(0, 0);
                backgroundImage.ScaleAbsolute(document.PageSize.Width, document.PageSize.Height);
                document.Add(backgroundImage);

                var titleFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 48);
                var title = new Paragraph("", titleFont);
                title.Alignment = Element.ALIGN_CENTER;
                title.SpacingAfter = 150;
                document.Add(title);

                var contentFont = FontFactory.GetFont(FontFactory.HELVETICA, 30);
                var content = new Paragraph("Certificamos que o aluno " + nomeAluno + " (CPF " + cpfAluno + ") concluiu o curso  " + nomeCurso + " com carga horária de " + cargaHoraria + " horas", contentFont);
                content.Alignment = Element.ALIGN_CENTER;
                content.SpacingAfter = 60;
                document.Add(content);

                var dateFont = FontFactory.GetFont(FontFactory.HELVETICA, 16);
                var date = new Paragraph(DateTime.Now.ToString("dd/MM/yyyy"), dateFont);
                date.Alignment = Element.ALIGN_CENTER;
                document.Add(date);

                document.Close();

                var pdfBytes = memoryStream.ToArray();

                string base64Pdf = Convert.ToBase64String(pdfBytes);
                string dataUri = base64Pdf;
                matricula.Certificado = dataUri;

                _dbContext.SaveChanges();

                //Mostra o PDF em tela no retorno
                //var fileContentResult = new FileContentResult(pdfBytes, "application/pdf");
                //fileContentResult.FileDownloadName = "certificado" + matriculaId + ".pdf";
                //return fileContentResult;

                return Ok(new { Message = "Certificado gerado com sucesso! ", UrlCertificado = dataUri });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                return BadRequest(new { Message = "Não encontrada matrícula do aluno para geração do certificado." });
            }
        }

        [HttpPost("SolicitarCertificado/{matriculaId}")]
        public IActionResult SolicitarCertificado(int matriculaId)
        {
            try
            {
                if (matriculaId == null)
                {
                    return BadRequest(new { Message = "Matrícula inválida" });
                }

                var matricula = _dbContext.Matriculas
                    .SingleOrDefault(m => m.Id == matriculaId);

                if (matricula == null)
                {
                    return NotFound(new { Message = "Matrícula não encontrada" });
                }

                var factory = new ConnectionFactory() { HostName = "service-rabbitmq", Port = 5672 };
                //var factory = new ConnectionFactory() { HostName = "localhost", Port = 56721 };

                using (var connection = factory.CreateConnection())
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: "certificado-queue",
                                        durable: true,
                                        exclusive: false,
                                        autoDelete: false,
                                        arguments: null);

                    var message = matriculaId.ToString();
                    var body = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish(exchange: "",
                                         routingKey: "certificado-queue",
                                         basicProperties: null,
                                         body: body);
                }

                matricula.DataSolicitacaoCertificado = DateTime.UtcNow;
                _dbContext.SaveChanges();

                return Ok(new { Message = "Solicitação de certificado enviada com sucesso!" });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                return BadRequest(new { Message = "Falha ao enviar solicitação de certificado." });
            }
        }

        [HttpGet("GetUrlCertificado/{matriculaId}")]
        public IActionResult GetUrlCertificado(int matriculaId)
        {
            try
            {
                if (matriculaId == null)
                {
                    return BadRequest(new { Message = "Matrícula inválida" });
                }

                var matricula = _dbContext.Matriculas
                    .SingleOrDefault(m => m.Id == matriculaId);

                if (matricula == null)
                {
                    return NotFound(new { Message = "Matrícula não encontrada" });
                }

                string certificadoUrl = matricula.Certificado;
                return Ok(certificadoUrl);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                return BadRequest(new { Message = "Erro ao buscar certificado." });
            }
        }
    }
}
