using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Npgsql;
using secretaria_academica.Models;
using System.Configuration;

namespace secretaria_academica.Views
{
    public partial class CadastroModulos : Page
    {
        private int idCurso;
        private List<ModuloModels> modulos = new List<ModuloModels>();
        private CursoModels curso;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UsuarioAutenticado"] == null || !(bool)Session["UsuarioAutenticado"])
            {
                Response.Redirect("Login.aspx");
            }

            if (Request.QueryString["idCurso"] != null)
            {
                idCurso = Convert.ToInt32(Request.QueryString["idCurso"]);
                curso = CursoModels.getByID(idCurso);
            }
            else
            {
                Response.Redirect($"CadastroModulos.aspx?idCurso={idCurso}");
            }

            if (!IsPostBack)
            {
                CarregarModulosDoBanco();
            }
        }

        protected void btnCadastrarModulo_Click(object sender, EventArgs e)
        {
            string nomeModulo = txtNomeModulo.Text;
            int? cargaHoraria = null;

            if (!string.IsNullOrEmpty(txtCargaHoraria.Text))
                cargaHoraria = Convert.ToInt32(txtCargaHoraria.Text);

            ModuloModels modulo = new ModuloModels
            {
                Curso = curso,
                NomeModulo = nomeModulo,
                CHModulo = cargaHoraria
            };

            AdicionarModuloAoBanco(modulo);
        }

        private void CarregarModulosDoBanco()
        {
            txtNomeModulo.Text = string.Empty;
            txtCargaHoraria.Text = string.Empty;

            txtNomeModuloEdicao.Text = string.Empty;
            txtCargaHorariaEdicao.Text = string.Empty;

            modulos.Clear();
            string connectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;

            using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    string sql = "SELECT * FROM \"ModulosCursos\" where \"CursoId\" = @CursoId";
                    using (NpgsqlCommand cmd = new NpgsqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@CursoId", idCurso);
                        using (NpgsqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int idModulo = Convert.ToInt32(reader["Id"]);
                                int cargaHoraria = Convert.ToInt32(reader["CH"]);
                                string nomeModulo = Convert.ToString(reader["Nome"]);

                                ModuloModels modulo = new ModuloModels
                                {
                                    IdModulo = idModulo,
                                    Curso = curso,
                                    NomeModulo = nomeModulo,
                                    CHModulo = cargaHoraria,
                                };

                                modulos.Add(modulo);
                            }
                        }
                    }

                    gridModulos.DataSource = modulos;
                    gridModulos.DataBind();
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

        private void AdicionarModuloAoBanco(ModuloModels modulo)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;

            using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    string sql = "INSERT INTO \"ModulosCursos\" (\"Nome\", \"CH\", \"CursoId\") VALUES (@Nome, @CH, @CursoId)";
                    using (NpgsqlCommand cmd = new NpgsqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@Nome", modulo.NomeModulo);
                        cmd.Parameters.AddWithValue("@CH", modulo.CHModulo);
                        cmd.Parameters.AddWithValue("@CursoId", idCurso);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            lblMensagemSucesso.Text = "Módulo adicionado com sucesso!";
                            CarregarModulosDoBanco();
                        }
                        else
                        {
                            lblMensagemErro.Text = "Ocorreu um erro ao adicionar o módulo.";
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

        private void DeletarModuloDoBanco(int idModulo)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;

            using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    string sql = "DELETE FROM \"ModulosCursos\" WHERE \"Id\" = @IdModulo";
                    using (NpgsqlCommand cmd = new NpgsqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@IdModulo", idModulo);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            lblMensagemSucesso.Text = "Módulo excluído com sucesso!";
                            CarregarModulosDoBanco();
                        }
                        else
                        {
                            lblMensagemErro.Text = "Ocorreu um erro ao deletar o módulo.";
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

        protected void gridModulos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int idModulo = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName == "Editar")
            {
                ModuloModels modulo = ObterModuloPorId(idModulo);
                if (modulo != null)
                {
                    hdnIdModulo.Value = idModulo.ToString();
                    txtNomeModuloEdicao.Text = modulo.NomeModulo;
                    txtCargaHorariaEdicao.Text = modulo.CHModulo?.ToString();

                    divCadastro.Visible = false;
                    divEdicao.Visible = true;
                }
            }
            else if (e.CommandName == "Excluir")
            {
                DeletarModuloDoBanco(idModulo);
                CarregarModulosDoBanco();
            }
        }

