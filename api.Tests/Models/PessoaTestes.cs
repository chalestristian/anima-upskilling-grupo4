using api.Models;
using Xunit;

namespace api.Tests.Models
{
    public class PessoaTestes
    {
        [Fact(DisplayName = nameof(CreatePessoa_ValidParameters_Success))]
        public void CreatePessoa_ValidParameters_Success()
        {
            // Arrange
            var pessoa = new Pessoa
            {
                Nome = "João Silva",
                CPF = "12345678900",
                Celular = "999999999",
                Email = "joao.silva@example.com"
            };

            // Act - No action required

            // Assert
            Assert.Equal("João Silva", pessoa.Nome);
            Assert.Equal("12345678900", pessoa.CPF);
            Assert.Equal("999999999", pessoa.Celular);
            Assert.Equal("joao.silva@example.com", pessoa.Email);
        }

        [Fact(DisplayName = nameof(CreatePessoa_InvalidParameters_Failure))]
        public void CreatePessoa_InvalidParameters_Failure()
        {
            // Arrange
            var pessoa = new Pessoa();

            // Act - No action required

            // Assert
            Assert.Null(pessoa.Nome);
            Assert.Null(pessoa.CPF);
            Assert.Null(pessoa.Celular);
            Assert.Null(pessoa.Email);
        }
    }
}
