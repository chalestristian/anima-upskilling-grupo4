using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using Webforms2.Models;

namespace WebForms2.Telas
{
    public partial class RelatorioPDF : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }
        [Obsolete]
        protected void btnAlunosCadastrados_Click(object sender, EventArgs e)
        {
            List<AlunosModels> alunos = ObterAlunosCadastrados();
            GerarRelatorioPDF("Alunos Cadastrados", alunos);
        }
        [Obsolete]
        protected void btnCursosCadastrados_Click(object sender, EventArgs e)
        {
            List<CursoModels> cursos = ObterCursosCadastrados();
            GerarRelatorioPDF("Cursos Cadastrados", cursos);
        }

        [Obsolete]
        protected void btnNotasLancadas_Click(object sender, EventArgs e)
        {
            List<NotaModels> notas = ObterNotasLancadas();
            GerarRelatorioPDF("Notas Lançadas", notas);
        }

        private List<AlunosModels> ObterAlunosCadastrados()
        {
            List<AlunosModels> alunos = new List<AlunosModels>();

            string connectionString = "Host=localhost;Port=54321;Username=postgres;Password=postgres;Database=UpskillingGrupo4Final";
            string query = "SELECT * FROM Alunos";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        AlunosModels aluno = new AlunosModels
                        {
                            RA = Convert.ToInt32(reader["RA"]),
                            Nome = reader["Nome"].ToString()
                        };
                        alunos.Add(aluno);
                    }

                    reader.Close();
                }
            }

            return alunos;
        }

        private List<CursoModels> ObterCursosCadastrados()
        {
            List<CursoModels> cursos = new List<CursoModels>();

            string connectionString = "Host=localhost;Port=54321;Username=postgres;Password=postgres;Database=UpskillingGrupo4Final";
            string query = "SELECT * FROM Cursos";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        CursoModels curso = new CursoModels
                        {
                            IdCurso = Convert.ToInt32(reader["Id"]),
                            NomeCurso = reader["Nome"].ToString()
                        };
                        cursos.Add(curso);
                    }

                    reader.Close();
                }
            }

            return cursos;
        }

        private List<NotaModels> ObterNotasLancadas()
        {
            List<NotaModels> notas = new List<NotaModels>();

            string connectionString = "Host=localhost;Port=54321;Username=postgres;Password=postgres;Database=UpskillingGrupo4Final";
            string query = "SELECT * FROM tabela_notas";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        NotaModels nota = new NotaModels
                        {
                            RA = Convert.ToInt32(reader["ra_aluno"]),
                            Nota = Convert.ToDecimal(reader["nota"]),
                            DataLancamento = Convert.ToDateTime(reader["data_lancamento"])
                        };
                        notas.Add(nota);
                    }

                    reader.Close();
                }
            }

            return notas;
        }

        [Obsolete]
        private void GerarRelatorioPDF<T>(string titulo, List<T> data)
        {
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=Relatorio.pdf");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);

            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            GridView gv = new GridView();

            gv.DataSource = data;
            gv.DataBind();

            gv.RenderControl(hw);
            StringReader sr = new StringReader(sw.ToString());
            Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
            HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
            PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
            pdfDoc.Open();

            Paragraph paragraph = new Paragraph(titulo, new Font(Font.FontFamily.HELVETICA, 20, Font.BOLD));
            paragraph.Alignment = Element.ALIGN_CENTER;
            pdfDoc.Add(paragraph);

            htmlparser.Parse(sr);
            pdfDoc.Close();
            Response.Write(pdfDoc);
            Response.End();
        }
    }
}
