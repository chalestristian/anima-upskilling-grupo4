using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using secretaria_academica.DTO;

namespace secretaria_academica.Models
{
    public class NotaModels : AlunoDTO
    {
        public int IdNota { get; set; }
        //public AlunosModels RA { get; set; }
        public ModuloModels IdModulo { get; set; }
        public CursoModels IdCurso { get; set; }
        public CursoModels NomeCurso { get; set; }
        public decimal Nota { get; set; }
        public DateTime DataLancamento { get; set; }

    }
}