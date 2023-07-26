using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Webforms2.DTO_s;

namespace Webforms2.Models
{
    public class NotaModulo : AlunoDTO
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