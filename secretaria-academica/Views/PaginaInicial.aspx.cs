using System;

namespace secretaria_academica.Views
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

        protected void btnSair_Click(object sender, EventArgs e)
        {
            // Apagar a sessão
            Session.Clear(); 
            Session.Abandon();
            Response.Cookies.Clear();

            Response.Redirect("Login.aspx");
        }
    }
}
