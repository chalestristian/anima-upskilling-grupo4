﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Webforms2.DTO_s;

namespace Webforms2.Models
{
    public class CursoModels : CursoDTO
    {
        
        public int? CargaHoraria { get; set; }
        public decimal? ValorCurso { get; set; }
    }
}

