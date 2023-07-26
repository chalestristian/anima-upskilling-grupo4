using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Webforms2.DTO_s;

namespace Webforms2.Models
{
    public class AlunosModels : AlunoDTO
    {       
        public PessoaModels Id { get; set; }
        public CursoModels NomeCurso { get; set; }
        
    }
}