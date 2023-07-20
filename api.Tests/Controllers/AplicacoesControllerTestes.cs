using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Xunit;
using api.Controllers;
using api.Models;
using api.Data;

namespace api.Tests.Controllers
{
    public class AplicacoesControllerTests
    {
        private readonly AppDbContext _context;
        private readonly AplicacoesController _controller;

        public AplicacoesControllerTests()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            _context = new AppDbContext(options);

            _controller = new AplicacoesController(_context);
        }

        [Fact]
        public void GetAplicacoes_ReturnsOkResult()
        {
            // Arrange
            var aplicacoes = new List<Aplicacao>
            {
                new Aplicacao { Id = 1, AppKey = "app1", SecretKey = "secret1" },
                new Aplicacao { Id = 2, AppKey = "app2", SecretKey = "secret2" }
            };
            _context.Aplicacoes.AddRange(aplicacoes);
            _context.SaveChanges();

            // Act
            var result = _controller.GetAplicacoes();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedAplicacoes = Assert.IsAssignableFrom<IEnumerable<Aplicacao>>(okResult.Value);
            Assert.Equal(aplicacoes.Count, returnedAplicacoes.Count());
        }

        [Fact]
        public void GetAplicacao_WithExistingId_ReturnsOkResult()
        {
            // Arrange
            var aplicacao = new Aplicacao { Id = 1, AppKey = "app1", SecretKey = "secret1" };
            _context.Aplicacoes.Add(aplicacao);
            _context.SaveChanges();

            // Act
            var result = _controller.GetAplicacao(aplicacao.Id);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedAplicacao = Assert.IsType<Aplicacao>(okResult.Value);
            Assert.Equal(aplicacao.Id, returnedAplicacao.Id);
        }

        [Fact]
        public void GetAplicacao_WithNonExistingId_ReturnsNotFoundResult()
        {
            // Arrange
            var nonExistingId = 999;

            // Act
            var result = _controller.GetAplicacao(nonExistingId);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public void CreateAplicacao_ReturnsCreatedAtActionResult()
        {
            // Arrange
            var aplicacao = new Aplicacao { AppKey = "app1", SecretKey = "secret1" };

            // Act
            var result = _controller.CreateAplicacao(aplicacao);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var returnedAplicacao = Assert.IsType<Aplicacao>(createdAtActionResult.Value);
            Assert.Equal(aplicacao.AppKey, returnedAplicacao.AppKey);
            Assert.Equal(aplicacao.SecretKey, returnedAplicacao.SecretKey);
        }

        [Fact]
        public void UpdateAplicacao_WithValidId_ReturnsNoContentResult()
        {
            // Arrange
            var aplicacao = new Aplicacao { Id = 1, AppKey = "app1", SecretKey = "secret1" };
            _context.Aplicacoes.Add(aplicacao);
            _context.SaveChanges();

            // Act
            var result = _controller.UpdateAplicacao(aplicacao.Id, aplicacao);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void UpdateAplicacao_WithInvalidId_ReturnsBadRequestResult()
        {
            // Arrange
            var aplicacao = new Aplicacao { Id = 1, AppKey = "app1", SecretKey = "secret1" };
            var invalidId = 999;

            // Act
            var result = _controller.UpdateAplicacao(invalidId, aplicacao);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public void DeleteAplicacao_WithExistingId_ReturnsNoContentResult()
        {
            // Arrange
            var aplicacao = new Aplicacao { Id = 1, AppKey = "app1", SecretKey = "secret1" };
            _context.Aplicacoes.Add(aplicacao);
            _context.SaveChanges();

            // Act
            var result = _controller.DeleteAplicacao(aplicacao.Id);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void DeleteAplicacao_WithNonExistingId_ReturnsNotFoundResult()
        {
            // Arrange
            var nonExistingId = 999;

            // Act
            var result = _controller.DeleteAplicacao(nonExistingId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}
