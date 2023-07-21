﻿using System;
using WebForms.Models;

namespace WebForms
{
    public partial class Login : System.Web.UI.Page
    {
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            UsuarioModels usuario = new UsuarioModels();
            usuario.NomeUsuario = txtUsuario.Text;
            usuario.Senha = txtSenha.Text;

            // Conexão com o banco de dados PostgreSQL e consulta para verificar as credenciais
            // Substitua as informações de conexão com o banco de dados abaixo pelos dados do seu PostgreSQL
            string connectionString = "Host=seu_host;Port=sua_porta;Database=seu_banco_de_dados;Username=seu_usuario;Password=sua_senha;";

            using (var connection = new Npgsql.NpgsqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // Consulta SQL para verificar as credenciais do usuário
                    string query = "SELECT COUNT(*) FROM Usuarios WHERE Usuario = @Usuario AND Senha = @Senha";
                    var command = new Npgsql.NpgsqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Usuario", usuario.NomeUsuario);
                    command.Parameters.AddWithValue("@Senha", usuario.Senha);
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
