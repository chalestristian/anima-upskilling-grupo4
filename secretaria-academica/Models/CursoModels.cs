using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace secretaria_academica.Models
{
    public class CursoModels
    {
        public int IdCurso { get; set; }
        public string NomeCurso { get; set; }
        public int? CargaHoraria { get; set; }
        public decimal? ValorCurso { get; set; }
    }
}

