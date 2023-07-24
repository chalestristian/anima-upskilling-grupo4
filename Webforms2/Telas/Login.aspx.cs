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
        // Lista para simular o armazenamento dos Usuários
        private List<Usuario> usuarios = new List<Usuario>
        {
            new Usuario("usuario1", "senha1"),
            new Usuario("usuario2", "senha2")
        };

        protected void btnEntrar_Click(object sender, EventArgs e)
        {
            string usuarioDigitado = txtUsuario.Text;
            string senhaDigitada = txtSenha.Text;

            
            Usuario usuarioAutenticado = usuarios.Find(u => u.Nome == usuarioDigitado && u.Senha == senhaDigitada);

            if (usuarioAutenticado != null)
            {
                
                Response.Redirect("PaginaProtegida.aspx");
            }
            else
            {
                
                string connectionString = "";

                using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
                {
                    try
                    {
                        con.Open();
                        string sql = "SELECT COUNT(*) FROM tabela_usuarios WHERE nome = @Nome AND senha = @Senha";
                        using (NpgsqlCommand cmd = new NpgsqlCommand(sql, con))
                        {
                            cmd.Parameters.AddWithValue("@Nome", usuarioDigitado);
                            cmd.Parameters.AddWithValue("@Senha", senhaDigitada);

                            int result = (int)cmd.ExecuteScalar();
                            if (result > 0)
                            {
                                // Se autenticação ok, redirecionar para a próxima página
                                Response.Redirect("PaginaProtegida.aspx");
                            }
                            else
                            {
                                lblMensagem.InnerText = "Usuário ou senha inválidos.";
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        
                        lblMensagem.InnerText = "Ocorreu um erro na autenticação.";
                    }
                }
            }
        }
    }
}
