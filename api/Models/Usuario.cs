using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace api.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public Pessoa Pessoa { get; set; }
        public string Login { get; set; }

        [JsonIgnore]
        public string Senha { get; set; }

        public DateTime? DataUltimoAcesso { get; set; }
        public DateTime DataCadastro { get; set; }
    }
}
