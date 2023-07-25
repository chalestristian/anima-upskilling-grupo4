using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace api.Models
{
    public class ModuloCurso
    {
        public int Id { get; set; }
        public Curso? Curso { get; set; }
        public string? Nome { get; set; }
        public int? CH { get; set; }

    }
}
