using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Webforms2.DTO_s;

namespace Webforms2.Models
{
    public class CursoModels
    {
        public int IdCurso { get; set; }
        public string NomeCurso { get; set; }
        public int? CargaHoraria { get; set; }
        public decimal? ValorCurso { get; set; }
    }
}

