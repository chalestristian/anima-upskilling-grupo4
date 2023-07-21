using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Webforms2.Models
{
    public class Usuario
    {
        public string Nome { get; set; }
        public string Senha { get; set; }

        public Usuario(string nome, string senha)
        {
            Nome = nome;
            Senha = senha;
        }
    }
}