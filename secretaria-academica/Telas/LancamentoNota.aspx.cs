using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Npgsql;
using Webforms2.Models;

namespace WebForms2.Telas
{
    public partial class LancamentoNota : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                List<CursoModels> cursos = GetCursosFromDatabase();
                ddlCursos.DataSource = cursos;
                ddlCursos.DataTextField = "NomeCurso";
                ddlCursos.DataValueField = "IdCurso";
                ddlCursos.DataBind();
            }
        }

        protected void ddlCursos_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idCurso = int.Parse(ddlCursos.SelectedValue);
            List<ModuloModels> modulos = ModulosCursoPostgree(idCurso);
            ddlModulos.DataSource = modulos;
            ddlModulos.DataTextField = "NomeModulo";
            ddlModulos.DataValueField = "IdModulo";
            ddlModulos.DataBind();
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            int raAluno = int.Parse(txtRAAluno.Text);
            int idCurso = int.Parse(ddlCursos.SelectedValue);
            int idModulo = int.Parse(ddlModulos.SelectedValue);
            decimal nota = decimal.Parse(txtNota.Text);

            if (VerificarMatriculaExistente(raAluno, idCurso, idModulo))
            {
                if (SalvarNotaAluno(raAluno, idCurso, idModulo, nota))
                {
                    lblMensagem.Text = "Nota lançada com sucesso!";
                }
                else
                {
                    lblMensagem.Text = "Erro ao lançar a nota.";
                }
            }
            else
            {
                lblMensagem.Text = "A matrícula do aluno no curso e módulo selecionados não foi encontrada.";
            }
        }

        private bool VerificarMatriculaExistente(int raAluno, int idCurso, int idModulo)
        {
            string connectionString = "Host=localhost;Port=54321;Username=postgres;Password=postgres;Database=UpskillingGrupo4Final";

            using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                string sql = "SELECT COUNT(*) FROM \"Matriculas\" WHERE \"RAAluno\" = @RAAluno AND \"CursoID\" = @IdCurso AND \"ModuloID\" = @IdModulo";
                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@RAAluno", raAluno);
                    cmd.Parameters.AddWithValue("@IdCurso", idCurso);
                    cmd.Parameters.AddWithValue("@IdModulo", idModulo);

                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    return count > 0;
                }
            }
        }

        private bool SalvarNotaAluno(int raAluno, int idCurso, int idModulo, decimal nota)
        {
            string connectionString = "Host=localhost;Port=54321;Username=postgres;Password=postgres;Database=UpskillingGrupo4Final";

            string sql = "INSERT INTO \"tabela_notas\" (\"ra_aluno\", \"id_curso\", \"id_modulo\", \"nota\", \"data_lancamento\") VALUES (@raAluno, @idCurso, @idModulo, @nota, @dataLancamento)";

            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();

                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@raAluno", raAluno);
                    cmd.Parameters.AddWithValue("@idCurso", idCurso);
                    cmd.Parameters.AddWithValue("@idModulo", idModulo);
                    cmd.Parameters.AddWithValue("@nota", nota);
                    cmd.Parameters.AddWithValue("@dataLancamento", DateTime.Now);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    return rowsAffected > 0;
                }
            }
        }

        private List<CursoModels> GetCursosFromDatabase()
        {
            List<CursoModels> cursos = new List<CursoModels>();

            string connectionString = "Host=localhost;Port=54321;Username=postgres;Password=postgres;Database=UpskillingGrupo4Final";

            using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                string sql = "SELECT * FROM \"Cursos\"";
                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conn))
                {
                    using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int idCurso = Convert.ToInt32(reader["Id"]);
                            string nomeCurso = reader["Nome"].ToString();

                            CursoModels curso = new CursoModels
                            {
                                IdCurso = idCurso,
                                NomeCurso = nomeCurso
                            };

                            cursos.Add(curso);
                        }
                    }
                }
            }

            return cursos;
        }

        private List<ModuloModels> ModulosCursoPostgree(int idCurso)
        {
            List<ModuloModels> modulos = new List<ModuloModels>();

            string connectionString = "Host=localhost;Port=54321;Username=postgres;Password=postgres;Database=UpskillingGrupo4Final";

            using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                string sql = "SELECT * FROM \"ModulosCursos\" WHERE \"CursoID\" = @IdCurso";
                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@IdCurso", idCurso);
                    using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int idModulo = Convert.ToInt32(reader["ID"]);
                            string nomeModulo = reader["Nome"].ToString();

                            ModuloModels modulo = new ModuloModels
                            {
                                IdModulo = idModulo,
                                NomeModulo = nomeModulo
                            };

                            modulos.Add(modulo);
                        }
                    }
                }
            }
            return modulos;
        }

        private bool NotaExistente(int raAluno, int idCurso, int idModulo)
        {
            string connectionString = "Host=localhost;Port=54321;Username=postgres;Password=postgres;Database=UpskillingGrupo4Final";

            using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                string sql = "SELECT COUNT(*) FROM \"tabela_notas\" WHERE \"ra_aluno\" = @raAluno AND \"id_curso\" = @idCurso AND \"id_modulo\" = @idModulo";
                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@raAluno", raAluno);
                    cmd.Parameters.AddWithValue("@idCurso", idCurso);
                    cmd.Parameters.AddWithValue("@idModulo", idModulo);

                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    return count > 0;
                }
            }
        }

        private bool EditarNotaAluno(int raAluno, int idCurso, int idModulo, decimal nota)
        {
            string connectionString = "Host=localhost;Port=54321;Username=postgres;Password=postgres;Database=UpskillingGrupo4Final";

            string sql = "UPDATE \"tabela_notas\" SET \"nota\" = @nota WHERE \"ra_aluno\" = @raAluno AND \"id_curso\" = @idCurso AND \"id_modulo\" = @idModulo";

            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();

                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@raAluno", raAluno);
                    cmd.Parameters.AddWithValue("@idCurso", idCurso);
                    cmd.Parameters.AddWithValue("@idModulo", idModulo);
                    cmd.Parameters.AddWithValue("@nota", nota);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    return rowsAffected > 0;
                }
            }
        }

        private bool DeletarNotaAluno(int raAluno, int idCurso, int idModulo)
        {
            string connectionString = "Host=localhost;Port=54321;Username=postgres;Password=postgres;Database=UpskillingGrupo4Final";

            string sql = "DELETE FROM \"tabela_notas\" WHERE \"ra_aluno\" = @raAluno AND \"id_curso\" = @idCurso AND \"id_modulo\" = @idModulo";

            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();

                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@raAluno", raAluno);
                    cmd.Parameters.AddWithValue("@idCurso", idCurso);
                    cmd.Parameters.AddWithValue("@idModulo", idModulo);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    return rowsAffected > 0;
                }
            }
        }

        protected void btnEditarNota_Click(object sender, EventArgs e)
        {
            int raAluno = int.Parse(txtRAAluno.Text);
            int idCurso = int.Parse(ddlCursos.SelectedValue);
            int idModulo = int.Parse(ddlModulos.SelectedValue);
            decimal nota = decimal.Parse(txtNota.Text);

            if (VerificarMatriculaExistente(raAluno, idCurso, idModulo))
            {
                if (EditarNotaAluno(raAluno, idCurso, idModulo, nota))
                {
                    lblMensagem.Text = "Nota editada com sucesso!";
                }
                else
                {
                    lblMensagem.Text = "Erro ao editar a nota.";
                }
            }
            else
            {
                lblMensagem.Text = "A matrícula do aluno no curso e módulo selecionados não foi encontrada.";
            }
        }

        protected void btnDeletarNota_Click(object sender, EventArgs e)
        {
            int raAluno = int.Parse(txtRAAluno.Text);
            int idCurso = int.Parse(ddlCursos.SelectedValue);
            int idModulo = int.Parse(ddlModulos.SelectedValue);

            if (VerificarMatriculaExistente(raAluno, idCurso, idModulo))
            {
                if (DeletarNotaAluno(raAluno, idCurso, idModulo))
                {
                    lblMensagem.Text = "Nota deletada com sucesso!";
                }
                else
                {
                    lblMensagem.Text = "Erro ao deletar a nota.";
                }
            }
            else
            {
                lblMensagem.Text = "A matrícula do aluno no curso e módulo selecionados não foi encontrada.";
            }
        }

        protected void btnBuscarNotas_Click(object sender, EventArgs e)
        {
            int raAluno = int.Parse(txtBuscarRAAluno.Text);
            List<NotaModels> notas = BuscarNotasAluno(raAluno);

            if (notas.Count > 0)
            {
                gridNotasLancadas.DataSource = notas;
                gridNotasLancadas.DataBind();
                lblMensagemBusca.Text = string.Empty;
            }
            else
            {
                lblMensagemBusca.Text = "Nenhuma nota encontrada para o RA do aluno informado.";
                gridNotasLancadas.DataSource = null;
                gridNotasLancadas.DataBind();
            }
        }

        protected void gridNotasLancadas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = gridNotasLancadas.Rows[index];
            int raAluno = int.Parse(row.Cells[0].Text);
            int idCurso = int.Parse(row.Cells[1].Text);
            int idModulo = int.Parse(row.Cells[2].Text);

            if (e.CommandName == "EditarNota")
            {
                // Redirecionar para a tela de edição com os valores já preenchidos
                Response.Redirect($"EdicaoNota.aspx?raAluno={raAluno}&idCurso={idCurso}&idModulo={idModulo}");
            }
            else if (e.CommandName == "DeletarNota")
            {
                if (DeletarNotaAluno(raAluno, idCurso, idModulo))
                {
                    lblMensagemBusca.Text = "Nota deletada com sucesso!";
                    btnBuscarNotas_Click(sender, e); // Atualizar a grade após a exclusão
                }
                else
                {
                    lblMensagemBusca.Text = "Erro ao deletar a nota.";
                }
            }
        }

        protected void btnRetornarPaginaInicial_Click(object sender, EventArgs e)
        {

        }

        private List<NotaModels> BuscarNotasAluno(int raAluno)
        {
            List<NotaModels> notas = new List<NotaModels>();

            string connectionString = "Host=localhost;Port=54321;Username=postgres;Password=postgres;Database=UpskillingGrupo4Final";

            using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                string sql = "SELECT * FROM \"tabela_notas\" WHERE \"ra_aluno\" = @raAluno";
                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@raAluno", raAluno);
                    using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int idCurso = Convert.ToInt32(reader["id_curso"]);
                            int idModulo = Convert.ToInt32(reader["id_modulo"]);
                            decimal nota = Convert.ToDecimal(reader["nota"]);

                            /*NotaModels notaAluno = new NotaModels
                            {
                                RA = raAluno,
                                IdCurso = idCurso,
                                IdModulo = idModulo,
                                Nota = nota
                            };*/

                            //notas.Add(notaAluno);
                        }
                    }
                }
            }

            return notas;
        }

    }
}
