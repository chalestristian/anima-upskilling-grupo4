using System;

namespace WebForms
{
    public partial class Login : System.Web.UI.Page
    {
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string usuario = txtUsuario.Text;
            string senha = txtSenha.Text;

            // Conexão com o banco de dados PostgreSQL
            string connectionString = "Host=localhost;Port=5432;Database=nomedobanco;Username=usuario;Password=senha;";
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT COUNT(*) FROM Usuarios WHERE Usuario = @Usuario AND Senha = @Senha";
                    NpgsqlCommand command = new NpgsqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Usuario", usuario);
                    command.Parameters.AddWithValue("@Senha", senha);
                    int count = (int)command.ExecuteScalar();

                    if (count == 1)
                    {
                        // Autenticação bem-sucedida, redirecione para a página principal ou outra página desejada
                        Response.Redirect("PaginaPrincipal.aspx");
                    }
                    else
                    {
                        // Autenticação falhou, exiba mensagem de erro ou tome outra ação
                        lblMensagemErro.Text = "Usuário ou senha inválidos!";
                    }
                }
                catch (Exception ex)
                {
                    // Lida com exceções do PostgreSQL, por exemplo, exibir mensagem de erro ou registrar
                    lblMensagemErro.Text = "Erro de banco de dados: " + ex.Message;
                }
            }
        }
    }
}