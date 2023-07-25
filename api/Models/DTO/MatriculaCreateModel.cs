using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace api.Models.DTO
{
    [NotMapped]
    public class MatriculaCreateModel
    {
        [Required]
        public int AlunoId { get; set; }
        [Required]
        public int CursoId { get; set; }
        [Required]
        public decimal? ValorMatricula { get; set; }
    }

}