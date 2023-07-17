namespace api.Configurations.DTO
{
    public class DadosAulaDTO
    {
        public int CodigoAula { get; set; }
        public string TituloAula { get; set; }
        public string LinkAula { get; set; }
        public string DescricaoAula { get; set; }
        public int Ordem { get; set; }
        public bool Ativo { get; set; }
        public DateTime DataCadastro { get; set; }
    }
}
