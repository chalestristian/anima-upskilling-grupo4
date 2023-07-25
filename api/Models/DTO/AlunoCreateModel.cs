using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace api.Models.DTO
{
    [NotMapped]
    public class AlunoCreateModel
    {
        [Required]
        public int PessoaId { get; set; }

        [Required]
        public string Matricula { get; set; }
    }

}