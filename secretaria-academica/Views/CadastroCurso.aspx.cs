﻿using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Npgsql;
using secretaria_academica.Models;
using System.Configuration;

namespace secretaria_academica.Views
{
    public partial class CadastroCurso : Page
    {
        private List<CursoModels> cursos = new List<CursoModels>();
        private List<ModuloModels> modulos = new List<ModuloModels>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UsuarioAutenticado"] == null || !(bool)Session["UsuarioAutenticado"])
            {
                Response.Redirect("Login.aspx");
            }

            if (!IsPostBack)
            {
                CarregarCursosDoBanco();
            }
        }

        protected void btnCadastrarCurso_Click(object sender, EventArgs e)
        {
            string nomeCurso = txtNomeCurso.Text;
            int? cargaHoraria = null;
            decimal? valor = null;

            if (!string.IsNullOrEmpty(txtCargaHoraria.Text))
                cargaHoraria = Convert.ToInt32(txtCargaHoraria.Text);

            if (!string.IsNullOrEmpty(txtValor.Text))
                valor = Convert.ToDecimal(txtValor.Text);

            CursoModels curso = new CursoModels
            {
                NomeCurso = nomeCurso,
                CargaHoraria = cargaHoraria,
                ValorCurso = valor
            };

            AdicionarCursoAoBanco(curso);
        }

        private void CarregarCursosDoBanco()
        {
            cursos.Clear();
            string connectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;

            using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    string sql = "SELECT * FROM \"Cursos\"";
                    using (NpgsqlCommand cmd = new NpgsqlCommand(sql, con))
                    {
                        using (NpgsqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int idCurso = Convert.ToInt32(reader["Id"]);
                                string nomeCurso = reader["Nome"].ToString();
                                int cargaHoraria = Convert.ToInt32(reader["CH"]);
                                decimal valor = Convert.ToDecimal(reader["Valor"]);

                                CursoModels curso = new CursoModels
                                {
                                    IdCurso = idCurso,
                                    NomeCurso = nomeCurso,
                                    CargaHoraria = cargaHoraria,
                                    ValorCurso = valor
                                };

                                cursos.Add(curso);
                            }
                        }
                    }

                    gridCursos.DataSource = cursos;
                    gridCursos.DataBind();
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

        private void AdicionarCursoAoBanco(CursoModels curso)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;

