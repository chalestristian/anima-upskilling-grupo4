using api.Models;
using Xunit;

namespace api.Tests.Models
{
    public class AplicacaoTestes
    {
        [Fact(DisplayName = nameof(CreateAplicacao_ValidParameters_Success))]
        public void CreateAplicacao_ValidParameters_Success()
        {
            // Arrange
            var app = new Aplicacao
            {
                AppKey = "myAppKey",
                SecretKey = "mySecretKey"
            };

            // Act - No action required

            // Assert
            Assert.Equal("myAppKey", app.AppKey);
            Assert.Equal("mySecretKey", app.SecretKey);
        }

        [Fact(DisplayName = nameof(CreateAplicacao_InvalidParameters_Failure))]
        public void CreateAplicacao_InvalidParameters_Failure()
        {
            // Arrange
            var app = new Aplicacao();

            // Act - No action required

            // Assert
            Assert.Null(app.AppKey);
            Assert.Null(app.SecretKey);
        }
    }
}
