using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models.DTO
{
    [NotMapped]
    public class LoginModel
    {
        [Required]
        public string Login { get; set; }

        [Required]
        public string Senha { get; set; }
    }

}