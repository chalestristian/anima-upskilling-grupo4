using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Webforms2.Models;

namespace Webforms2.DTO_s
{
    public class AlunoDTO : PessoaModels
    {
        public int RA { get; set; }
        public string Nome { get; set; }
        public DateTime DataCadastro { get; set; }
        
    }
}