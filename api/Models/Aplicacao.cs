using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace api.Models
{
    public class Aplicacao
    {
        [JsonIgnore]
        public int Id { get; set; }

        [JsonIgnore]
        public string? Nome { get; set; }

        [Required]
        public string? AppKey { get; set; }

        [Required]
        public string? SecretKey { get; set; }
    }
}
