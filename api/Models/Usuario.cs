using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace api.Models
{
    public class Usuario
    {

        public Usuario()
        {
            PerfilAdministrativo = false;
            PerfilAluno = true;
        }

        public int Id { get; set; }
        public Pessoa? Pessoa { get; set; }
        public string? Login { get; set; }

        //[JsonIgnore]
        public string? Senha { get; set; }

        public Boolean? PerfilAdministrativo { get; set; }
        public Boolean? PerfilAluno { get; set; }
        public Aluno? Aluno { get; set;  }

        public DateTime? DataUltimoAcesso { get; set; }
        public DateTime? DataCadastro { get; set; }
    }
}
