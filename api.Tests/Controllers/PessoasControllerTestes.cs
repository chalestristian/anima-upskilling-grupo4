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
    public class PessoasControllerTests
    {
        private readonly AppDbContext _context;
        private readonly PessoasController _controller;

        public PessoasControllerTests()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            _context = new AppDbContext(options);

            _controller = new PessoasController(_context);
        }

        [Fact]
        public void GetPessoas_ReturnsOkResult()
        {
            // Arrange
            var pessoas = new List<Pessoa>
            {
                new Pessoa { Id = 1, Nome = "João", CPF = "123456789", Celular = "987654321", Email = "joao@example.com" },
                new Pessoa { Id = 2, Nome = "Maria", CPF = "987654321", Celular = "123456789", Email = "maria@example.com" }
            };
            _context.Pessoas.AddRange(pessoas);
            _context.SaveChanges();

            // Act
            var result = _controller.GetPessoas();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedPessoas = Assert.IsAssignableFrom<IEnumerable<Pessoa>>(okResult.Value);
            Assert.Equal(pessoas.Count, returnedPessoas.Count());
        }

        [Fact]
        public void GetPessoa_WithExistingId_ReturnsOkResult()
        {
            // Arrange
            var pessoa = new Pessoa { Id = 1, Nome = "João", CPF = "123456789", Celular = "987654321", Email = "joao@example.com" };
            _context.Pessoas.Add(pessoa);
            _context.SaveChanges();

            // Act
            var result = _controller.GetPessoa(pessoa.Id);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedPessoa = Assert.IsType<Pessoa>(okResult.Value);
            Assert.Equal(pessoa.Id, returnedPessoa.Id);
        }

        [Fact]
        public void GetPessoa_WithNonExistingId_ReturnsNotFoundResult()
        {
            // Arrange
            var nonExistingId = 999;

            // Act
            var result = _controller.GetPessoa(nonExistingId);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public void CreatePessoa_ReturnsCreatedAtActionResult()
        {
            // Arrange
            var pessoa = new Pessoa { Nome = "João", CPF = "123456789", Celular = "987654321", Email = "joao@example.com" };

            // Act
            var result = _controller.CreatePessoa(pessoa);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var returnedPessoa = Assert.IsType<Pessoa>(createdAtActionResult.Value);
            Assert.Equal(pessoa.Nome, returnedPessoa.Nome);
        }

        [Fact]
        public void UpdatePessoa_WithValidId_ReturnsNoContentResult()
        {
            // Arrange
            var pessoa = new Pessoa { Id = 1, Nome = "João", CPF = "123456789", Celular = "987654321", Email = "joao@example.com" };
            _context.Pessoas.Add(pessoa);
            _context.SaveChanges();

            // Act
            var result = _controller.UpdatePessoa(pessoa.Id, pessoa);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void UpdatePessoa_WithInvalidId_ReturnsBadRequestResult()
        {
            // Arrange
            var pessoa = new Pessoa { Id = 1, Nome = "João", CPF = "123456789", Celular = "987654321", Email = "joao@example.com" };
            var invalidId = 999;

            // Act
            var result = _controller.UpdatePessoa(invalidId, pessoa);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public void DeletePessoa_WithExistingId_ReturnsNoContentResult()
        {
            // Arrange
            var pessoa = new Pessoa { Id = 1, Nome = "João", CPF = "123456789", Celular = "987654321", Email = "joao@example.com" };
            _context.Pessoas.Add(pessoa);
            _context.SaveChanges();

            // Act
            var result = _controller.DeletePessoa(pessoa.Id);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void DeletePessoa_WithNonExistingId_ReturnsNotFoundResult()
        {
            // Arrange
            var nonExistingId = 999;

            // Act
            var result = _controller.DeletePessoa(nonExistingId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}
