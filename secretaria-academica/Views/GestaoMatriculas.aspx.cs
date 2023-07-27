using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Npgsql;
using secretaria_academica.Models;
using System.Configuration;
using iTextSharp.text;

namespace secretaria_academica.Views
{
    public partial class GestaoMatriculas : Page
    {
        private List<MatriculaModels> matriculas = new List<MatriculaModels>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UsuarioAutenticado"] == null || !(bool)Session["UsuarioAutenticado"])
            {
                Response.Redirect("Login.aspx");
            }

            if (!IsPostBack)
            {
                CarregarMatriculas();
            }
        }

        private void CarregarMatriculas()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;

            using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    string sql = "SELECT m.*, c.*, c.\"Nome\" as cursoNome, p.\"Nome\" as pessoaNome, P.\"CPF\", P.\"Email\" " +
                                 " FROM \"Matriculas\" m " +
                                 "INNER JOIN \"Alunos\" a ON m.\"AlunoId\" = a.\"Id\" " +
                                 "INNER JOIN \"Cursos\" c ON m.\"CursoId\" = c.\"Id\" " +
                                 "INNER JOIN \"Pessoas\" p ON a.\"PessoaId\" = p.\"Id\" " +
                                 " ORDER BY m.\"Id\" ";

                    using (NpgsqlCommand cmd = new NpgsqlCommand(sql, con))
                    {
                        using (NpgsqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int idMatricula = Convert.ToInt32(reader["Id"]);
                                string matriculaAluno = reader["Id"].ToString();
                                DateTime dataMatricula = Convert.ToDateTime(reader["DataMatricula"]);
                                decimal valorMatricula = Convert.ToDecimal(reader["ValorMatricula"]);

                                DateTime dataConclusao = default(DateTime);
                                decimal media = default(decimal);
                                string status = default(string);
                                bool matriculaConfirmada = default(bool);

                                if (!Convert.IsDBNull(reader["DataConclusao"]))
                                {
                                    dataConclusao = Convert.ToDateTime(reader["DataConclusao"]);
                                }
                                if (!Convert.IsDBNull(reader["Media"]))
                                {
                                    media = Convert.ToDecimal(reader["Media"]);
                                }
                                if (!Convert.IsDBNull(reader["Status"]))
                                {
                                    status = reader["Status"].ToString();
                                }
                                if (!Convert.IsDBNull(reader["MatriculaConfirmada"]))
                                {
                                    matriculaConfirmada = Convert.ToBoolean(reader["MatriculaConfirmada"]);
                                }

                                int idAluno = Convert.ToInt32(reader["AlunoId"]);
                                string nomeAluno = reader["pessoaNome"].ToString();
                                string cpfAluno = reader["CPF"].ToString();
                                string emailAluno = reader["Email"].ToString();

                                int idCurso = Convert.ToInt32(reader["CursoId"]);
                                string nomeCurso = reader["cursoNome"].ToString();
                                int? cargaHoraria = reader["CH"] != DBNull.Value ? Convert.ToInt32(reader["CH"]) : (int?)null;
                                decimal? valorCurso = reader["Valor"] != DBNull.Value ? Convert.ToDecimal(reader["Valor"]) : (decimal?)null;

                                AlunoModels aluno = new AlunoModels
                                {
                                    Id = idAluno,
                                    Pessoa = new PessoaModels
                                    {
                                        NomePessoa = nomeAluno,
                                        CPF = cpfAluno,
                                        Email = emailAluno
                                    },
                                    Matricula = matriculaAluno
                                };

                                CursoModels curso = new CursoModels
                                {
                                    IdCurso = idCurso,
                                    NomeCurso = nomeCurso,
                                    CargaHoraria = cargaHoraria,
                                    ValorCurso = valorCurso
                                };

                                MatriculaModels matricula = new MatriculaModels
                                {
                                    Id = idMatricula,
                                    Aluno = aluno,
                                    Curso = curso,
                                    DataMatricula = dataMatricula,
                                    ValorMatricula = valorMatricula,
                                    DataConclusao = dataConclusao,
                                    Media = media,
                                    Status = status,
                                    MatriculaConfirmada = matriculaConfirmada
                                };

                                matriculas.Add(matricula);
                            }
                        }
                    }

                    gridAlunos.DataSource = matriculas;
                    gridAlunos.DataBind();
                }
                catch (NpgsqlException ex)
                {
                    lblMensagemErro.Text = "Ocorreu um erro na conexão com o banco de dados: " + ex.Message;
                }
                catch (Exception ex)
                {
                    lblMensagemErro.Text = "Ocorreu um erro desconhecido: " + ex.Message;
                }
            }
        }

        protected void gridAlunos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                MatriculaModels matricula = (MatriculaModels)e.Row.DataItem;
                Button btnConfirmarMatricula = (Button)e.Row.FindControl("btnConfirmarMatricula");
                Button btnCancelarMatricula = (Button)e.Row.FindControl("btnCancelarMatricula");

                if (btnConfirmarMatricula != null && btnCancelarMatricula != null)
                {
                    btnConfirmarMatricula.Visible = !matricula.MatriculaConfirmada;
                    btnCancelarMatricula.Visible = matricula.MatriculaConfirmada;
                }
            }
        }

        protected void gridAlunos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "ConfirmarMatricula")
            {
                int idMatricula = Convert.ToInt32(e.CommandArgument);
                ConfirmarMatricula(idMatricula);
            }
            else if (e.CommandName == "CancelarMatricula")
            {
                int idMatricula = Convert.ToInt32(e.CommandArgument);
                CancelarMatricula(idMatricula);
            }

            CarregarMatriculas();
        }

        protected void gridAlunos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridAlunos.PageIndex = e.NewPageIndex;
            gridAlunos.DataBind();
        }

        protected void btnVoltarParaTelaAnterior_Click(object sender, EventArgs e)
        {
            Response.Redirect("PaginaInicial.aspx");
        }

        private bool ConfirmarMatricula(int idMatricula)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;

            using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
            {
                try
                {
                    con.Open();

                    string sql = "UPDATE \"Matriculas\" SET \"MatriculaConfirmada\" = true WHERE \"Id\" = @IdMatricula";

                    using (NpgsqlCommand cmd = new NpgsqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@IdMatricula", idMatricula);

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            return true;
                        }
                    }
                }
                catch (NpgsqlException ex)
                {
                }
                catch (Exception ex)
                {
                }
                return false;
            }
        }

        private bool CancelarMatricula(int idMatricula)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;

            using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
            {
                try
                {
                    con.Open();

                    string sql = "UPDATE \"Matriculas\" SET \"MatriculaConfirmada\" = false WHERE \"Id\" = @IdMatricula";

                    using (NpgsqlCommand cmd = new NpgsqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@IdMatricula", idMatricula);

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            return true;
                        }
                    }
                }
                catch (NpgsqlException ex)
                {
                }
                catch (Exception ex)
                {
                }
                return false;
            }
        }


    }


}
