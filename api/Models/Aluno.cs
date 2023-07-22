namespace api.Models
{
    public class Aluno
    {
        public int Id { get; set; }
        public Pessoa? Pessoa { get; set; }
        public string? Matricula { get; set; }
        public DateTime? DataCadastro { get; set; }
    }
}
