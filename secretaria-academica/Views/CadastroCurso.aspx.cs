using System;
using System.Collections.Generic;
using System.Web.UI;
using Npgsql;
using secretaria_academica.Models;

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
                //CarregarModulosDoBanco();
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

        protected void btnDeletar_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(ddlCursos.SelectedValue))
            {
                int idCurso = Convert.ToInt32(ddlCursos.SelectedValue);
                DeletarCursoDoBanco(idCurso);
            }
        }

        protected void btnCadastrarModulo_Click(object sender, EventArgs e)
        {
            int cursoId = Convert.ToInt32(ddlCursosModulo.SelectedValue);
            string nomeModulo = txtNomeModulo.Text;
            int cargaHorariaModulo = Convert.ToInt32(txtCargaHorariaModulo.Text);

            ModuloModels modulo = new ModuloModels
            {   
                Curso = new CursoModels { IdCurso = cursoId},
                NomeModulo = nomeModulo,
                CHModulo = cargaHorariaModulo
            };

            AdicionarModuloAoBanco(modulo);
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

                    ddlCursos.DataSource = cursos;
                    ddlCursos.DataTextField = "NomeCurso";
                    ddlCursos.DataValueField = "IdCurso";
                    ddlCursos.DataBind();

                    ddlCursosModulo.DataSource = cursos;
                    ddlCursosModulo.DataTextField = "NomeCurso";
                    ddlCursosModulo.DataValueField = "IdCurso";
                    ddlCursosModulo.DataBind();
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
                    string sql = "INSERT INTO \"Cursos\" (\"Nome\", \"CH\", \"Valor\") VALUES (@Nome, @CH, @Valor)";
                    using (NpgsqlCommand cmd = new NpgsqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@Nome", curso.NomeCurso);
                        cmd.Parameters.AddWithValue("@CH", curso.CargaHoraria);
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

        /*private void CarregarModulosDoBanco()
        {
            string connectionString = "Host=localhost;Port=54321;Username=postgres;Password=postgres;Database=UpskillingGrupo4Final";

            using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    string sql = "SELECT * FROM \"ModulosCursos\"";
                    using (NpgsqlCommand cmd = new NpgsqlCommand(sql, con))
                    {
                        using (NpgsqlDataReader reader = cmd.ExecuteReader())
                        {
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
                        }
                    }

                    ddlCursosModulo.DataSource = modulos;
                    ddlCursosModulo.DataTextField = "NomeModulo";
                    ddlCursosModulo.DataValueField = "IdModulo";
                    ddlCursosModulo.DataBind();
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
        }*/

        private void AdicionarModuloAoBanco(ModuloModels modulo)
        {
            string connectionString = "Host=localhost;Port=54321;Username=postgres;Password=postgres;Database=UpskillingGrupo4Final";

            using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    string sql = "INSERT INTO \"ModulosCursos\" (\"CursoId\", \"Nome\", \"CH\") VALUES (@IdCurso, @Nome, @CH)";
                    using (NpgsqlCommand cmd = new NpgsqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@IdCurso", modulo.Curso.IdCurso);
                        cmd.Parameters.AddWithValue("@Nome", modulo.NomeModulo);
                        cmd.Parameters.AddWithValue("@CH", modulo.CHModulo);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            lblMensagemModulo.Text = "Módulo cadastrado com sucesso!";
                            //CarregarModulosDoBanco();
                        }
                        else
                        {
                            lblMensagemModulo.Text = "Ocorreu um erro ao cadastrar o módulo.";
                        }
                    }
                }
                catch (NpgsqlException ex)
                {
                    lblMensagemModulo.Text = "Ocorreu um erro na conexão com o banco de dados: " + ex.Message;
                }
                catch (Exception ex)
                {
                    lblMensagemModulo.Text = "Ocorreu um erro desconhecido: " + ex.Message;
                }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
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
    }
}
