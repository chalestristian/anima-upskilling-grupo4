using System;
using System.Collections.Generic;
using System.Data;
using Npgsql;
using System.Web.UI;
using Webforms2.Models;

namespace WebForms2.Telas
{
    public partial class Login : Page
    {
        protected void btnEntrar_Click(object sender, EventArgs e)
        {
            string usuarioDigitado = txtUsuario.Text;
            string senhaDigitada = txtSenha.Text;

            string connectionString = "Host=localhost;Port=54321;Username=postgres;Password=postgres;Database=UpskillingGrupo4Final";

            using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    string sql = "SELECT COUNT(*) FROM \"Usuarios\" WHERE \"Login\" = @Nome AND \"Senha\" = @Senha";
                    using (NpgsqlCommand cmd = new NpgsqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@Nome", usuarioDigitado);
                        cmd.Parameters.AddWithValue("@Senha", senhaDigitada);

                        int result = (int)cmd.ExecuteScalar();
                        if (result > 0)
                        {
                            // Autenticação bem-sucedida
                            Session["UsuarioAutenticado"] = true;
                            Response.Redirect("PaginaInicial.aspx");
                        }
                        else
                        {
                            lblMensagem.Text = "Usuário ou senha inválidos.";
                        }
                    }
                }
                catch (Exception ex)
                {
                    lblMensagem.Text = "Ocorreu um erro na autenticação, verifique os dados informados.";
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //Redireciona para Página Inicial 
            if (Session["UsuarioAutenticado"] != null && (bool)Session["UsuarioAutenticado"])
            {
                Response.Redirect("PaginaInicial.aspx");
            }
        }
    }
}