        protected void gridModulos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridModulos.PageIndex = e.NewPageIndex;
            gridModulos.DataBind();
        }

        protected void btnCancelarEdicao_Click(object sender, EventArgs e)
        {
            txtNomeModuloEdicao.Text = string.Empty;
            txtCargaHorariaEdicao.Text = string.Empty;

            txtNomeModulo.Text = string.Empty;
            txtCargaHoraria.Text = string.Empty;

            divCadastro.Visible = true;
            divEdicao.Visible = false;
        }

        protected void btnSalvarEdicao_Click(object sender, EventArgs e)
        {
            int idCurso = Convert.ToInt32(hdnIdModulo.Value);

            ModuloModels moduloAtual = ObterModuloPorId(idCurso);
            if (moduloAtual != null)
            {
                moduloAtual.NomeModulo = txtNomeModuloEdicao.Text;
                if (!string.IsNullOrEmpty(txtCargaHorariaEdicao.Text))
                    moduloAtual.CHModulo = Convert.ToInt32(txtCargaHorariaEdicao.Text);
                else
                    moduloAtual.CHModulo = null;

                // Salvar as alterações no banco de dados (ou na lista de módulos)
                AtualizarModuloNoBanco(moduloAtual);

                // Limpar o formulário de edição
                txtNomeModuloEdicao.Text = string.Empty;
                txtCargaHorariaEdicao.Text = string.Empty;

                // Mostrar o formulário de cadastro e ocultar o formulário de edição
                divCadastro.Visible = true;
                divEdicao.Visible = false;

                // Atualizar a lista de módulos exibida no GridView
                CarregarModulosDoBanco();
            }
        }

        private ModuloModels ObterModuloPorId(int idModulo)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
            ModuloModels modulo = null;

            using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    string sql = "SELECT * FROM \"ModulosCursos\" WHERE \"Id\" = @IdModulo";
                    using (NpgsqlCommand cmd = new NpgsqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@IdModulo", idModulo);

                        using (NpgsqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                modulo = new ModuloModels
                                {
                                    IdModulo = Convert.ToInt32(reader["Id"]),
                                    Curso = CursoModels.getByID(Convert.ToInt32(reader["CursoId"])),
                                    NomeModulo = reader["Nome"].ToString(),
                                    CHModulo = Convert.ToInt32(reader["CH"]),
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

            return modulo;
        }

        private void AtualizarModuloNoBanco(ModuloModels modulo)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;

            using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    string sql = "UPDATE \"ModulosCursos\" SET \"Nome\" = @Nome, \"CH\" = @CH, \"CursoId\" = @CursoId WHERE \"Id\" = @IdModulo";
                    using (NpgsqlCommand cmd = new NpgsqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@IdModulo", modulo.IdModulo);
                        cmd.Parameters.AddWithValue("@Nome", modulo.NomeModulo);
                        cmd.Parameters.AddWithValue("@CH", modulo.CHModulo);
                        cmd.Parameters.AddWithValue("@CursoId", modulo.Curso.IdCurso);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            lblMensagemSucesso.Text = "Módulo atualizado com sucesso!";
                        }
                        else
                        {
                            lblMensagemErro.Text = "Ocorreu um erro ao atualizar o módulo.";
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
            Response.Redirect("CadastroCurso.aspx");
        }
    }


}
