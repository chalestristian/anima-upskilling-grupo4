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
    public class NotasModulosControllerTests
    {
        private readonly AppDbContext _context;
        private readonly NotasModulosController _controller;

        public NotasModulosControllerTests()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            _context = new AppDbContext(options);

            _controller = new NotasModulosController(_context);
        }

        [Fact]
        public void GetNotasModulos_ReturnsOkResult()
        {
            // Arrange
            var notasModulos = new List<NotaModulo>
            {
                new NotaModulo { Id = 1, Modulo = new ModuloCurso(), Nota = 8.5m, DataLancamento = DateTime.Now },
                new NotaModulo { Id = 2, Modulo = new ModuloCurso(), Nota = 7.8m, DataLancamento = DateTime.Now }
            };
            _context.Notas.AddRange(notasModulos);
            _context.SaveChanges();

            // Act
            var result = _controller.GetNotasModulos();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedNotasModulos = Assert.IsAssignableFrom<IEnumerable<NotaModulo>>(okResult.Value);
            Assert.Equal(notasModulos.Count, returnedNotasModulos.Count());
        }

        [Fact]
        public void GetNotaModulo_WithExistingId_ReturnsOkResult()
        {
            // Arrange
            var notaModulo = new NotaModulo { Id = 1, Modulo = new ModuloCurso(), Nota = 8.5m, DataLancamento = DateTime.Now };
            _context.Notas.Add(notaModulo);
            _context.SaveChanges();

            // Act
            var result = _controller.GetNotaModulo(notaModulo.Id);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedNotaModulo = Assert.IsType<NotaModulo>(okResult.Value);
            Assert.Equal(notaModulo.Id, returnedNotaModulo.Id);
        }

        [Fact]
        public void GetNotaModulo_WithNonExistingId_ReturnsNotFoundResult()
        {
            // Arrange
            var nonExistingId = 999;

            // Act
            var result = _controller.GetNotaModulo(nonExistingId);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public void CreateNotaModulo_ReturnsCreatedAtActionResult()
        {
            // Arrange
            var notaModulo = new NotaModulo { Modulo = new ModuloCurso(), Nota = 8.5m, DataLancamento = DateTime.Now };

            // Act
            var result = _controller.CreateNotaModulo(notaModulo);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var returnedNotaModulo = Assert.IsType<NotaModulo>(createdAtActionResult.Value);
            Assert.Equal(notaModulo.Nota, returnedNotaModulo.Nota);
        }

        [Fact]
        public void UpdateNotaModulo_WithValidId_ReturnsNoContentResult()
        {
            // Arrange
            var notaModulo = new NotaModulo { Id = 1, Modulo = new ModuloCurso(), Nota = 8.5m, DataLancamento = DateTime.Now };
            _context.Notas.Add(notaModulo);
            _context.SaveChanges();

            // Act
            var result = _controller.UpdateNotaModulo(notaModulo.Id, notaModulo);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void UpdateNotaModulo_WithInvalidId_ReturnsBadRequestResult()
        {
            // Arrange
            var notaModulo = new NotaModulo { Id = 1, Modulo = new ModuloCurso(), Nota = 8.5m, DataLancamento = DateTime.Now };
            var invalidId = 999;

            // Act
            var result = _controller.UpdateNotaModulo(invalidId, notaModulo);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public void DeleteNotaModulo_WithExistingId_ReturnsNoContentResult()
        {
            // Arrange
            var notaModulo = new NotaModulo { Id = 1, Modulo = new ModuloCurso(), Nota = 8.5m, DataLancamento = DateTime.Now };
            _context.Notas.Add(notaModulo);
            _context.SaveChanges();

            // Act
            var result = _controller.DeleteNotaModulo(notaModulo.Id);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void DeleteNotaModulo_WithNonExistingId_ReturnsNotFoundResult()
        {
            // Arrange
            var nonExistingId = 999;

            // Act
            var result = _controller.DeleteNotaModulo(nonExistingId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}
