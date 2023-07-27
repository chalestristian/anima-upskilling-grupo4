using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using secretaria_academica.Models;

namespace secretaria_academica.DTO
{
    public class AlunoDTO : PessoaModels
    {
        public int RA { get; set; }
        public string Nome { get; set; }
        public DateTime DataCadastro { get; set; }
        
    }
}