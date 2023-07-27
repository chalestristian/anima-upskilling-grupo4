using Npgsql;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Web;

namespace secretaria_academica.DTO
{
    public class NotasDoAlunoViewModel
    {
        public int ModuloId { get; set; }
        public string ModuloNome { get; set; }
        public decimal Nota { get; set; }
    }
}