using api.Models;
using Xunit;

namespace api.Tests.Models
{
    public class AlunoTest
    {
        [Fact(DisplayName = nameof(CreateAluno_ValidParameters_Success))]
        public void CreateAluno_ValidParameters_Success()
        {
            // Arrange
            var aluno = new Aluno
            {
                Id = 1,
                Pessoa = new Pessoa(),
                Matricula = "123456",
                DataCadastro = new DateTime(2023, 7, 1)
            };

            // Act - No action required

            // Assert
            Assert.Equal(1, aluno.Id);
            Assert.NotNull(aluno.Pessoa);
            Assert.Equal("123456", aluno.Matricula);
            Assert.Equal(new DateTime(2023, 7, 1), aluno.DataCadastro);
        }

        [Fact(DisplayName = nameof(CreateAluno_InvalidParameters_Failure))]
        public void CreateAluno_InvalidParameters_Failure()
        {
            // Arrange
            var aluno = new Aluno();

            // Act - No action required

            // Assert
            Assert.Equal(0, aluno.Id);
            Assert.Null(aluno.Pessoa);
            Assert.Null(aluno.Matricula);
            Assert.Equal(default(DateTime), aluno.DataCadastro);
        }
    }
}
