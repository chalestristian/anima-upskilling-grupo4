using System;
using System.Collections.Generic;
using System.Web.UI;
using Webforms2.Models;

namespace WebForms2.Telas
{
    public partial class Login : Page
    {
        private List<Usuario> usuarios = new List<Usuario>
        {
            new Usuario("usuario1", "senha1"),
            new Usuario("usuario2", "senha2")
            
        };

        protected void btnEntrar_Click(object sender, EventArgs e)
        {
            string usuarioDigitado = txtUsuario.Text;
            string senhaDigitada = txtSenha.Text;

            // Verificar se o usuário está na lista de usuários
            Usuario usuarioAutenticado = usuarios.Find(u => u.Nome == usuarioDigitado && u.Senha == senhaDigitada);

            if (usuarioAutenticado != null)
            {
                //mudar p próxx pag
                Response.Redirect("PaginaProtegida.aspx");
            }
            else
            {                
                lblMensagem.Text = "Usuário ou senha inválidos.";
            }
        }
    }
}
