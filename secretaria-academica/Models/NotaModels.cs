using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace secretaria_academica.Models
{
    public class NotaModels
    {
        public int IdNota { get; set; }
        public AlunoModels Aluno { get; set; }
        public ModuloModels Modulo { get; set; }
        public CursoModels Curso { get; set; }
        public decimal Nota { get; set; }
        public DateTime DataLancamento { get; set; }

    }
}