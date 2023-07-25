using System;
using System.Collections.Generic;
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
                // Carregar curso do Banco
                List<CursoModels> cursos = GetCursos();
                ddlCursos.DataSource = cursos;
                ddlCursos.DataTextField = "NomeCurso";
                ddlCursos.DataValueField = "IdCurso";
                ddlCursos.DataBind();
            }
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            int raAluno = int.Parse(txtRAAluno.Text);
            int idCurso = int.Parse(ddlCursos.SelectedValue);
            decimal nota = decimal.Parse(txtNota.Text);

            // lógica para salvar a nota do aluno
                       
            string connectionString = "Host=localhost:54321;Username=postgres;Password=postgres;Database=UpskillingGrupo4Final";

            // insert de nota na tabela do banco de dados
            string sql = "INSERT INTO tabela_notas (ra_aluno, id_curso, nota, data_lancamento) VALUES (@raAluno, @idCurso, @nota, @dataLancamento)";

            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();

                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@raAluno", raAluno);
                    cmd.Parameters.AddWithValue("@idCurso", idCurso);
                    cmd.Parameters.AddWithValue("@nota", nota);
                    cmd.Parameters.AddWithValue("@dataLancamento", DateTime.Now);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        lblMensagem.Text = "Nota lançada com sucesso!";
                    }
                    else
                    {                       
                        lblMensagem.Text = "Erro ao lançar a nota.";
                    }
                }
            }
        }

        // Buscar no banco de dados
        private List<CursoModels> GetCursos()
        {
            List<CursoModels> cursos = new List<CursoModels>
            {
                new CursoModels { IdCurso = 1, NomeCurso = "Curso 1" },
                new CursoModels { IdCurso = 2, NomeCurso = "Curso 2" },
                new CursoModels { IdCurso = 3, NomeCurso = "Curso 3" }
                // Adc os cursos do banco de dados aqui
            };
            return cursos;
        }
    }
}
