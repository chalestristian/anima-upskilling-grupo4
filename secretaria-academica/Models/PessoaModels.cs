using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace secretaria_academica.Models
{
    public class PessoaModels
    {
        public int IdPessoa { get; set; }
        public string NomePessoa { get; set; }
        public string CPF { get; set; }
        public string Celular { get; set; }
        public string Email { get; set; }
        public DateTime DataNascimento { get; set; }
    }
}