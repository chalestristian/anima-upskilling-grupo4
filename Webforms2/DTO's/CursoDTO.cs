using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Webforms2.DTO_s
{
    public class CursoDTO
    {
        public int IdCurso { get; set; }
        public string NomeCurso { get; set; }
        public int? CargaHoraria { get; set; }
        public decimal? ValorCurso { get; set; }
    }
}