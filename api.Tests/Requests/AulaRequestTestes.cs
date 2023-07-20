using api.Requests;
using Xunit;

namespace api.Tests.Requests
{
    public class AulaRequestTestes
    {
        [Fact(DisplayName = nameof(CreateAluno_ValidParameters_Success))]
        public void CreateAluno_ValidParameters_Success()
        {
            // Arrange
            var aulaRequest = new AulaRequest
            {
                DescricaoAula = "Descri��o da aula",
                TituloAula = "T�tulo da aula",
                LinkAula = "https://www.example.com/aula"
            };

            // Act - No action required

            // Assert
            Assert.Equal("Descri��o da aula", aulaRequest.DescricaoAula);
            Assert.Equal("T�tulo da aula", aulaRequest.TituloAula);
            Assert.Equal("https://www.example.com/aula", aulaRequest.LinkAula);
        }

        [Fact(DisplayName = nameof(CreateAulaRequest_InvalidParameters_ThrowsException))]
        public void CreateAulaRequest_InvalidParameters_ThrowsException()
        {
            // Arrange
            var aulaRequest = new AulaRequest
            {
                DescricaoAula = null, // Par�metro inv�lido
                TituloAula = "T�tulo da aula",
                LinkAula = "https://www.example.com/aula"
            };

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => aulaRequest.DescricaoAula);
        }
    }
}
