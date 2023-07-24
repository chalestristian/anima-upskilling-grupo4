using System.ComponentModel.DataAnnotations;

namespace api.Models
{
    public class LoginRequest
    {
        [Required]
        public string Login { get; set; }

        [Required]
        public string Senha { get; set; }
    }

}