using System;
using System.Collections.Generic;
using System.IO;
using System.Web.UI;
using Webforms2.Models;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace WebForms2.Telas
{
    public partial class Relatorio : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void btnGerarRelatorio_Click(object sender, EventArgs e)
        {
            // Simulação dos dados dos cursos > Buscar do banco
            var cursos = new List<CursoModels>
            {
                new CursoModels { IdCurso = 1, NomeCurso = "Curso A", CargaHoraria = 40, ValorCurso = 100.00M },
                new CursoModels { IdCurso = 2, NomeCurso = "Curso B", CargaHoraria = 30, ValorCurso = 150.00M },
                new CursoModels { IdCurso = 3, NomeCurso = "Curso C", CargaHoraria = 50, ValorCurso = 200.00M }
            };

            var alunos = new List<AlunosModels>
            {
                new AlunosModels { RA = 123, Nome = "Aluno 1", Id = new PessoaModels { IdPessoa = 1, NomePessoa = "Pessoa 1", CPF = "111.111.111-11", Celular = "(11) 1111-1111", Email = "pessoa1@example.com", DataNascimento = DateTime.Now.AddYears(-25) }, NomeCurso = cursos[0], DataCadastro = DateTime.Now },
                new AlunosModels { RA = 456, Nome = "Aluno 2", Id = new PessoaModels { IdPessoa = 2, NomePessoa = "Pessoa 2", CPF = "222.222.222-22", Celular = "(22) 2222-2222", Email = "pessoa2@example.com", DataNascimento = DateTime.Now.AddYears(-30) }, NomeCurso = cursos[1], DataCadastro = DateTime.Now },
                new AlunosModels { RA = 789, Nome = "Aluno 3", Id = new PessoaModels { IdPessoa = 3, NomePessoa = "Pessoa 3", CPF = "333.333.333-33", Celular = "(33) 3333-3333", Email = "pessoa3@example.com", DataNascimento = DateTime.Now.AddYears(-28) }, NomeCurso = cursos[1], DataCadastro = DateTime.Now },
                new AlunosModels { RA = 101, Nome = "Aluno 4", Id = new PessoaModels { IdPessoa = 4, NomePessoa = "Pessoa 4", CPF = "444.444.444-44", Celular = "(44) 4444-4444", Email = "pessoa4@example.com", DataNascimento = DateTime.Now.AddYears(-22) }, NomeCurso = null, DataCadastro = DateTime.Now }
            };

            // Gera e envia o arquivo PDF para download no navegador.
            byte[] pdfBytes = GeneratePDF(cursos, alunos);
            
            Response.Clear();
            Response.ContentType = "application/pdf";
            Response.Headers.Add("Content-Disposition", "inline; filename=RelatorioCursosAlunos.pdf");
            Response.BinaryWrite(pdfBytes);
            Response.Flush();
            Response.End();
        }

        private byte[] GeneratePDF(List<CursoModels> cursos, List<AlunosModels> alunos)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                Document document = new Document();
                PdfWriter.GetInstance(document, stream);
                document.Open();
                
                Font fontTitulo = new Font(Font.FontFamily.HELVETICA, 16, Font.BOLD);
                
                Font fontConteudo = new Font(Font.FontFamily.HELVETICA, 12, Font.NORMAL);

                foreach (var curso in cursos)
                {
                    document.Add(new Paragraph($"ID do Curso: {curso.IdCurso}, Nome do Curso: {curso.NomeCurso}, Carga Horária: {curso.CargaHoraria}, Valor do Curso: {curso.ValorCurso:C}", fontTitulo));

                    var alunosMatriculados = alunos.FindAll(a => a.NomeCurso?.IdCurso == curso.IdCurso);

                    if (alunosMatriculados.Count > 0)
                    {
                        foreach (var aluno in alunosMatriculados)
                        {
                            var conteudo = new Paragraph($"RA: {aluno.RA}, Nome do Aluno: {aluno.Nome}, ID do Aluno: {aluno.Id.IdPessoa}, Data de Cadastro: {aluno.DataCadastro.ToShortDateString()}", fontConteudo);
                            document.Add(conteudo);
                        }
                    }
                    else
                    {
                        document.Add(new Paragraph("Nenhum aluno matriculado neste curso.", fontConteudo));
                    }

                    document.Add(new Paragraph("")); // adc uma linha p separar os cursos
                }

                document.Close();
                return stream.ToArray();
            }
        }
    }
}
