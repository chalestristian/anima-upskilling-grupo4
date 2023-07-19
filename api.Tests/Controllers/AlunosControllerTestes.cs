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
    public class AlunosControllerTests
    {
        private readonly AppDbContext _context;
        private readonly AlunosController _controller;

        public AlunosControllerTests()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            _context = new AppDbContext(options);

            _controller = new AlunosController(_context);
        }

        [Fact]
        public void GetAlunos_ReturnsOkResult()
        {
            // Arrange
            var alunos = new List<Aluno>
            {
                new Aluno { Id = 1, Nome = "João" },
                new Aluno { Id = 2, Nome = "Maria" }
            };
            _context.Alunos.AddRange(alunos);
            _context.SaveChanges();

            // Act
            var result = _controller.GetAlunos();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedAlunos = Assert.IsAssignableFrom<IEnumerable<Aluno>>(okResult.Value);
            Assert.Equal(alunos.Count, returnedAlunos.Count());
        }

        [Fact]
        public void GetAluno_WithExistingId_ReturnsOkResult()
        {
            // Arrange
            var aluno = new Aluno { Id = 1, Nome = "João" };
            _context.Alunos.Add(aluno);
            _context.SaveChanges();

            // Act
            var result = _controller.GetAluno(aluno.Id);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedAluno = Assert.IsType<Aluno>(okResult.Value);
            Assert.Equal(aluno.Id, returnedAluno.Id);
        }

        [Fact]
        public void GetAluno_WithNonExistingId_ReturnsNotFoundResult()
        {
            // Arrange
            var nonExistingId = 999;

            // Act
            var result = _controller.GetAluno(nonExistingId);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public void CreateAluno_ReturnsCreatedAtActionResult()
        {
            // Arrange
            var aluno = new Aluno { Nome = "João" };

            // Act
            var result = _controller.CreateAluno(aluno);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var returnedAluno = Assert.IsType<Aluno>(createdAtActionResult.Value);
            Assert.Equal(aluno.Nome, returnedAluno.Nome);
        }

        [Fact]
        public void UpdateAluno_WithValidId_ReturnsNoContentResult()
        {
            // Arrange
            var aluno = new Aluno { Id = 1, Nome = "João" };
            _context.Alunos.Add(aluno);
            _context.SaveChanges();

            // Act
            var result = _controller.UpdateAluno(aluno.Id, aluno);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void UpdateAluno_WithInvalidId_ReturnsBadRequestResult()
        {
            // Arrange
            var aluno = new Aluno { Id = 1, Nome = "João" };
            var invalidId = 999;

            // Act
            var result = _controller.UpdateAluno(invalidId, aluno);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public void DeleteAluno_WithExistingId_ReturnsNoContentResult()
        {
            // Arrange
            var aluno = new Aluno { Id = 1, Nome = "João" };
            _context.Alunos.Add(aluno);
            _context.SaveChanges();

            // Act
            var result = _controller.DeleteAluno(aluno.Id);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void DeleteAluno_WithNonExistingId_ReturnsNotFoundResult()
        {
            // Arrange
            var nonExistingId = 999;

            // Act
            var result = _controller.DeleteAluno(nonExistingId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}
