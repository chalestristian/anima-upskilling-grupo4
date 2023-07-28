using api.Models;
using Xunit;

namespace api.Tests.Models
{
    public class ModuloCursoTestes
    {
        [Fact(DisplayName = nameof(CreateModuloCurso_ValidParameters_Success))]

        public void CreateModuloCurso_ValidParameters_Success()
        {
            // Arrange
            var moduloCurso = new ModuloCurso
            {
                Curso = new Curso(),
                Nome = "Módulo 1",
                CH = 40
            };

            // Act - No action required

            // Assert
            Assert.NotNull(moduloCurso.Curso);
            Assert.Equal("Módulo 1", moduloCurso.Nome);
            Assert.Equal(40, moduloCurso.CH);
        }

        [Fact(DisplayName = nameof(CreateModuloCurso_InvalidParameters_Failure))]
        public void CreateModuloCurso_InvalidParameters_Failure()
        {
            // Arrange
            var moduloCurso = new ModuloCurso();

            // Act - No action required

            // Assert
            Assert.Null(moduloCurso.Curso);
            Assert.Null(moduloCurso.Nome);
        }
    }
}
