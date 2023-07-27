using secretaria_academica.DTO;
using secretaria_academica.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;

namespace secretaria_academica.Views
{
    public partial class LancarNotas : System.Web.UI.Page
    {
        private List<CursoModels> cursos;
        private List<AlunoModels> alunos;

        protected void Page_Load(object sender, EventArgs e)
        {
            // Verifique se o usuário está autenticado
            if (Session["UsuarioAutenticado"] == null || !(bool)Session["UsuarioAutenticado"])
            {
                Response.Redirect("Login.aspx");
            }

            if (!IsPostBack)
            {
                CarregarCursos();
            }
        }

        protected void ddlCurso_SelectedIndexChanged(object sender, EventArgs e)
        {
            int cursoId = Convert.ToInt32(ddlCurso.SelectedValue);
            CarregarAlunosDoCurso(cursoId);
        }

        protected void ddlAluno_SelectedIndexChanged(object sender, EventArgs e)
        {
            int cursoId = Convert.ToInt32(ddlCurso.SelectedValue);
            int alunoId = Convert.ToInt32(ddlAluno.SelectedValue);
            CarregarNotasDoAluno(cursoId, alunoId);
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            int cursoId = Convert.ToInt32(ddlCurso.SelectedValue);
            int alunoId = Convert.ToInt32(ddlAluno.SelectedValue);

            foreach (GridViewRow row in gridNotas.Rows)
            {
                TextBox txtNota = (TextBox)row.FindControl("txtNota");
                int moduloId = Convert.ToInt32(gridNotas.DataKeys[row.RowIndex].Value);
                decimal nota = Convert.ToDecimal(txtNota.Text);

                SalvarNotaDoAluno(cursoId, alunoId, moduloId, nota);
            }

            CalcularMediaEStatus(alunoId, cursoId);

            lblMensagemSucesso.Text = "Nota salva com sucesso!";
        }
        
        private void CarregarCursos()
        {
            cursos = CursoModels.GetAllCursos();

            ddlCurso.DataSource = cursos;
            ddlCurso.DataTextField = "NomeCurso";
            ddlCurso.DataValueField = "IdCurso";
            ddlCurso.DataBind();

            ddlCurso.Items.Insert(0, new ListItem("Selecione um curso", ""));
        }

        private void CarregarAlunosDoCurso(int cursoId)
        {
            alunos = AlunoModels.GetAlunosMatriculadosPorCurso(cursoId);

            ddlAluno.Items.Clear();

            ddlAluno.Items.Add(new ListItem("Selecione um aluno", ""));

            foreach (var aluno in alunos)
            {
                ddlAluno.Items.Add(new ListItem(aluno.Pessoa.NomePessoa, aluno.Id.ToString()));
            }
        }

        private void CarregarNotasDoAluno(int cursoId, int alunoId)
        {
            List<ModuloModels> modulos = ModuloModels.GetModulosByCurso(cursoId);

            List<NotasDoAlunoViewModel> notas = new List<NotasDoAlunoViewModel>();
            foreach (var modulo in modulos)
            {
                decimal nota = NotaModels.GetNotaDoAlunoPorModulo(cursoId, alunoId, modulo.IdModulo);
                notas.Add(new NotasDoAlunoViewModel { ModuloId = modulo.IdModulo, ModuloNome = modulo.NomeModulo, Nota = nota });
            }

            gridNotas.DataSource = notas;
            gridNotas.DataBind();
        }


        private decimal ObterNotaDoAluno(int cursoId, int alunoId, string moduloNome)
        {
            // Implemente a lógica para buscar a nota do aluno para o módulo com o nome 'moduloNome'
            // no curso com o ID cursoId e ID do aluno alunoId no banco de dados
            // Retorne a nota encontrada ou um valor padrão caso a nota não exista.
            // Exemplo fictício para demonstração:
            if (alunoId == 1 && moduloNome == "Módulo X")
            {
                return 8.5m;
            }
            else if (alunoId == 1 && moduloNome == "Módulo Y")
            {
                return 7.8m;
            }
            else if (alunoId == 2 && moduloNome == "Módulo X")
            {
                return 9.2m;
            }
            else if (alunoId == 2 && moduloNome == "Módulo Y")
            {
                return 6.5m;
            }
            else
            {
                // Se a nota não existir, retorne um valor padrão, por exemplo, 0.
                return 0m;
            }
        }

        protected void btnVoltarParaTelaAnterior_Click(object sender, EventArgs e)
        {
            Response.Redirect("PaginaInicial.aspx");
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("LancarNotas.aspx");
        }

        private void CalcularMediaEStatus(int alunoId, int cursoId)
        {
            // Calcular média
            decimal media = CalcularMedia(alunoId, cursoId);

            // Obter status com base na média calculada
            string status = ObterStatus(media);

            // Salvar a média e o status no banco de dados
            AlunoModels.AtualizarMediaEStatus(alunoId, cursoId, media, status);

            // Se aprovado, preencher a Data de Conclusão do Curso
            if (status == "APROVADO")
            {
                AlunoModels.PreencherDataConclusao(alunoId, cursoId);
            }
        }

        private decimal CalcularMedia(int matriculaId, int cursoId)
        {
            List<NotaModels> notas = NotaModels.GetNotasDoAlunoPorCurso(matriculaId, cursoId);
            decimal somaNotas = notas.Sum(nota => nota.Nota);
            decimal media = somaNotas / notas.Count;
            return media;
        }

        private string ObterStatus(decimal media)
        {
            if (media < 3)
            {
                return "REPROVADO";
            }
            else if (media >= 3 && media < 7)
            {
                return "RECUPERAÇÃO";
            }
            else
            {
                return "APROVADO";
            }
        }

        private void SalvarNotaDoAluno(int cursoId, int matriculaId, int moduloId, decimal nota)
        {
            NotaModels.SalvarNotaAluno(cursoId, matriculaId, moduloId, nota);
        }
    }
}
