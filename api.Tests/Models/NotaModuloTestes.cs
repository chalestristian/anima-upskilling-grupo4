using api.Models;
using Xunit;

namespace api.Tests.Models
{
    public class NotaModuloTestes
    {
        [Fact(DisplayName = nameof(CreateNotaModulo_ValidParameters_Success))]
        public void CreateNotaModulo_ValidParameters_Success()
        {
            // Arrange
            var notaModulo = new NotaModulo
            {
                MatriculaId = 1,
                Modulo = new ModuloCurso(),
                Nota = 8.5m,
                DataLancamento = new DateTime(2022, 1, 1)
            };

            // Act - No action required

            // Assert
            Assert.Equal(1, notaModulo.MatriculaId);
            Assert.NotNull(notaModulo.Modulo);
            Assert.Equal(8.5m, notaModulo.Nota);
            Assert.Equal(new DateTime(2022, 1, 1), notaModulo.DataLancamento);
        }

        [Fact(DisplayName = nameof(CreateNotaModulo_InvalidParameters_Failure))]
        public void CreateNotaModulo_InvalidParameters_Failure()
        {
            // Arrange
            var notaModulo = new NotaModulo();

            // Act - No action required

            // Assert
            Assert.Equal(default(int), notaModulo.MatriculaId);
            Assert.Null(notaModulo.Modulo);
            Assert.Equal(default(decimal), notaModulo.Nota);
            Assert.Equal(default(DateTime), notaModulo.DataLancamento);
        }
    }
}
