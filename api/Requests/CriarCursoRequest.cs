namespace api.Requests
{
    public class CriarCursoRequest
    {
        public CursoRequest Curso { get; set; }
        public IEnumerable<AulaRequest> Aulas { get; set; }
    }
}