            using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    string sql = "INSERT INTO \"Cursos\" (\"Nome\", \"CH\", \"Valor\") VALUES (@Nome, @CH, @Valor)";
                    using (NpgsqlCommand cmd = new NpgsqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@Nome", curso.NomeCurso);
                        cmd.Parameters.AddWithValue("@CH", curso.CargaHoraria);
                        cmd.Parameters.AddWithValue("@Valor", curso.ValorCurso);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            lblMensagemSucesso.Text = "Curso adicionado com sucesso!";
                            CarregarCursosDoBanco();
                        }
                        else
                        {
                            lblMensagemErro.Text = "Ocorreu um erro ao adicionar o curso.";
                        }
                    }
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

        private void DeletarCursoDoBanco(int idCurso)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;

            using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    string sql = "DELETE FROM \"Cursos\" WHERE \"Id\" = @IdCurso";
                    using (NpgsqlCommand cmd = new NpgsqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@IdCurso", idCurso);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            lblMensagemSucesso.Text = "Curso excluído com sucesso!";
                            CarregarCursosDoBanco();
                        }
                        else
                        {
                            lblMensagemErro.Text = "Ocorreu um erro ao deletar o curso.";
                        }
                    }
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

        protected void gridCursos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int idCurso = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName == "Editar")
            {
                CursoModels curso = ObterCursoPorId(idCurso);
                if (curso != null)
                {
                    hdnIdCurso.Value = idCurso.ToString();
                    txtNomeCursoEdicao.Text = curso.NomeCurso;
                    txtCargaHorariaEdicao.Text = curso.CargaHoraria?.ToString();
                    txtValorEdicao.Text = curso.ValorCurso?.ToString();

                    divCadastro.Visible = false;
                    divEdicao.Visible = true;
                }
            }
            else if (e.CommandName == "GerenciarModulos")
            {
                Response.Redirect($"CadastroModulos.aspx?idCurso={idCurso}");
            }
            else if (e.CommandName == "Excluir")
            {
                DeletarCursoDoBanco(idCurso);
                CarregarCursosDoBanco();
            }
        }

        protected void gridCursos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridCursos.PageIndex = e.NewPageIndex;
            gridCursos.DataBind();
        }

        protected void btnCancelarEdicao_Click(object sender, EventArgs e)
        {
            txtNomeCursoEdicao.Text = string.Empty;
            txtCargaHorariaEdicao.Text = string.Empty;
            txtValorEdicao.Text = string.Empty;

            divCadastro.Visible = true;
            divEdicao.Visible = false;
        }

        protected void btnSalvarEdicao_Click(object sender, EventArgs e)
        {
            int idCurso = Convert.ToInt32(hdnIdCurso.Value);

            CursoModels cursoAtual = ObterCursoPorId(idCurso);
            if (cursoAtual != null)
            {
                cursoAtual.NomeCurso = txtNomeCursoEdicao.Text;
                if (!string.IsNullOrEmpty(txtCargaHorariaEdicao.Text))
                    cursoAtual.CargaHoraria = Convert.ToInt32(txtCargaHorariaEdicao.Text);
                else
                    cursoAtual.CargaHoraria = null;

                if (!string.IsNullOrEmpty(txtValorEdicao.Text))
                    cursoAtual.ValorCurso = Convert.ToDecimal(txtValorEdicao.Text);
                else
                    cursoAtual.ValorCurso = null;

                // Salvar as alterações no banco de dados (ou na lista de cursos)
                AtualizarCursoNoBanco(cursoAtual);

                // Limpar o formulário de edição
                txtNomeCursoEdicao.Text = string.Empty;
                txtCargaHorariaEdicao.Text = string.Empty;
                txtValorEdicao.Text = string.Empty;

                // Mostrar o formulário de cadastro e ocultar o formulário de edição
                divCadastro.Visible = true;
                divEdicao.Visible = false;

                // Atualizar a lista de cursos exibida no GridView
                CarregarCursosDoBanco();
            }
        }

        private CursoModels ObterCursoPorId(int idCurso)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
            CursoModels curso = null;

            using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    string sql = "SELECT * FROM \"Cursos\" WHERE \"Id\" = @IdCurso";
                    using (NpgsqlCommand cmd = new NpgsqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@IdCurso", idCurso);

                        using (NpgsqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                curso = new CursoModels
                                {
                                    IdCurso = Convert.ToInt32(reader["Id"]),
                                    NomeCurso = reader["Nome"].ToString(),
                                    CargaHoraria = Convert.ToInt32(reader["CH"]),
                                    ValorCurso = Convert.ToDecimal(reader["Valor"])
                                };
                            }
                        }
                    }
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

            return curso;
        }

        private void AtualizarCursoNoBanco(CursoModels curso)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;

            using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    string sql = "UPDATE \"Cursos\" SET \"Nome\" = @Nome, \"CH\" = @CH, \"Valor\" = @Valor WHERE \"Id\" = @IdCurso";
                    using (NpgsqlCommand cmd = new NpgsqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@IdCurso", curso.IdCurso);
                        cmd.Parameters.AddWithValue("@Nome", curso.NomeCurso);
                        cmd.Parameters.AddWithValue("@CH", curso.CargaHoraria);
                        cmd.Parameters.AddWithValue("@Valor", curso.ValorCurso);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            lblMensagemSucesso.Text = "Curso atualizado com sucesso!";
                        }
                        else
                        {
                            lblMensagemErro.Text = "Ocorreu um erro ao atualizar o curso.";
                        }
                    }
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

        protected void btnVoltarParaTelaAnterior_Click(object sender, EventArgs e)
        {
            Response.Redirect("PaginaInicial.aspx");
        }
    }


}
