using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace secretaria_academica.Models
{
    public class MatriculaModels
    {
        public MatriculaModels()
        {
            MatriculaConfirmada = false;
        }

        public int Id { get; set; }
        public AlunoModels Aluno { get; set; }
        public CursoModels Curso { get; set; }
        public DateTime DataMatricula { get; set; }
        public decimal ValorMatricula { get; set; }
        public DateTime DataConclusao { get; set; }
        public decimal Media { get; set; }
        public string Status { get; set; }
        public Boolean MatriculaConfirmada { get; set; }

    }
}