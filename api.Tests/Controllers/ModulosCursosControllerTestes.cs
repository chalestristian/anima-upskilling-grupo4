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
    public class ModulosCursosControllerTests
    {
        private readonly AppDbContext _context;
        private readonly ModulosCursosController _controller;

        public ModulosCursosControllerTests()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            _context = new AppDbContext(options);

            _controller = new ModulosCursosController(_context);
        }

        [Fact]
        public void GetModulosCursos_ReturnsOkResult()
        {
            // Arrange
            var modulosCursos = new List<ModuloCurso>
            {
                new ModuloCurso { Id = 1, Curso = new Curso(), Nome = "Módulo 1", CH = 20 },
                new ModuloCurso { Id = 2, Curso = new Curso(), Nome = "Módulo 2", CH = 30 }
            };
            _context.ModulosCursos.AddRange(modulosCursos);
            _context.SaveChanges();

            // Act
            var result = _controller.GetModulosCursos();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedModulosCursos = Assert.IsAssignableFrom<IEnumerable<ModuloCurso>>(okResult.Value);
            Assert.Equal(modulosCursos.Count, returnedModulosCursos.Count());
        }

        [Fact]
        public void GetModuloCurso_WithExistingId_ReturnsOkResult()
        {
            // Arrange
            var moduloCurso = new ModuloCurso { Id = 1, Curso = new Curso(), Nome = "Módulo 1", CH = 20 };
            _context.ModulosCursos.Add(moduloCurso);
            _context.SaveChanges();

            // Act
            var result = _controller.GetModuloCurso(moduloCurso.Id);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedModuloCurso = Assert.IsType<ModuloCurso>(okResult.Value);
            Assert.Equal(moduloCurso.Id, returnedModuloCurso.Id);
        }

        [Fact]
        public void GetModuloCurso_WithNonExistingId_ReturnsNotFoundResult()
        {
            // Arrange
            var nonExistingId = 999;

            // Act
            var result = _controller.GetModuloCurso(nonExistingId);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public void CreateModuloCurso_ReturnsCreatedAtActionResult()
        {
            // Arrange
            var moduloCurso = new ModuloCurso { Curso = new Curso(), Nome = "Módulo 1", CH = 20 };

            // Act
            var result = _controller.CreateModuloCurso(moduloCurso);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var returnedModuloCurso = Assert.IsType<ModuloCurso>(createdAtActionResult.Value);
            Assert.Equal(moduloCurso.Nome, returnedModuloCurso.Nome);
        }

        [Fact]
        public void UpdateModuloCurso_WithValidId_ReturnsNoContentResult()
        {
            // Arrange
            var moduloCurso = new ModuloCurso { Id = 1, Curso = new Curso(), Nome = "Módulo 1", CH = 20 };
            _context.ModulosCursos.Add(moduloCurso);
            _context.SaveChanges();

            // Act
            var result = _controller.UpdateModuloCurso(moduloCurso.Id, moduloCurso);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void UpdateModuloCurso_WithInvalidId_ReturnsBadRequestResult()
        {
            // Arrange
            var moduloCurso = new ModuloCurso { Id = 1, Curso = new Curso(), Nome = "Módulo 1", CH = 20 };
            var invalidId = 999;

            // Act
            var result = _controller.UpdateModuloCurso(invalidId, moduloCurso);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public void DeleteModuloCurso_WithExistingId_ReturnsNoContentResult()
        {
            // Arrange
            var moduloCurso = new ModuloCurso { Id = 1, Curso = new Curso(), Nome = "Módulo 1", CH = 20 };
            _context.ModulosCursos.Add(moduloCurso);
            _context.SaveChanges();

            // Act
            var result = _controller.DeleteModuloCurso(moduloCurso.Id);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void DeleteModuloCurso_WithNonExistingId_ReturnsNotFoundResult()
        {
            // Arrange
            var nonExistingId = 999;

            // Act
            var result = _controller.DeleteModuloCurso(nonExistingId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}
