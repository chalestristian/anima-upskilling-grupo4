using System;
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
    public class MatriculasControllerTests
    {
        private readonly AppDbContext _context;
        private readonly MatriculasController _controller;

        public MatriculasControllerTests()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            _context = new AppDbContext(options);

            _controller = new MatriculasController(_context);
        }

        [Fact]
        public void GetMatriculas_ReturnsOkResult()
        {
            // Arrange
            var matriculas = new List<Matricula>
            {
                new Matricula { Id = 1, Aluno = new Aluno(), Curso = new Curso(), DataMatricula = DateTime.Now },
                new Matricula { Id = 2, Aluno = new Aluno(), Curso = new Curso(), DataMatricula = DateTime.Now }
            };
            _context.Matriculas.AddRange(matriculas);
            _context.SaveChanges();

            // Act
            var result = _controller.GetMatriculas();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedMatriculas = Assert.IsAssignableFrom<IEnumerable<Matricula>>(okResult.Value);
            Assert.Equal(matriculas.Count, returnedMatriculas.Count());
        }

        [Fact]
        public void GetMatricula_WithExistingId_ReturnsOkResult()
        {
            // Arrange
            var matricula = new Matricula { Id = 1, Aluno = new Aluno(), Curso = new Curso(), DataMatricula = DateTime.Now };
            _context.Matriculas.Add(matricula);
            _context.SaveChanges();

            // Act
            var result = _controller.GetMatricula(matricula.Id);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedMatricula = Assert.IsType<Matricula>(okResult.Value);
            Assert.Equal(matricula.Id, returnedMatricula.Id);
        }

        [Fact]
        public void GetMatricula_WithNonExistingId_ReturnsNotFoundResult()
        {
            // Arrange
            var nonExistingId = 999;

            // Act
            var result = _controller.GetMatricula(nonExistingId);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public void CreateMatricula_ReturnsCreatedAtActionResult()
        {
            // Arrange
            var matricula = new Matricula { Aluno = new Aluno(), Curso = new Curso(), DataMatricula = DateTime.Now };

            // Act
            var result = _controller.CreateMatricula(matricula);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var returnedMatricula = Assert.IsType<Matricula>(createdAtActionResult.Value);
            Assert.Equal(matricula.DataMatricula, returnedMatricula.DataMatricula);
        }

        [Fact]
        public void UpdateMatricula_WithValidId_ReturnsNoContentResult()
        {
            // Arrange
            var matricula = new Matricula { Id = 1, Aluno = new Aluno(), Curso = new Curso(), DataMatricula = DateTime.Now };
            _context.Matriculas.Add(matricula);
            _context.SaveChanges();

            // Act
            var result = _controller.UpdateMatricula(matricula.Id, matricula);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void UpdateMatricula_WithInvalidId_ReturnsBadRequestResult()
        {
            // Arrange
            var matricula = new Matricula { Id = 1, Aluno = new Aluno(), Curso = new Curso(), DataMatricula = DateTime.Now };
            var invalidId = 999;

            // Act
            var result = _controller.UpdateMatricula(invalidId, matricula);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public void DeleteMatricula_WithExistingId_ReturnsNoContentResult()
        {
            // Arrange
            var matricula = new Matricula { Id = 1, Aluno = new Aluno(), Curso = new Curso(), DataMatricula = DateTime.Now };
            _context.Matriculas.Add(matricula);
            _context.SaveChanges();

            // Act
            var result = _controller.DeleteMatricula(matricula.Id);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void DeleteMatricula_WithNonExistingId_ReturnsNotFoundResult()
        {
            // Arrange
            var nonExistingId = 999;

            // Act
            var result = _controller.DeleteMatricula(nonExistingId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}
