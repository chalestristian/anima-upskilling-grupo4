using api.Models;
using Xunit;

namespace api.Tests.Models
{
    public class CursoTestes
    {
        [Fact(DisplayName = nameof(CreateCurso_ValidParameters_Success))]
        public void CreateCurso_ValidParameters_Success()
        {
            // Arrange
            var curso = new Curso
            {
                Nome = "Curso de C#",
                CH = 40,
                Valor = 99.99m
            };

            // Act - No action required

            // Assert
            Assert.Equal("Curso de C#", curso.Nome);
            Assert.Equal(40, curso.CH);
            Assert.Equal(99.99m, curso.Valor);
        }

        [Fact(DisplayName = nameof(CreateCurso_InvalidParameters_Failure))]
        public void CreateCurso_InvalidParameters_Failure()
        {
            // Arrange
            var curso = new Curso();

            // Act - No action required

            // Assert
            Assert.Null(curso.Nome);
            Assert.Null(curso.CH);
            Assert.Null(curso.Valor);
        }
    }
}
