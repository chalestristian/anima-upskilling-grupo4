using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using secretaria_academica.DTO;

namespace secretaria_academica.Models
{
    public class AlunosModels : AlunoDTO
    {       
        public PessoaModels Id { get; set; }
        public CursoModels NomeCurso { get; set; }
        
    }
}