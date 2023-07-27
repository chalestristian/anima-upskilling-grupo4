using System;
using System.Collections.Generic;
using System.Data;
using Npgsql;
using System.Web.UI;
using Webforms2.Models;
using System.Configuration;
using System.Security.Cryptography;
using System.Text;

namespace WebForms2.Telas
{
    public partial class Login : Page
    {
        protected void btnEntrar_Click(object sender, EventArgs e)
        {
            string usuarioDigitado = txtUsuario.Text;
            string senhaDigitada = txtSenha.Text;

            string connectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;

            using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    string sql = "SELECT \"Senha\" FROM \"Usuarios\" WHERE \"Login\" = @Usuario";
                    using (NpgsqlCommand cmd = new NpgsqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@Usuario", usuarioDigitado);

                        var senhaCriptografadaDB = (string)cmd.ExecuteScalar();

                        if (!string.IsNullOrEmpty(senhaCriptografadaDB))
                        {
                            var sha256 = new SHA256Managed();
                            var senhaBytes = Encoding.UTF8.GetBytes(senhaDigitada);
                            var senhaCriptografadaInput = Convert.ToBase64String(sha256.ComputeHash(senhaBytes));

                            if (senhaCriptografadaInput == senhaCriptografadaDB)
                            {
                                Session["UsuarioAutenticado"] = true;
                                Response.Redirect("PaginaInicial.aspx");
                            }
                            else
                            {
                                lblMensagem.Text = "Usuário ou senha inválidos.";
                            }
                        }
                        else
                        {
                            lblMensagem.Text = "Usuário não encontrado.";
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
            /*if (Session["UsuarioAutenticado"] != null && (bool)Session["UsuarioAutenticado"])
            {
                Response.Redirect("PaginaInicial.aspx");
            }*/
        }
    }
}
