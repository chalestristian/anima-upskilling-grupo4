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
    public class UsuariosControllerTests
    {
        private readonly AppDbContext _context;
        private readonly UsuariosController _controller;

        public UsuariosControllerTests()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            _context = new AppDbContext(options);

            _controller = new UsuariosController(_context);
        }

        [Fact]
        public void GetUsuarios_ReturnsOkResult()
        {
            // Arrange
            var usuarios = new List<Usuario>
            {
                new Usuario { Id = 1, Login = "user1", Senha = "password1" },
                new Usuario { Id = 2, Login = "user2", Senha = "password2" }
            };
            _context.Usuarios.AddRange(usuarios);
            _context.SaveChanges();

            // Act
            var result = _controller.GetUsuarios();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedUsuarios = Assert.IsAssignableFrom<IEnumerable<Usuario>>(okResult.Value);
            Assert.Equal(usuarios.Count, returnedUsuarios.Count());
        }

        [Fact]
        public void GetUsuario_WithExistingId_ReturnsOkResult()
        {
            // Arrange
            var usuario = new Usuario { Id = 1, Login = "user1", Senha = "password1" };
            _context.Usuarios.Add(usuario);
            _context.SaveChanges();

            // Act
            var result = _controller.GetUsuario(usuario.Id);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedUsuario = Assert.IsType<Usuario>(okResult.Value);
            Assert.Equal(usuario.Id, returnedUsuario.Id);
        }

        [Fact]
        public void GetUsuario_WithNonExistingId_ReturnsNotFoundResult()
        {
            // Arrange
            var nonExistingId = 999;

            // Act
            var result = _controller.GetUsuario(nonExistingId);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public void CreateUsuario_ReturnsCreatedAtActionResult()
        {
            // Arrange
            var usuario = new Usuario { Login = "user1", Senha = "password1" };

            // Act
            var result = _controller.CreateUsuario(usuario);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var returnedUsuario = Assert.IsType<Usuario>(createdAtActionResult.Value);
            Assert.Equal(usuario.Login, returnedUsuario.Login);
        }

        [Fact]
        public void UpdateUsuario_WithValidId_ReturnsNoContentResult()
        {
            // Arrange
            var usuario = new Usuario { Id = 1, Login = "user1", Senha = "password1" };
            _context.Usuarios.Add(usuario);
            _context.SaveChanges();

            // Act
            var result = _controller.UpdateUsuario(usuario.Id, usuario);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void UpdateUsuario_WithInvalidId_ReturnsBadRequestResult()
        {
            // Arrange
            var usuario = new Usuario { Id = 1, Login = "user1", Senha = "password1" };
            var invalidId = 999;

            // Act
            var result = _controller.UpdateUsuario(invalidId, usuario);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public void DeleteUsuario_WithExistingId_ReturnsNoContentResult()
        {
            // Arrange
            var usuario = new Usuario { Id = 1, Login = "user1", Senha = "password1" };
            _context.Usuarios.Add(usuario);
            _context.SaveChanges();

            // Act
            var result = _controller.DeleteUsuario(usuario.Id);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void DeleteUsuario_WithNonExistingId_ReturnsNotFoundResult()
        {
            // Arrange
            var nonExistingId = 999;

            // Act
            var result = _controller.DeleteUsuario(nonExistingId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}
