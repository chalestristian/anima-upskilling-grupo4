using System;
using System.Collections.Generic;
using System.Web.UI;
using Webforms2.Models;

namespace WebForms2.Telas
{
    public partial class CadastroCurso : Page
    {
        // Lista para simular o armazenamento dos cursos, tenho que buscar no banco os dados
        private List<CursoModels> cursos = new List<CursoModels>();

        protected void Page_Load(object sender, EventArgs e)
        {            
            if (!IsPostBack)
            {
                //carregamento de cursos previamente cadastrados 
                cursos.Add(new CursoModels { IdCurso = 1, NomeCurso = "Curso A", CargaHoraria = 40, ValorCurso = 100.00M });
                cursos.Add(new CursoModels { IdCurso = 2, NomeCurso = "Curso B", CargaHoraria = 30, ValorCurso = 150.00M });
                cursos.Add(new CursoModels { IdCurso = 3, NomeCurso = "Curso C", CargaHoraria = 50, ValorCurso = 200.00M });

            }
            else
            {
                // recarregar página
            }
        }

        protected void btnCadastrar_Click(object sender, EventArgs e)
        {
            string nomeCurso = txtNomeCurso.Text;
            int? cargaHoraria = null;
            decimal? valor = null;

            // campos de CH e Valor foram preenchidos?
            if (!string.IsNullOrEmpty(txtCargaHoraria.Text))
                cargaHoraria = Convert.ToInt32(txtCargaHoraria.Text);

            if (!string.IsNullOrEmpty(txtValor.Text))
                valor = Convert.ToDecimal(txtValor.Text);

            // Criação do objeto CursoModels
            CursoModels curso = new CursoModels
            {
                NomeCurso = nomeCurso,
                CargaHoraria = cargaHoraria,
                ValorCurso = valor
            };

            // Adiciona o curso à lista de cursos > substituit pelo banco
            cursos.Add(curso);
            // Implementar de acordo com o cadastro PGSG
        }
    }
}
