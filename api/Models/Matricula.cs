namespace api.Models
{
    public class Matricula
    {
        public int Id { get; set; }
        public Aluno Aluno { get; set; }
        public Curso Curso { get; set; }
        public DateTime DataMatricula { get; set; }
        public decimal? ValorMatricula { get; set; }
        public DateTime? DataConclusao { get; set; }
        public decimal? Media { get; set; }
        public string? Status { get; set; }
    }
}
