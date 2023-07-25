using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace api.Models.DTO
{
    [NotMapped]
    public class MatriculaGetModel
    {
        [Required]
        public int AlunoId { get; set; }
        [Required]
        public int CursoId { get; set; }
    }

}