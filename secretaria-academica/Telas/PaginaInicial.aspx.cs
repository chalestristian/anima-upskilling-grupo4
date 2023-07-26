using System;

namespace WebForms2.Telas
{
    public partial class PaginaInicial : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Verifique se o usuário está autenticado
            if (Session["UsuarioAutenticado"] == null || !(bool)Session["UsuarioAutenticado"])
            {
                Response.Redirect("Login.aspx");
            }
        }

        protected void btnCadastroCurso_Click(object sender, EventArgs e)
        {
            Response.Redirect("CadastroCurso.aspx");
        }

        protected void btnLancamentoNotas_Click(object sender, EventArgs e)
        {
            Response.Redirect("LancamentoNota.aspx");
        }

        protected void btnRelatorioCursos_Click(object sender, EventArgs e)
        {
            Response.Redirect("RelatorioPDF.aspx");
        }
    }
}
