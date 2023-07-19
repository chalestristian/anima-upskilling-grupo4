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
    public class CursosControllerTests
    {
        private readonly AppDbContext _context;
        private readonly CursosController _controller;

        public CursosControllerTests()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            _context = new AppDbContext(options);

            _controller = new CursosController(_context);
        }

        [Fact]
        public void GetCursos_ReturnsOkResult()
        {
            // Arrange
            var cursos = new List<Curso>
            {
                new Curso { Id = 1, Nome = "Curso 1", CH = 20, Valor = 100 },
                new Curso { Id = 2, Nome = "Curso 2", CH = 30, Valor = 150 }
            };
            _context.Cursos.AddRange(cursos);
            _context.SaveChanges();

            // Act
            var result = _controller.GetCursos();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedCursos = Assert.IsAssignableFrom<IEnumerable<Curso>>(okResult.Value);
            Assert.Equal(cursos.Count, returnedCursos.Count());
        }

        [Fact]
        public void GetCurso_WithExistingId_ReturnsOkResult()
        {
            // Arrange
            var curso = new Curso { Id = 1, Nome = "Curso 1", CH = 20, Valor = 100 };
            _context.Cursos.Add(curso);
            _context.SaveChanges();

            // Act
            var result = _controller.GetCurso(curso.Id);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedCurso = Assert.IsType<Curso>(okResult.Value);
            Assert.Equal(curso.Id, returnedCurso.Id);
        }

        [Fact]
        public void GetCurso_WithNonExistingId_ReturnsNotFoundResult()
        {
            // Arrange
            var nonExistingId = 999;

            // Act
            var result = _controller.GetCurso(nonExistingId);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public void CreateCurso_ReturnsCreatedAtActionResult()
        {
            // Arrange
            var curso = new Curso { Nome = "Curso 1", CH = 20, Valor = 100 };

            // Act
            var result = _controller.CreateCurso(curso);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var returnedCurso = Assert.IsType<Curso>(createdAtActionResult.Value);
            Assert.Equal(curso.Nome, returnedCurso.Nome);
            Assert.Equal(curso.CH, returnedCurso.CH);
            Assert.Equal(curso.Valor, returnedCurso.Valor);
        }

        [Fact]
        public void UpdateCurso_WithValidId_ReturnsNoContentResult()
        {
            // Arrange
            var curso = new Curso { Id = 1, Nome = "Curso 1", CH = 20, Valor = 100 };
            _context.Cursos.Add(curso);
            _context.SaveChanges();

            // Act
            var result = _controller.UpdateCurso(curso.Id, curso);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void UpdateCurso_WithInvalidId_ReturnsBadRequestResult()
        {
            // Arrange
            var curso = new Curso { Id = 1, Nome = "Curso 1", CH = 20, Valor = 100 };
            var invalidId = 999;

            // Act
            var result = _controller.UpdateCurso(invalidId, curso);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public void DeleteCurso_WithExistingId_ReturnsNoContentResult()
        {
            // Arrange
            var curso = new Curso { Id = 1, Nome = "Curso 1", CH = 20, Valor = 100 };
            _context.Cursos.Add(curso);
            _context.SaveChanges();

            // Act
            var result = _controller.DeleteCurso(curso.Id);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void DeleteCurso_WithNonExistingId_ReturnsNotFoundResult()
        {
            // Arrange
            var nonExistingId = 999;

            // Act
            var result = _controller.DeleteCurso(nonExistingId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}
