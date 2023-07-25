using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Webforms2.Models
{
    public class AlunosModels
    {
        public int RA { get; set; }
        public string Nome { get; set; }
        public PessoaModels Id { get; set; }
        public CursoModels NomeCurso { get; set; }
        public DateTime DataCadastro { get; set; }
    }
}