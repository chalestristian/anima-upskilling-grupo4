using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Webforms2.Models;

namespace Webforms2.DTO_s
{
    public class CursoDTO : ModuloModels
    {
        public int IdCurso { get; set; }
        public string NomeCurso { get; set; }
        /*public int? CargaHoraria { get; set; }
        public decimal? ValorCurso { get; set; }*/
    }
}