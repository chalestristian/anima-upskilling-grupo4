using api.Models;
using Xunit;

namespace api.Tests.Models
{
    public class UsuarioTestes
    {
        [Fact(DisplayName = nameof(CreateUsuario_ValidParameters_Success))]
        public void CreateUsuario_ValidParameters_Success()
        {
            // Arrange
            var usuario = new Usuario
            {
                Pessoa = new Pessoa(),
                Login = "johndoe",
                Senha = "password",
                DataUltimoAcesso = null,
                DataCadastro = new DateTime(2022, 1, 1)
            };

            // Act - No action required

            // Assert
            Assert.NotNull(usuario.Pessoa);
            Assert.Equal("johndoe", usuario.Login);
            Assert.Equal("password", usuario.Senha);
            Assert.Null(usuario.DataUltimoAcesso);
            Assert.Equal(new DateTime(2022, 1, 1), usuario.DataCadastro);
        }

        [Fact(DisplayName = nameof(CreateUsuario_InvalidParameters_Failure))]
        public void CreateUsuario_InvalidParameters_Failure()
        {
            // Arrange
            var usuario = new Usuario();

            // Act - No action required

            // Assert
            Assert.Null(usuario.Pessoa);
            Assert.Null(usuario.Login);
            Assert.Null(usuario.Senha);
            Assert.Null(usuario.DataUltimoAcesso);
            Assert.Equal(default(DateTime), usuario.DataCadastro);
        }
    }
}
