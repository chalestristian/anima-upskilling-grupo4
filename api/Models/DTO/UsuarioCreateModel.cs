using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace api.Models.DTO
{
    [NotMapped]
    public class UsuarioCreateModel
    {
        [Required]
        public int PessoaId { get; set; }

        [Required]
        public string Login { get; set; }

        [Required]
        public string Senha { get; set; }

        [Required]
        public int AlunoId { get; set; }

        public bool PerfilAluno { get; set; } = true;
    }

}