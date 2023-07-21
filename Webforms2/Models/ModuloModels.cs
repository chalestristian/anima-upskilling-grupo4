using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Webforms2.Models
{
    public class ModuloModels
    {
        public int IdModulo { get; set; }
        public CursoModels Curso { get; set; }
        public string Nome { get; set; }
        public int CH { get; set; }
    }
}