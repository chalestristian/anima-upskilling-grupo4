using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace secretaria_academica.Models
{
    public class AlunoModels
    {
        public int Id { get; set; }
        public PessoaModels Pessoa { get; set; }
        public string Matricula { get; set; }
        public DateTime? DataCadastro { get; set; }

    }
}