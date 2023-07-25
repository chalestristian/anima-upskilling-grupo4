using System;
using System.Collections.Generic;
using System.Web.UI;
using Npgsql;
using Webforms2.Models;

namespace WebForms2.Telas
{
    public partial class CadastroCurso : Page
    {
        private List<CursoModels> cursos = new List<CursoModels>();
        private List<ModuloModels> modulos = new List<ModuloModels>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CarregarCursosDoBanco();
                CarregarModulosDoBanco();
            }
        }

        protected void btnCadastrar_Click(object sender, EventArgs e)
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

        protected void btnDeletar_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(ddlCursos.SelectedValue))
            {
                int idCurso = Convert.ToInt32(ddlCursos.SelectedValue);
                DeletarCursoDoBanco(idCurso);
            }
        }

        private void CarregarCursosDoBanco()
        {
            cursos.Clear();
            string connectionString = "Host=localhost;Port=54321;Username=postgres;Password=postgres;Database=UpskillingGrupo4Final";

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
                                int cargaHoraria = Convert.ToInt32(reader["CargaHoraria"]);
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

                    ddlCursos.DataSource = cursos;
                    ddlCursos.DataTextField = "NomeCurso";
                    ddlCursos.DataValueField = "IdCurso";
                    ddlCursos.DataBind();
                }
                catch (NpgsqlException ex)
                {
                    lblMensagem.Text = "Ocorreu um erro na conexão com o banco de dados: " + ex.Message;
                }
                catch (Exception ex)
                {
                    lblMensagem.Text = "Ocorreu um erro desconhecido: " + ex.Message;
                }
            }
        }

        private void AdicionarCursoAoBanco(CursoModels curso)
        {
            string connectionString = "Host=localhost;Port=54321;Username=postgres;Password=postgres;Database=UpskillingGrupo4Final";

            using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    string sql = "INSERT INTO \"Cursos\" (\"Nome\", \"CargaHoraria\", \"Valor\") VALUES (@Nome, @CargaHoraria, @Valor)";
                    using (NpgsqlCommand cmd = new NpgsqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@Nome", curso.NomeCurso);
                        cmd.Parameters.AddWithValue("@CargaHoraria", curso.CargaHoraria);
                        cmd.Parameters.AddWithValue("@Valor", curso.ValorCurso);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            lblMensagem.Text = "Curso adicionado com sucesso!";
                            CarregarCursosDoBanco();
                        }
                        else
                        {
                            lblMensagem.Text = "Ocorreu um erro ao adicionar o curso.";
                        }
                    }
                }
                catch (NpgsqlException ex)
                {
                    lblMensagem.Text = "Ocorreu um erro na conexão com o banco de dados: " + ex.Message;
                }
                catch (Exception ex)
                {
                    lblMensagem.Text = "Ocorreu um erro desconhecido: " + ex.Message;
                }
            }
        }

        private void DeletarCursoDoBanco(int idCurso)
        {
            string connectionString = "Host=localhost;Port=54321;Username=postgres;Password=postgres;Database=UpskillingGrupo4Final";

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
                            lblMensagem.Text = "Curso deletado com sucesso!";
                            CarregarCursosDoBanco();
                        }
                        else
                        {
                            lblMensagem.Text = "Ocorreu um erro ao deletar o curso.";
                        }
                    }
                }
                catch (NpgsqlException ex)
                {
                    lblMensagem.Text = "Ocorreu um erro na conexão com o banco de dados: " + ex.Message;
                }
                catch (Exception ex)
                {
                    lblMensagem.Text = "Ocorreu um erro desconhecido: " + ex.Message;
                }
            }
        }

        private void CarregarModulosDoBanco()
        {
            string connectionString = "Host=localhost;Port=54321;Username=postgres;Password=postgres;Database=UpskillingGrupo4Final";

            using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    string sql = "SELECT * FROM \"Modulos\"";
                    using (NpgsqlCommand cmd = new NpgsqlCommand(sql, con))
                    {
                        using (NpgsqlDataReader reader = cmd.ExecuteReader())
                        {
                            List<ModuloModels> modulos = new List<ModuloModels>();

                            while (reader.Read())
                            {
                                int idModulo = Convert.ToInt32(reader["ID"]);
                                string nomeModulo = reader["Nome"].ToString();
                                int cargaHorariaModulo = Convert.ToInt32(reader["CH"]);

                                ModuloModels modulo = new ModuloModels
                                {
                                    IdModulo = idModulo,
                                    NomeModulo = nomeModulo,
                                    CHModulo = cargaHorariaModulo
                                };

                                modulos.Add(modulo);
                            }

                            ddlModulos.DataSource = modulos;
                            ddlModulos.DataTextField = "NomeModulo";
                            ddlModulos.DataValueField = "IdModulo";
                            ddlModulos.DataBind();
                        }
                    }
                }
                catch (NpgsqlException ex)
                {
                    lblMensagem.Text = "Ocorreu um erro na conexão com o banco de dados: " + ex.Message;
                }
                catch (Exception ex)
                {
                    lblMensagem.Text = "Ocorreu um erro desconhecido: " + ex.Message;
                }
            }
        }



    }
}
