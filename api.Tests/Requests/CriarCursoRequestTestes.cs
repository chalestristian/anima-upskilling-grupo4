using api.Requests;
using System;
using System.Collections.Generic;
using Xunit;

namespace api.Tests.Requests
{
    public class CriarCursoRequestTest
    {
        [Fact]
        public void CreateCriarCursoRequest_ValidParameters_Success()
        {
            // Arrange
            var cursoRequest = new CursoRequest
            {
                Nome = "Curso de Programação",
                CH = 40,
                Valor = 199.99m
            };

            var aulaRequest1 = new AulaRequest
            {
                DescricaoAula = "Introdução à Programação",
                TituloAula = "Aula 1",
                LinkAula = "https://www.example.com/aula1"
            };

            var aulaRequest2 = new AulaRequest
            {
                DescricaoAula = "Estruturas de Controle",
                TituloAula = "Aula 2",
                LinkAula = "https://www.example.com/aula2"
            };

            var criarCursoRequest = new CriarCursoRequest
            {
                Curso = cursoRequest,
                Aulas = new List<AulaRequest> { aulaRequest1, aulaRequest2 }
            };

            // Act
            var curso = criarCursoRequest.Curso;
            var aulas = criarCursoRequest.Aulas;

            // Assert
            Assert.Equal("Curso de Programação", curso.Nome);
            Assert.Equal(40, curso.CH);
            Assert.Equal(199.99m, curso.Valor);
            Assert.Collection(aulas,
                aula => Assert.Equal("Introdução à Programação", aula.DescricaoAula),
                aula => Assert.Equal("Estruturas de Controle", aula.DescricaoAula)
            );
        }

        [Fact]
        public void CreateCriarCursoRequest_InvalidParameters_ThrowsException()
        {
            // Arrange
            var criarCursoRequest = new CriarCursoRequest
            {
                Curso = null, // Parâmetro inválido
                Aulas = null // Parâmetro inválido
            };

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => criarCursoRequest.Curso);
            Assert.Throws<ArgumentNullException>(() => criarCursoRequest.Aulas);
        }
    }
}
