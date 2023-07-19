using api.Models;
using Xunit;

namespace api.Tests.Models
{
    public class MatriculaTestes
    {
        [Fact(DisplayName = nameof(CreateMatricula_ValidParameters_Success))]
        public void CreateMatricula_ValidParameters_Success()
        {
            // Arrange
            var matricula = new Matricula
            {
                Aluno = new Aluno(),
                Curso = new Curso(),
                DataMatricula = new DateTime(2022, 1, 1),
                ValorMatricula = 150.00m,
                DataConclusao = new DateTime(2022, 6, 30),
                Media = 8.5m,
                Status = "Aprovado"
            };

            // Act - No action required

            // Assert
            Assert.NotNull(matricula.Aluno);
            Assert.NotNull(matricula.Curso);
            Assert.Equal(new DateTime(2022, 1, 1), matricula.DataMatricula);
            Assert.Equal(150.00m, matricula.ValorMatricula);
            Assert.Equal(new DateTime(2022, 6, 30), matricula.DataConclusao);
            Assert.Equal(8.5m, matricula.Media);
            Assert.Equal("Aprovado", matricula.Status);
        }

        [Fact(DisplayName = nameof(CreateMatricula_InvalidParameters_Failure))]
        public void CreateMatricula_InvalidParameters_Failure()
        {
            // Arrange
            var matricula = new Matricula();

            // Act - No action required

            // Assert
            Assert.Null(matricula.Aluno);
            Assert.Null(matricula.Curso);
            Assert.Equal(default(DateTime), matricula.DataMatricula);
            Assert.Null(matricula.ValorMatricula);
            Assert.Null(matricula.DataConclusao);
            Assert.Null(matricula.Media);
            Assert.Null(matricula.Status);
        }
    }
}
