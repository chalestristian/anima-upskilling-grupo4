using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace secretaria_academica.Models
{
    public class ModuloModels 
    {
        public int IdModulo { get; set; }
        public string NomeModulo { get; set; }
        public int CHModulo { get; set; }

        public CursoModels Curso { get; set; }
    }
}