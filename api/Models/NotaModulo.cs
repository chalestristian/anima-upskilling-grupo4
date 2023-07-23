namespace api.Models
{
    public class NotaModulo
    {
        public int Id { get; set; }
        public int? MatriculaId { get; set; }
        public ModuloCurso? Modulo { get; set; }
        public decimal? Nota { get; set; }
        public DateTime? DataLancamento { get; set; }
    }
}
