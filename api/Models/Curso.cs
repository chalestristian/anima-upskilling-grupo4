using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace api.Models
{
    public class Curso
    {
        public int Id { get; set; }
        public string? Nome { get; set; }
        public int? CH { get; set; }
        public decimal? Valor { get; set; }

        [NotMapped]
        public List<ModuloCurso> ModulosCurso { get; set; }
    }
}
